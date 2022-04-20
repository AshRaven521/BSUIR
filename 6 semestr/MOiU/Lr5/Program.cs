using MathNet.Numerics.LinearAlgebra;
using System;
using System.Collections.Generic;
using System.Linq;
using Force.DeepCloner;

namespace Lr5
{
    class Program
    {
        private static Vector<double> vectorA = Vector<double>.Build.DenseOfArray(new double[] { 0, 0, 0 });
        private static Vector<double> vectorB = Vector<double>.Build.DenseOfArray(new double[] { 0, 0, 0 });
        private static int m = vectorA.Count;
        private static int n = vectorB.Count;
        private static List<BasisCell> cells = new List<BasisCell>();

        static void Main(string[] args)
        {
            cells.Add(new BasisCell(0, 0));
            cells.Add(new BasisCell(0, 1));
            cells.Add(new BasisCell(0, 2));
            cells.Add(new BasisCell(1, 0));
            cells.Add(new BasisCell(1, 1));
            cells.Add(new BasisCell(1, 2));
            cells.Add(new BasisCell(2, 0));
            cells.Add(new BasisCell(2, 1));
            cells.Add(new BasisCell(2, 2));
            var matrixC = Matrix<double>.Build.DenseOfArray(new double[,]
            {
                {8, 4, 1 },
                {8, 4, 3 },
                {9, 7, 5 }
            });
            var (matrix, basis) = FirstPhase(vectorA, vectorB);
            var optimalPlan = SecondPhase(matrixC, matrix, basis);
            Console.WriteLine($"Оптимальный план : {optimalPlan}");
        }

        private static Tuple<Matrix<double>, List<BasisCell>> FirstPhase(Vector<double> vectorA, Vector<double> vectorB)
        {
            var basis = new List<BasisCell>();

            var matrix = Matrix<double>.Build.DenseOfArray(new double[m, n]);

            for (int i = 0, j = 0; i < m && j < n;)
            {
                double value = Math.Min(vectorA[i], vectorB[j]);

                vectorA[i] -= value;
                vectorB[j] -= value;
                matrix[i, j] = value;

                basis.Add(new BasisCell(i, j));


                if (vectorA[i] == 0)
                {
                    i++;
                }
                else if (vectorB[j] == 0)
                {
                    j++;
                }

                if (i == m)
                {
                    for (i--, j++; j < n; j++)
                    {
                        basis.Add(new BasisCell(i, j));
                    }
                }
                if (j == n)
                {
                    for (i++, j--; i < m; i++)
                    {
                        basis.Add(new BasisCell(i, j));
                    }
                }
            }

            return Tuple.Create(matrix, basis);
        }

        private static void ClearBasis(List<BasisCell> basis, Func<BasisCell, int> predicate)
        {
            var testArray = basis.GroupBy(predicate).Where(arr => arr.Count() == 1);
            foreach (var arr in testArray)
            {
                basis.RemoveAll(cell => cell.Equals(arr.ElementAt(0)));
            }
        }

        private static List<BasisCell> PolarizeBasis(List<BasisCell> basis, BasisCell cell, bool sign = true)
        {
            if (cell != null && !cell.Sign)
            {
                cell.Sign = sign;
            }
            else
            {
                return basis;
            }

            var rowCell = basis.FirstOrDefault(x => x.i == cell.i && x.j != cell.j);
            var columnCell = basis.FirstOrDefault(x => x.i != cell.i && x.j == cell.j);
            PolarizeBasis(basis, rowCell, !sign);
            return PolarizeBasis(basis, columnCell, !sign);
        }

        private static Matrix<double> SecondPhase(Matrix<double> matrixC, Matrix<double> matrix, List<BasisCell> basis)
        {
            var b = Vector<double>.Build.DenseOfArray(new double[m + n]);
            var matrixUV = Matrix<double>.Build.DenseOfArray(new double[m + n, m + n]);

            for (int index = 0; index < basis.Count; index++)
            {
                var i = basis[index].i;
                var j = basis[index].j;
                b[index] = matrixC[i, j];
                matrixUV[index, i] = 1;
                matrixUV[index, j + m] = 1;
            }

            matrixUV[n + m - 1, 0] = 1;
            var solution = matrixUV.Solve(b);

            var u = solution.Take(m);
            var v = solution.Skip(m);

            List<BasisCell> notBasis = new List<BasisCell>();

            for (int i = 0; i < cells.Count; i++)
            {
                bool bBa = false;
                for (int r = 0; r < basis.Count; r++)
                {
                    if (cells.ElementAt(i).i == basis.ElementAt(r).i && cells.ElementAt(i).j == basis.ElementAt(r).j)
                    {
                        bBa = true;
                    }
                }
                if (!bBa)
                {
                    notBasis.Add(cells.ElementAt(i));
                }
            }

            var cell = notBasis.FirstOrDefault(x => u.ElementAt(x.i) + v.ElementAt(x.j) > matrixC[x.i, x.j]);
            if (cell == null)
            {
                return matrix;
            }
            basis.Add(cell);

            var basisCopy = basis.DeepClone();
            ClearBasis(basisCopy, x => x.i);
            ClearBasis(basisCopy, x => x.j);

            var polarizedBasis = PolarizeBasis(basisCopy, cell.DeepClone());

            var tetta = polarizedBasis.Select(cell => !cell.Sign ? matrix[cell.i, cell.j] : Double.PositiveInfinity).Min();

            polarizedBasis.ForEach(cell => matrix[cell.i, cell.j] += cell.Sign ? tetta : -tetta);

            foreach (var pb in polarizedBasis)
            {
                if (matrix[pb.i, pb.j] == 0 && !pb.Sign)
                {
                    basis.RemoveAll(cell => cell.i.Equals(pb.i) && cell.j.Equals(pb.j));
                    break;
                }
            }

            return SecondPhase(matrixC, matrix, basis);
        }

    }
}
