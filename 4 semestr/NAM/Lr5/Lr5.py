import numpy as np
from functools import reduce

np.set_printoptions(precision=4, floatmode='fixed')
variant = 11.0

C = np.array([[0.2, 0, 0.2, 0, 0],
              [0, 0.2, 0, 0.2, 0],
              [0.2, 0, 0.2, 0, 0.2],
              [0, 0.2, 0, 0.2, 0],
              [0, 0, 0.2, 0, 0.2]], dtype=float)

D = np.array([[2.33, 0.81, 0.67, 0.92, -0.53],
              [0.81, 2.33, 0.81, 0.67, 0.92],
              [0.67, 0.81, 2.33, 0.81, 0.92],
              [0.92, 0.67, 0.81, 2.33, -0.53],
              [-0.53, 0.92, 0.92, -0.53, 2.33]], dtype=float)

A = np.add(np.multiply(C, variant), D)

def hessenberg_decomposition(A):
    A = A.astype(float)
    n = len(A)
    for step in range(n-2):
        row_idx = step + 2 + np.argmax(np.abs(A[step + 2:, step]))
        if(A[row_idx, step] == 0):
            continue
        elif abs(A[row_idx, step]) < abs(A[step + 1, step]):
            row_idx = step + 1
        row = A[row_idx].copy()
        A[row_idx] = A[step+1]
        A[step+1] = row
        column = A[:, row_idx].copy()
        A[:, row_idx] = A[:, step+1]
        A[:, step+1] = column
        for i in range(step+2, n):
            multiplier = A[i, step] / A[step+1, step]
            A[i] -= multiplier * A[step + 1]
            A[:, step+1] += multiplier * A[:, i]
    return A

def qr_hessenberg(A):
    n = len(A)
    Q = np.eye(n)
    for i in range(n):
        j = i+1
        if j < n and A[j, i] != 0:
            c = A[i, i] / (A[i ,i]** 2 + A[j, i] ** 2) ** 0.5
            s = s = A[j, i] / (A[i,i] ** 2 + A[j, i] ** 2) ** 0.5
            T = np.eye(n)
            T[[i,j,i,j], [i,j,j,i]] = [c, c, s, -s]
            Q = T @ Q
            A[i], A[j] = c * A[i] + s * A[j], -s * A[i] + c * A[j]
    return Q.T, A

def qr_eigenvalues(A, epsilon=1e-6):
    A = hessenberg_decomposition(A)
    n = len(A)
    while np.sum(np.abs(np.diag(A, k=-1))) > epsilon:
        Q, R = qr_hessenberg(A)
        A = R @ Q
    return np.diag(A)

my_vals = qr_eigenvalues(A)
vals = np.linalg.eigvals(A)
w,v = np.linalg.eig(A)
print("собственные векторы входной матрицы")
print(v)

print("Найденные сзм - разложение/библиотечная функция")
for my_val, val in zip(my_vals, vals):
    print(f"{round(my_val,4): <30}{round(val,4): <30}")
