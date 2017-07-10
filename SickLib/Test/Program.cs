using System;
using System.Linq;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;

using SickLib.Collections.SickLinkedList;
using SickLib.Collections.Trees.SickTree;
using static SickLib.LINQ_Extensions.TypeConverter;

namespace Test
{
    internal class Program
    {
        private static void Main()
        {
            B();

            Console.ReadKey();
        }

        private static void A()
        {
            var slist = new SickLinkedList<int>();
            var list = new List<int>();
            var rand = new Random();

            var timer = new Stopwatch();
            timer.Start();
            for (var i = 0; i < 100000; i++)
            {
                list.Insert(rand.Next(0, i), i);
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
                Console.WriteLine(count);
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
        }

        private static void B()
        {
            SickTree<int, int> tree = new SickTree<int, int>();

            tree.AddPath(1, new int[] { 1, 4 });
            tree.AddPath(2, new int[] { 1, 4, 2 });
            tree.AddPath(3, new int[] { 1, 4, 8 });
            tree.AddPath(4, new int[] { 1 }, true);
            tree.AddPath(5, new int[0]);

            Tuple<string, int[]>[] convTest = new Tuple<string, int[]>[]
            {
                Tuple.Create("eins", new int[]{ 1, 4, 5 }),
                Tuple.Create("zwei", new int[]{ 1, 4, 10 }),
                Tuple.Create("drei", new int[]{ 1, 2 })
            };

            tree.ToList().ForEach(Console.WriteLine);
            var tree2 = convTest.ToSickTree(
                t => t.Item1,
                t => t.Item2
                );

        }
    }
}