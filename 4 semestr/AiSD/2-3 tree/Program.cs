using System;
using System.Diagnostics;

namespace proj3 
{
    class Program 
    {
        static void Main(string[] args) {
            Tree234 tree = new Tree234();

            /*var items = new int[] {1, 12, 8, 2, 25, 6, 14, 28, 17, 7, 52, 16, 48, 68, 3, 26, 29, 53, 55, 45};
            foreach (int item in items) 
            {
                tree.Insert(item); 
            }
            Console.WriteLine(tree);
            Console.WriteLine("***********************************");

            items = new int[] { 3, 17, 55, 1, 52, 48, 2, 14, 25 };
            foreach (int item in items) 
            {
                tree.Delete(item);
                Console.WriteLine(tree);
            }*/
            Stopwatch clock = new Stopwatch();
            clock.Start();
            for(int i = 0; i < Int32.MaxValue; i++)
            {
                tree.Insert(i);
            }

            clock.Stop();
            Console.WriteLine(clock.ElapsedMilliseconds);
            //Console.WriteLine(tree);
            Console.ReadKey();
        }
    }
}
