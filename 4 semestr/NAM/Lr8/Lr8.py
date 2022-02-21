import numpy as np
import matplotlib.pyplot as plot
import sympy as sp
import math

integration_types = {
    "left_rect": lambda func, n, left_border, h:
    np.sum(np.array([func(left_border + i*h) for i in range(int(n))], dtype=np.float64))*h,
    "right_rect": lambda func, n, left_border, h:
    np.sum(np.array([func(left_border + (i+1)*h) for i in range(int(n))], dtype=np.float64))*h,
    "central_rect": lambda func, n, left_border, h:
    np.sum(np.array([func(left_border + (i+1/2)*h) for i in range(int(n))], dtype=np.float64))*h,
    "trapezium": lambda func, n, left_border, h:
    (np.sum(np.array([func(left_border + i*h) for i in range(1, int(n))], dtype=np.float64)) + 0.5*(func(left_border + h*n) + func(left_border)))*h,
    "simpson": lambda func, n, left_border, h :
    (h/3)*(func(left_border) + func(left_border + n*h)
        + 2*np.sum(np.array([func(2*i) for i in range(1, int(n)//2)], dtype=np.float64))
        + 4*np.sum(np.array([func(2*i + 1) for i in range(int(n)//2)], dtype=np.float64)))
}



def error_estimation(method, func, a, b, m, eps):
    n = 1
    while True:
        if abs(method(func, a, b, 2 * n) - method(func, a, b, n))/m < eps:
            break
        n *= 2
    #print(abs(method(func, a, b, n * 2) - method(func, a, b, n)))
    return n * 2


def calculate_iterations(left_border, right_border, accuracy):
    h = np.power(accuracy, 0.25)
    return (right_border - left_border)/h


def integrate(func, left_border, right_border, accuracy, _type_, total=None):
    if _type_ != "simpson":
        n = np.ceil(calculate_iterations(left_border, right_border, accuracy))
        h = (right_border - left_border)/n
        I = integration_types[_type_](func, n, left_border, h)
        I_prev = I + accuracy*20
        counter = 0
        while abs(I - I_prev)/3 > accuracy or (total is not None and counter < total):
            counter += 1
            I_prev = I
            h /= 2
            n *= 2
            I = integration_types[_type_](func, n, left_border, h)
        return I, counter
    else:
        n = np.ceil(calculate_iterations(left_border, right_border, accuracy) / 2) * 2
        h = (right_border - left_border)/n
        I = integration_types[_type_](func, n, left_border, h)
        I_prev = I + accuracy*20
        counter = 0
        while abs(I - I_prev)/15 > accuracy or (total is not None and counter < total):
            counter += 1
            I_prev = I
            h /= 2
            n *= 2
            I = integration_types[_type_](func, n, left_border, h)
        return I, counter


def diff(func, x, accuracy):
    diff = 10*accuracy
    h = 0.1
    derivative = 10*accuracy
    derivative = (func(x + h) - func(x - h))/(2*h)
    counter = 0
    while abs(diff) > accuracy:
        temp = derivative
        derivative = (func(x + h) - func(x - h))/(2*h)
        if counter != 0:
            diff = derivative - temp
        else:
            diff = 10*accuracy
        h /= 4
        counter += 1
    return derivative, counter


def diff2(func, x, accuracy):
    diff = 10*accuracy
    h = 0.1
    derivative = 10*accuracy
    derivative = (func(x + h) - func(x - h))/(2*h)
    counter = 0
    while abs(diff) > accuracy:
        temp = derivative
        derivative = (func(x + h) + func(x - h) - 2*func(x))/(4*h**2)
        if counter != 0:
            diff = derivative - temp
        else:
            diff = 10*accuracy
        h /= 4
        counter += 1
    return derivative, counter


def simpson(func, a, b, n):
    h = (b - a) / n
    return h / 3 * sum(
        [func(a + h * i) + 4 * func(a + h * (i + 1)) + func(a + h * (i + 2)) for i in range(0, n - 1, 2)])


print("First diff")
print(diff(lambda x: sp.log(x), 2, 1e-2))

print("Second diff")
print(diff2(lambda x: sp.log(x), 2, 1e-2))

print("Trapezium method")
print(integrate(lambda x: sp.log(x), 1, 3, 1e-5, "trapezium"))

print('метод Симпcон:')
n = error_estimation(simpson, lambda x:sp.log(x), 1, 3, 2, 1e-5)
s = simpson(lambda x:sp.log(x), 1, 3, n)

print('n:  ', s)
s2 = simpson(lambda x:sp.log(x), 1, 3, n // 2)
print('n/2:', s2)

print("Central rect method:")
print(integrate(lambda x: sp.log(x), 1, 3, 1e-5, "central_rect"))

x, y = sp.symbols('x y')
sym = sp.integrate(sp.log(x), (x, 1, 3))
print('Точное решение:', sp.simplify(sym), '=', sym.n(16))
