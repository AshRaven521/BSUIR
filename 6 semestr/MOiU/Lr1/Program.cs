using System;
using System.Collections.Generic;
using System.IO;

namespace Lr1
{
    class Program
    {
        static void Main(string[] args)
        {
            int matrixDemension = 0;
            //List<int> matrixA = new List<int>();
            double[,] matrixA;
            //List<int> reversed_matrix = new List<int>();
            double[,] reversedMatrix;
            List<double> vector = new List<double>();
            int position = 0;

            //Получение данных из файла
            using(StreamReader stream = new StreamReader("data.txt"))
            {
                matrixDemension = Convert.ToInt32(stream.ReadLine());
                matrixA = new double[matrixDemension, matrixDemension];
                reversedMatrix = new double[matrixDemension, matrixDemension];
                for(int i = 0; i < matrixDemension; i++)
                {
                    var text = stream.ReadLine().Trim();
                    var res = text.Split(' ');

                    for(int j = 0; j < matrixDemension; j++)
                    {
                        matrixA[i, j] = Convert.ToDouble(res[j]);
                    }
                }

                for (int i = 0; i < matrixDemension; i++)
                {
                    var test = stream.ReadLine().Trim().Split(' ');

                    for(int j = 0; j < matrixDemension; j++)
                    {
                        reversedMatrix[i, j] = Convert.ToDouble(test[j]);
                    }
                }


                var input = stream.ReadLine().Trim().Split(' ');
                foreach(string number in input)
                {
                    vector.Add(Convert.ToDouble(number));
                }
                position = Convert.ToInt32(stream.ReadLine());



                //Выполнение самой лабораторной
                var l = MultipleMatrixOnVector(vector, reversedMatrix);
                var tempL = l[position];

                if(tempL == 0)
                {
                    Console.WriteLine("Матрица не может быть обращена");
                    return;
                }
                Console.WriteLine("Матрица обратима");
                l[position] = -1;
                l = MultipleVectorOnConst(l, -1d / tempL);

                var identMatrix = CreateIdentityMatrix(matrixA.Length);
                identMatrix = ChangeColumn(identMatrix, l, position);

                var resMatrix = MultipleMatrixOnMatrix(identMatrix, reversedMatrix, matrixDemension);
                Printmatrix(resMatrix, matrixDemension);

                Console.ReadKey();
            }
        }

        static void Printmatrix(double[,] matrix, int dimension)
        {
            for (int i = 0; i < dimension; i++)
            {
                for (int j = 0; j < dimension; j++)
                {
                    Console.Write(matrix[i, j] );
                    Console.Write("  ");
                }
                Console.Write("\n");
            }
        }

        static void PrintVector(List<double> vector)
        {
            foreach(int i in vector)
            {
                Console.Write(i + " ");
            }
        }
        
        static List<double> MultipleMatrixOnVector(List<double> vector, double[,] matrix)
        {
            List<double> resultVector = new List<double>();

            for (int i = 0; i < vector.Count; i++)
            {
                var temp = 0.0;
                for (int j = 0; j < vector.Count; j++)
                {
                    temp += matrix[i, j] * vector[j];
                }

                //resultVector[i] = temp;
                resultVector.Add(temp);
            }

            return resultVector;
        }

        static double[,] ChangeColumn(double[,] matrix, List<double> vector, int position)
        {
            for(int i = 0; i < vector.Count; i++)
            {
                matrix[i, position] = vector[i];
            }

            return matrix;
        }

        static List<double> MultipleVectorOnConst(List<double> vector, double val)
        {
            for(int i = 0; i < vector.Count; i++)
            {
                vector[i] *= val;
            }

            return vector;
        }

        //Функция для создания единичной матрицы
        static double[,] CreateIdentityMatrix(int size)
        {
            double[,] matrix = new double[size, size];
            for(int i = 0; i < size; i++)
            {
                matrix[i, i] = 1;
            }

            return matrix;
        }

        static double[,] MultipleMatrixOnMatrix(double[,] matrix, double[,] anotherMatrix, int size)
        {
            double[,] resultMatrix = new double[size, size];

            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    for (int k = 0; k < size; k++)
                    {
                        resultMatrix[i, j] += matrix[i, k] * anotherMatrix[k, j];
                    }
                }
            }

            return resultMatrix;
        }
    }
}
