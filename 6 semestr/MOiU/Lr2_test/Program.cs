using MathNet.Numerics.LinearAlgebra;
using System;
using System.Collections.Generic;
using System.Linq;
namespace Lr2_test
{
    class Program
    {
        static void Main(string[] args)
        {
            

            var vectorC = Vector<double>.Build.DenseOfArray(new double[] { 1, 1, 0, 0, 0});
            var matrixA = Matrix<double>.Build.DenseOfArray(new double[,] { { -1, 1, 1, 0, 0},
                                                                            {1, 0, 0, 1, 0 },
                                                                            { 0, 1, 0, 0, 1} });

            var vectorX = Vector<double>.Build.DenseOfArray(new double[] { 0, 0, 1, 3, 2 });
            var vectorB = Vector<double>.Build.DenseOfArray(new double[] { 3, 4, 5 });

            try
            {
                var optimalPlan = MainSimplexMethod(matrixA, vectorC, vectorX, vectorB);
                Console.WriteLine($"Оптимальный план : {optimalPlan.Item1}");
                Console.WriteLine($"Базисные индексы : {optimalPlan.Item2}");
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private static Tuple<Vector<double>, Vector<double>> MainSimplexMethod(Matrix<double> matrixA, Vector<double> vectorC, Vector<double> vectorX, Vector<double> vectorB)
        {
            int replaceIndex = 0;
            //Бесконечный цикл, но с итерациями
            for (int i = 0; ; i++)
            {
                var basisMatrix = Matrix<double>.Build.DenseOfColumnArrays(vectorB.Select(j => matrixA.Column((int)j - 1).ToArray()).ToArray());
                var invertMatrix = Matrix<double>.Build.Dense(basisMatrix.RowCount, matrixA.RowCount);
                if (i != 0)
                {
                    invertMatrix = GetInverseMatrix(basisMatrix, vectorB, replaceIndex);
                }
                else
                {
                    invertMatrix = basisMatrix.Inverse();
                }

                var vectorCB = Vector<double>.Build.DenseOfEnumerable(vectorB.Select(j => vectorC[(int)j - 1]));
                var potentialVector = invertMatrix.Multiply(vectorCB);

                var transponseMatrixA = matrixA.Transpose();
                var test = transponseMatrixA.Multiply(potentialVector);
                
                
                var gradeVector = test.Subtract(vectorC);

                
                var number = gradeVector.FirstOrDefault(x => !vectorB.Contains(Array.IndexOf(gradeVector.ToArray(), x) + 1) && x < 0);
                var j0 = Array.IndexOf(gradeVector.ToArray(), number);
                if (j0 == -1)
                {
                    return Tuple.Create(vectorX, vectorB);
                }

                var vectorZ = invertMatrix.Multiply(matrixA.Column(j0));
                var vectorT = vectorZ.Select((z, i) => z > 0 ? vectorX[(int)vectorB[i] - 1] / z : Double.PositiveInfinity).ToArray();
                var tetta = vectorT.Min();

                if(tetta == Double.PositiveInfinity)
                {
                    throw new Exception("Функция не ограничена");
                }
                replaceIndex = Array.IndexOf(vectorT, tetta);
                
                for(int j = 0; j < vectorB.Count; j++)
                {
                    double k = vectorB[j];
                    vectorX[(int)k - 1] -= tetta * vectorZ[j];
                }

                vectorB[replaceIndex] = j0 + 1;
                vectorX[j0] = tetta;
            }
        }

        private static Matrix<double> MultipleMatrices(Matrix<double> first, Matrix<double> second, int position)
        {
            var lenght = first.ColumnCount;

            var resultMatrix = Matrix<double>.Build.Dense(lenght, lenght);
            for (int i = 0; i < lenght; i++)
            {
                for (int j = 0; j < lenght; j++)
                {
                    resultMatrix[i, j] = first[i, position] * second[position, j];
                    if (i != position)
                    {
                        resultMatrix[i, j] += second[i, j];
                    }
                }
            }

            return resultMatrix;
        }

        private static Matrix<double> GetInverseMatrix(Matrix<double> matrix, Vector<double> vector, int position)
        {
            var l = matrix.Multiply(vector);

            if (l[position] == 0)
            {
                throw new ArgumentException("Матрица не может быть обращена");
            }
            else
            {
                double num = l[position];
                l[position] = -1;
                var newVector = l.Multiply(-1d / num);
                var identityMatrix = Matrix<double>.Build.DenseIdentity(matrix.RowCount, matrix.RowCount);

                for (int i = 0; i < newVector.Count; i++)
                {
                    identityMatrix[i, position] = newVector[i];
                }

                var result = MultipleMatrices(identityMatrix, matrix, position);
                return result;
            }


        }
    }
}
