using System;
using System.Linq;
using MathNet.Numerics.LinearAlgebra;

namespace Lr6
{
    class Program
    {
        static void Main(string[] args)
        {
            var vectorC = Vector<double>.Build.DenseOfArray(new double[] { -8, -6, -4, -6 });
            var vectorB = Vector<double>.Build.DenseOfArray(new double[] { 2, 3 });
            var vectorX = Vector<double>.Build.DenseOfArray(new double[] { 2, 3, 0, 0 });
            var matrixA = Matrix<double>.Build.DenseOfArray(new double[,]
            {
                {1, 0, 2, 1},
                {0, 1, -1, 2}
            });
            var matrixD = Matrix<double>.Build.DenseOfArray(new double[,]
            {
                {2, 1, 1, 0},
                {1, 1, 0, 0},
                {1, 0, 1, 0},
                {0, 0, 0, 0}
            });
            var supportConstraints = Vector<double>.Build.DenseOfArray(new double[] { 0, 1 });
            var supportConstraintsExtended = Vector<double>.Build.DenseOfArray(new double[] { 0, 1 });
            var optimalPlan = QuadraticProgramming(vectorC, vectorX, matrixA, matrixD, supportConstraints, supportConstraintsExtended);
            Console.WriteLine($"Оптимальный план: {optimalPlan}");
        }

        private static Vector<double> QuadraticProgramming(Vector<double> vectorC, Vector<double> vectorX, Matrix<double> matrixA,
                                         Matrix<double> matrixD, Vector<double> supportConstraints, Vector<double> supportConstraintsExtended)
        {
            var matrixAb = Matrix<double>.Build.DenseOfColumns(supportConstraints.Select(j => matrixA.Column((int)j)));
            var invertedMatrixAb = matrixAb.Inverse();
            var vectorCx = vectorC + (vectorX * matrixD);
            var vectorCbx = Vector<double>.Build.DenseOfEnumerable(supportConstraints.Select(j => -vectorCx[(int)j]));
            var vectorUx = vectorCbx * invertedMatrixAb;
            var deltaX = (vectorUx * matrixA) + vectorCx;

            var j0 = deltaX.ToList().FindIndex(x => x < 0 && !supportConstraintsExtended.Contains(x));
            if (j0 == -1)
            {
                return vectorX;
            }

            var vectorL = Vector<double>.Build.DenseOfArray(new double[vectorX.Count]);
            vectorL[j0] = 1;

            var test = matrixD.ToColumnArrays();
            var matrixDx = Matrix<double>.Build.DenseOfColumns(supportConstraintsExtended.Select(j => test[(int)j].Where((_, i) => supportConstraintsExtended.Contains(i))));
            var matrixAx = Matrix<double>.Build.DenseOfColumns(supportConstraintsExtended.Select(j => matrixA.Column((int)j)));
            var matrixAxTrasnposed = matrixAx.Transpose();

            var topMatrix = matrixDx.Append(matrixAxTrasnposed);
            int dimension = matrixDx.RowCount + matrixAx.RowCount - matrixAxTrasnposed.RowCount;
            var bottomMatrix = matrixAx.Append(Matrix<double>.Build.Dense(dimension, dimension));
            Matrix<double>[,] testH =
            {
                {topMatrix},
                {bottomMatrix }
            };
            var matrixH = Matrix<double>.Build.DenseOfMatrixArray(testH);

            var topPart = Vector<double>.Build.DenseOfEnumerable(supportConstraintsExtended.Select(j => matrixD[(int)j, j0]).ToArray());
            var bottomPart = matrixA.Column(j0);
            var vectorBxtest = Vector<double>.Build.DenseOfEnumerable(topPart.Concat(bottomPart));
            var vectorBx = vectorBxtest * -1;

            var matrixHInverse = matrixH.Inverse();

            var res = matrixHInverse * vectorBx;
            for (int i = 0; i < supportConstraintsExtended.Count; i++)
            {
                var j = (int)supportConstraintsExtended[i];
                vectorL[j] = res[j];
            }

            var delta = (vectorL * matrixD) * vectorL;
            var tettaj0 = delta == 0 ? Double.PositiveInfinity : Math.Abs(deltaX[j0]) / delta;

            double[] tettas = new double[vectorX.Count];
            for (int i = 0; i < tettas.Length; i++)
            {
                tettas[i] = Double.PositiveInfinity;
            }

            for (int i = 0; i < supportConstraintsExtended.Count; i++)
            {
                var j = (int)supportConstraintsExtended[i];
                if(vectorL[j] < 0)
                {
                    tettas[j] = -vectorX[j] / vectorL[j];
                }
                else
                {
                    tettas[j] = Double.PositiveInfinity;
                }
            }
            tettas[j0] = tettaj0;

            var q = tettas.Min();
            if(q == Double.PositiveInfinity)
            {
                throw new Exception("Целевая функция не ограничена!");
            }

            var jq = Array.IndexOf(tettas, q);
            var newVectorX = vectorX + (vectorL * q);
            var diff = supportConstraintsExtended.Except(supportConstraints);

            if(j0 == jq)
            {
                supportConstraintsExtended = Vector<double>.Build.DenseOfEnumerable(supportConstraintsExtended.Append(jq));
            }
            else if(diff.Contains(jq))
            {
                supportConstraintsExtended = Vector<double>.Build.DenseOfEnumerable(supportConstraintsExtended.Where(val => val != jq));
            }
            else
            {
                var sup = Array.IndexOf(supportConstraints.ToArray(), jq);
                int jp = 0;
                for (int i = 0; i < diff.Count(); i++)
                {
                    var j = (int)diff.ElementAt(i);
                    var t = (invertedMatrixAb * matrixA.Column(j))[sup];
                    jp = Array.IndexOf(diff.ToArray(), t);
                }
                if(jp != -1)
                {
                    supportConstraintsExtended = Vector<double>.Build.DenseOfEnumerable(supportConstraintsExtended.Where(val => val != jq));
                }
                else
                {
                    supportConstraintsExtended[sup] = j0;
                }
                if(jp != -1)
                {
                    supportConstraints[sup] = jp;
                }
                else
                {
                    supportConstraints[sup] = j0;
                }
            }

            return QuadraticProgramming(vectorC, newVectorX, matrixA, matrixD, supportConstraints, supportConstraintsExtended);
        }
    }
}
