using MathNet.Numerics.LinearAlgebra;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Lr3
{
    class Program
    {
        static void Main(string[] args)
        {
            var basis = Vector<double>.Build.DenseOfArray(new double[] { 0 });
            var matrixA = Matrix<double>.Build.DenseOfArray(new double[,] { { -1, -1, -1},
                                                                            { -2, -2, -2}});
            var vectorB = Vector<double>.Build.DenseOfArray(new double[] { -1, 0 });


            var (vectorX, vectorBResult) = StartSimplexMethod(matrixA, vectorB, basis);
            

            foreach (var arr in vectorX)
            {
                Console.WriteLine($"Элемент вектора X: {arr}");
            }
            Console.WriteLine();
            foreach (var v in vectorBResult)
            {
                Console.WriteLine($"Элемент вектора B: {v}");

            }
            
        }

        private static Tuple<ArraySegment<double>, List<double>> StartSimplexMethod(Matrix<double> matrixA, Vector<double> vectorB, Vector<double> basis)
        {
            var rowsCount = matrixA.RowCount;
            var columnsCount = matrixA.ColumnCount;


            var zeros = Vector<double>.Build.Dense(columnsCount, 0);
            var identityMatrix = Matrix<double>.Build.DenseIdentity(2, 2);

            var ones = Vector<double>.Build.Dense(rowsCount, 1);
            for (int i = 0; i < vectorB.Count; i++)
            {
                if (vectorB[i] < 0)
                {
                    vectorB[i] *= -1;
                    for (int j=0; j < matrixA.ColumnCount; j++)
                    {
                        matrixA[i, j] *= -1;
                    }
                }

            }
            var newMatrix = matrixA.Append(identityMatrix);
            
            var vectorJb = Vector<double>.Build.DenseOfEnumerable(ones.Select((z, i) => columnsCount + z + i));
            var vectorC = Vector<double>.Build.DenseOfEnumerable(zeros.Concat(ones.Negate()));
            var vectorX = Vector<double>.Build.DenseOfEnumerable(zeros.Concat(vectorB));

            var (resX, resB) = MainSimplexMethod(newMatrix, vectorC, vectorX, vectorJb);

            var subResX = resX.SubVector(columnsCount, resX.Count - columnsCount);


            if (subResX.Any(x => x != 0))
            {
                throw new Exception("Задача не совместима");
            }


            //Находим индекс положительного элемента
            int k = 0;
            for (int i = 0; i < resB.Count; i++)
            {
                if (resB.ElementAt(i) > 0)
                {
                    k = i;
                }
            }


            var array = new double[columnsCount - basis.Count];
            for (int i = 0; i < columnsCount - 1; i++)
            {
                array[i] = i + 1;
            }
            var vectorJnb = Vector<double>.Build.DenseOfEnumerable(array.Where(x => !basis.Contains(x)));


            var test = Matrix<double>.Build.DenseOfColumns(resB.Select(j => newMatrix.Column((int)j - 1).ToArray()));
            var invertedAb = test.Inverse();
            var vectorL = vectorJnb.Select(j => invertedAb.Multiply(newMatrix.Column((int)j)).ToArray()).ToArray();
           

            var vectorBNew = new List<double>();
            var vectorCNew = new List<double>();
            var vectorJbNew = new List<double>();

            if (vectorL.All(x => x[k] == 0))
            {
                matrixA = matrixA.RemoveRow(k);
               
                vectorJbNew = vectorJb.ToList();
                vectorJbNew.RemoveAt(k);

                vectorCNew = vectorC.ToList();
                vectorCNew.RemoveAt(k);

                vectorBNew = vectorB.ToList();
                vectorBNew.RemoveAt(k);
                
                newMatrix = newMatrix.RemoveColumn(k);
                
                

            }

            //выбираем элементы из resX по позициям от 0 до columnsCount
            var res = new ArraySegment<double>(resX.ToArray(), 0, columnsCount);


            return Tuple.Create(res, vectorJbNew);
        }

        //Методы из ЛР №2(изменено относительно предыдущего проекта)
        private static Tuple<Vector<double>, Vector<double>> MainSimplexMethod(Matrix<double> matrixA, Vector<double> vectorC, Vector<double> vectorX, Vector<double> vectorB)
        {
            int replaceIndex = 0;
            //Бесконечный цикл, но с итерациями
            for (int i = 0; ; i++)
            {
                var basisMatrix = Matrix<double>.Build.DenseOfColumnArrays(vectorB.Select(j => matrixA.Column((int)j - 1).ToArray()).ToArray());
                
                var invertMatrix = Matrix<double>.Build.Dense(basisMatrix.RowCount, matrixA.RowCount);
                
                var testvector = Vector<double>.Build.DenseOfEnumerable(matrixA.Column(replaceIndex));
                
                if (i != 0)
                {
                    invertMatrix = GetInverseMatrix(basisMatrix, testvector, replaceIndex);
                    
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

                int j0 = -1;
                for (int j = 0; j < gradeVector.Count; j++)
                {
                    if(gradeVector[j] < 0)
                    {
                        j0 = j;
                        break;
                    }
                }
                
                
                if (j0 == -1)
                {
                    return Tuple.Create(vectorX, vectorB);
                }

                var vectorZ = invertMatrix.Multiply(matrixA.Column(j0));
                var vectorT = vectorZ.Select((z, i) => z > 0 ? vectorX[(int)vectorB[i] - 1] / z : Double.PositiveInfinity).ToArray();
                var tetta = vectorT.Min();

                if (tetta == Double.PositiveInfinity)
                {
                    throw new Exception("Функция не ограничена");
                }
                replaceIndex = Array.IndexOf(vectorT, tetta);

                for (int j = 0; j < vectorB.Count; j++)
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
