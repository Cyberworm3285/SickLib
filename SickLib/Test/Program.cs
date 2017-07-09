using System;
using SickLib.Collections.SickList;

using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;

namespace Test
{
    internal class Program
    {
        private static void Main()
        {
            var slist = new SickList<int>();
            var list = new List<int>();
            var rand = new Random();

            var timer = new Stopwatch();
            timer.Start();
            for (var i = 0; i < 100000; i++)
            {
                list.Insert(rand.Next(0, i),i);
            }
            timer.Stop();
            Console.WriteLine(timer.ElapsedMilliseconds);
            timer.Reset();
            timer.Start();
            for (var i = 0; i < 100000; i++)
            {
                slist.Insert(rand.Next(0, i), i);
            }
            timer.Stop();
            Console.WriteLine(timer.ElapsedMilliseconds);


            int count = 0;
            int sCount = 0;
            try
            {
                for (;;)
                {
                    list.Add(int.MaxValue);
                    count++;
                }
            }
            catch
            {
                Console.WriteLine( count );
            }
            try
            {
                for (; sCount <= count; ++sCount)
                {
                    slist.Add(int.MaxValue);
                    count++;
                }
            }
            catch
            {
                Console.WriteLine(count);
            }

            Console.ReadKey();
        }
    }
}