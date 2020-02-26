using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;
using System.Threading;

namespace project3
{
    class Program
    {
        static void Main(string[] args)
        {
            //Выполнение 2 пункта

            Console.Write("Введите строку :");
            string f = Console.ReadLine();
            string[] slova = f.Split(' ',',','.','!');//разбиваем строку на слова
            for (int i = slova.Length - 1; i >= 0; i--)//у каждого массива в c# есть метод .Length(этот метод возвращает кол-во элементов с отсчетом от 1,здесь же нужно от 0,т.к массив)
              {
                Console.Write("{0} ",slova[i]);
              }
            Console.Write("\n");


            //Выполнение 7 пункта

            Console.Write("Введите строку :");
            string str = Console.ReadLine();
            int a = str.Length;
              for(int i=1;i<=a;i++)
                {
                Console.Write("{0:X4} ",i);
                }
            Console.WriteLine("\n");

            //Выполнение 8 пункта

            Console.WriteLine("Введите язык в формате: es-английский,fr-французский,ru-русский,ch-китайский,jp-японский и т.д.");
            string languange = Console.ReadLine();
            Thread.CurrentThread.CurrentCulture = new CultureInfo(languange);
            DateTime now = new DateTime(2020,1,1);

              for(int i=1;i<=12;i++)
                {                 
                    Console.WriteLine(now.ToString("MMMM"));
                    now = now.AddMonths(1);
                }
            
            
            Console.ReadKey();
        }
    }
}
