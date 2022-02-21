import numpy

numpy.set_printoptions(precision=4, floatmode='fixed')
option = 11.0

C_ = numpy.array([[0.2, 0, 0.2, 0, 0],
                 [0, 0.2, 0, 0.2, 0],
                 [0.2, 0, 0.2, 0, 0.2],
                 [0, 0.2, 0, 0.2, 0],
                 [0, 0, 0.2, 0, 0.2]], dtype=numpy.float64)

D_ = numpy.array([[2.33, 0.81, 0.67, 0.92, -0.53],
                 [-0.53, 2.33, 0.81, 0.67, 0.92],
                 [0.92, -0.53, 2.33, 0.81, 0.67],
                 [0.67, 0.92, -0.53, 2.33, 0.81],
                 [0.81, 0.67, 0.92, -0.53, 2.33]], dtype=numpy.float64)

B_ = numpy.array([4.2, 4.2, 4.2, 4.2, 4.2], dtype=numpy.float64)

A_ = numpy.add(numpy.multiply(C_, option), D_)
#print(A_)

A = numpy.array([[ 4.5300, 0.8100, 2.8700, 0.9200, -0.5300, 4.2],
     [-0.5300, 4.5300, 0.8100, 2.8700, 0.9200, 4.2],
     [3.1200, -0.5300, 4.5300, 0.8100, 2.8700, 4.2],
     [0.6700, 3.1200, -0.5300, 4.5300, 0.8100, 4.2],
     [0.8100, 0.6700, 3.1200, -0.5300, 4.5300, 4.2]], dtype=numpy.float64)
  

def matrix_max_column(matrix, n):
    max_element = matrix[n][n]
    max_column = n
    for i in range(n + 1, len(matrix)):
        if abs(matrix[n][i]) > abs(max_element):
            max_element = matrix[n][i]
            max_column = i
        if max_column != n:
            matrix[n], matrix[max_column] = matrix[max_column], matrix[n]
 
def Gauss_Column(matrix):
    n = len(matrix)
    for k in range(n - 1):
        matrix_max_column(matrix, k)
        for i in range(k + 1, n):
            div = matrix[i][k] / matrix[k][k]
            matrix[i][-1] -= div * matrix[k][-1]
            for j in range(k, n):
                matrix[i][j] -= div * matrix[k][j]
    if is_singular(matrix):
        print('Система имеет бесконечное число решений')
        return

    x=[0 for i in range(n)]
    for k in range(n - 1, -1, -1):
        x[k] = (matrix[k][-1] - sum([matrix[k][j] * x[j] for j in range(k + 1, n) ])) / matrix[k][k]
    print(['{:.4f}'.format(x[i]) for i in range(n)])

    return x

def is_singular(matrix):
    for i in range(len(matrix)):
        if not matrix[i][i]:
            return True
        return False

def Gauss_Main(matrix):
    n = matrix.shape[0]
    for k in range(n-1):
      main_element(matrix,k)
      for i in range(k+1,n):
         div = matrix[i, k] / matrix[k,k]
         matrix[i,-1] -= div * matrix[k,-1]       
    if is_singular(matrix):
        print('Система имеет бесконечное число решений')
        return
    x = numpy.array([0.0 for i in range(n)], float).transpose()
    for k in range(n - 1, -1, -1):
        x[k] = (matrix[k,-1] - sum([matrix[k,i] * x[i] for i in range(k, n)])) / matrix[k][k]

    print(['{:.4f}'.format(x[i]) for i in range(n)])
 
def main_element(matrix,k):
    buf = k + numpy.argmax(numpy.abs([k-1, k]))
    if k != buf:
        matrix[k,-1], matrix[buf,-1] = numpy.copy(matrix[buf,-1]),numpy.copy(matrix[k,-1])
        
Gauss_Column(A)
print('\n')
Gauss_Main(A)

# ['0.8580', '0.8762', '0.6145', '0.6730', '0.7988'] - Gauss(A)


