using System;
using System.Collections.Generic;
using System.Linq;
using MathNet.Numerics.LinearAlgebra;

namespace Lr1
{
    class Program
    {
        static void Main(string[] args)
        {
            var matrixA = Matrix<double>.Build.DenseOfArray(new double[,] { { 3, 2, 1, 0},
                                                                            { -3, 2, 0, 1} });
            var vectorB = Vector<double>.Build.DenseOfArray(new double[] { 6, 0 });

            var result = ClippingPlaneMethod(matrixA, vectorB);

            foreach (var res in result)
            {
                Console.WriteLine($"Элемент итогового списка: {res}");
            }
        }

        private static double GetFractionPart(double num)
        {
            return num - Math.Floor(num);
        }

        private static List<double> ClippingPlaneMethod(Matrix<double> matrixA, Vector<double> vectorB)
        {
            var res = Simplex.StartSimplexMethod(matrixA, vectorB);
            var vectorX = Vector<double>.Build.DenseOfArray(res.Item1.ToArray());
            var dirtBasis = res.Item2;

            // Проверяем, все ли числа в векторе целые
            if (vectorX.All(d => Math.Abs(d % 1) <= (Double.Epsilon * 100)))
            {
                return vectorX.ToList();
            }

            var basis = dirtBasis.Select(x => x - 1);

            var bruh = new Dictionary<int, double>();
            // Заполняем словарь(ключ = i, значение = vectorX[i])
            for (int i = 0; i < vectorX.Count; i++)
            {
                bruh.Add(i, vectorX[i]);
            }

            var notBasis = bruh.Keys.Where(i => !basis.Contains(i));

            var matrixAb = Matrix<double>.Build.DenseOfColumns(basis.Select(i => matrixA.Column((int)i)));
            var matrixAn = Matrix<double>.Build.DenseOfColumns(notBasis.Select(i => matrixA.Column(i)));
            var multipleRes = matrixAb.Inverse() * matrixAn;

            // Находим индекс нецелого числа в vectorX
            int index = vectorX.ToList().FindIndex(x => !(Math.Abs(x % 1) <= (Double.Epsilon * 100)));

            double k = basis.ElementAt(index);

            var fractions = multipleRes.Row((int)k).Select(GetFractionPart);

            double[] arr = new double[vectorX.Count];

            for (int i = 0; i < arr.Length; i++)
            {
                int ind = notBasis.ToList().IndexOf(i);
                if (ind != -1)
                {
                    arr[i] = fractions.ElementAt(ind);
                }
                else
                {
                    arr[i] = 0.0;
                }
            }
            double fractionPart = GetFractionPart(vectorX[(int)k]);

            var resVector = arr.Append(-1.0).Append(fractionPart).ToList();

            return resVector;
        }
    }
}
