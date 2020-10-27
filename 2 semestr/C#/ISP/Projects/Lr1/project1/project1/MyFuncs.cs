using System;

namespace Arifmetics
{
    class MyFuncs
    {
        public double LeftRectangle(double a, double b, int m)
        {
            double I, h, Xi, f;
            int i;

            //Метод левых прямоугольников
            Console.WriteLine("Метод левых прямоугольников");
            h = (b - a) / m;
            I = 0;
            for (i = 0; i <= m - 1; i++)
            {
                Xi = a + i * h;

                //формула интеграла или приблизительный рассчет
                f = Math.Sqrt(0.3 * Math.Pow(Xi, 2) + 2.3) / (1.8 + (Math.Sqrt(2 * Xi + 1.6)));

                //выводим данные
                Console.WriteLine("X{0:d}= {1:f}", i + 1, Xi);
                Console.WriteLine("f(X{0:d})= {1:f}", i + 1, f);
                I += f;
                Console.WriteLine("I{0:d}= {1:f}", i + 1, I);
            }
            I = h * I;
            return I;
        }

        public double RightRectangle(double a, double b, int m)
        {

            double I = 0, h, Xi, f;
            int i;
            //Метод правых прямоугоьников
            Console.WriteLine("Метод правых прямоугольников");
            h = (b - a) / m;
            for (i = 1; i <= m; i++)
            {
                Xi = a + i * h;

                f = Math.Sqrt(0.3 * Math.Pow(Xi, 2) + 2.3) / (1.8 + (Math.Sqrt(2 * Xi + 1.6)));

                //выводим данные
                Console.WriteLine("X{0:d}= {1:f}", i + 1, Xi);
                Console.WriteLine("f(X{0:d})= {1:f}", i + 1, f);
                I += f;
                Console.WriteLine("I{0:d}= {1:f}", i + 1, I);

            }
            I = h * I;
            return I;
        }

        public double CentralRectangle(double a, double b, int m)
        {

            double I = 0, h, Xi, f;
            int i;
            //Метод центральных прямоугольников
            Console.WriteLine("Метод центральных прямоугольников");
            h = (b - a) / m;
            for (i = 0; i <= m - 1; i++)
            {
                Xi = a + i * h + h / 2;
                f = Math.Sqrt(0.3 * Math.Pow(Xi, 2) + 2.3) / (1.8 + (Math.Sqrt(2 * Xi + 1.6)));

                //выводим данные
                Console.WriteLine("X{0:d}= {1:f}", i + 1, Xi);
                Console.WriteLine("f(X{0:d})= {1:f}", i + 1, f);
                I += f;
                Console.WriteLine("I{0:d}= {1:f}", i + 1, I);
            }
            I = h * I;
            return I;
        }
    }
}
