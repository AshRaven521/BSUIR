using MathNet.Numerics.LinearAlgebra;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Lr4
{
    class Program
    {
        static void Main(string[] args)
        {
            var vectorC = Vector<double>.Build.DenseOfArray(new double[] { -4, -3, -7, 0, 0 });
            var matrixA = Matrix<double>.Build.DenseOfArray(new double[,] { { -2, -1, -4, 1, 0 },
                                                                            { -2, -2, -2, 0, 1} });
            var vectorB = Vector<double>.Build.DenseOfArray(new double[] { -1, -1.5 });
            var basis = Vector<double>.Build.DenseOfArray(new double[] { 3, 4 });

            var res = SimplexMethod(matrixA, vectorB, vectorC, basis);
            Console.WriteLine(res);
        }

        static Vector<double> SimplexMethod(Matrix<double> matrixA, Vector<double> vectorB, Vector<double> vectorC, Vector<double> basis)
        {
            var matrixAb = Matrix<double>.Build.DenseOfColumnVectors(basis.Select(j => matrixA.Column((int)j)));

            var invertedMatrix = matrixAb.Inverse();

            var vectorCb = Vector<double>.Build.DenseOfEnumerable(basis.Select(j => vectorC.ElementAt((int)j)));

            var vectorY = vectorCb * invertedMatrix;

            var kappaB = invertedMatrix * vectorB;

            var kappa = Vector<double>.Build.DenseOfArray(new double[vectorC.Count]);

            for (int i = 0; i < basis.Count; i++)
            {
                var j = (int)basis[i];
                kappa[j] = kappaB[i];
            }

            if (kappa.All(k => k >= 0))
            {
                return kappa;
            }

            var kappaIndex = Array.IndexOf(kappa.ToArray(), kappa.FirstOrDefault(k => k < 0));

            var index = Array.IndexOf(basis.ToArray(), basis.FirstOrDefault(j => j == kappaIndex));

            var deltaY = invertedMatrix.Row(0);

            var array = new double[vectorC.Count];
            for (int i = 0; i < array.Length; i++)
            {
                array[i] = i;
            }
            var notBasis = Vector<double>.Build.DenseOfEnumerable(array.Where(j => !basis.Contains(j)));

            var mu = notBasis.Select(j => deltaY * matrixA.Column((int)j)).ToArray();

            if (mu.All(m => m >= 0))
            {
                throw new Exception("Задача не совместима");
            }

            var sigmaList = new List<double>();

            for (int i = 0; i < notBasis.Count; i++)
            {
                sigmaList.Add((vectorC[i] - matrixA.Column(i) * vectorY) / mu[i]);
            }
            var sigma = Vector<double>.Build.DenseOfEnumerable(sigmaList);

            var newComp = Array.IndexOf(sigma.ToArray(), sigma.FirstOrDefault(s => s == sigma.Min()));

            basis[index] = newComp;

            return SimplexMethod(matrixA, vectorB, vectorC, basis);
        }
    }
}
