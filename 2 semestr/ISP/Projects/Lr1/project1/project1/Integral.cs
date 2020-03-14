using System;

namespace Arifmetics
{
    class Integral
    {
        static void Main(string[] args)
        {
            double leftrectintegral = 0, rightrectintegral = 0, centralrectintegral = 0;
            double c = 0.8, d = 1.6;
            int n = 0;

            MyFuncs fun = new MyFuncs();
            Console.Write("На какое количество частей разбить отрезок интегрирования? n:");

            n= ValidateInt();


            //IL-интеграл,рассчитанный методом левых прямоугольнков
            leftrectintegral = fun.LeftRectangle(c, d, n);
            Console.WriteLine("Интеграл равен {0:f} ", leftrectintegral);
            

            Console.WriteLine("");
            Console.WriteLine("На какое количество частей разбить отрезок интегрирования? n:");
            n= ValidateInt();
            rightrectintegral = fun.RightRectangle(c, d, n);
            Console.WriteLine("Интеграл равен {0:f} ", rightrectintegral);
            Console.WriteLine("");

            Console.WriteLine("На какое количество частей разбить отрезок интегрирования? n:");
            n= ValidateInt();
            centralrectintegral = fun.CentralRectangle(c, d, n);
            Console.WriteLine("Интеграл равен {0:f} ", centralrectintegral);
            Console.WriteLine("");

            Console.ReadKey();
        }

        public static int ValidateInt()
        {
            int a;

            while (!int.TryParse(Console.ReadLine(), out a) || a<=0)
            {
                Console.Write("Неправильный ввод, введите корректные данные: ");
            }
            return a;
        }
    }
}
