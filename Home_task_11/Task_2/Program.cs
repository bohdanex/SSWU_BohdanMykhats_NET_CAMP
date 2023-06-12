using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection.PortableExecutable;

namespace Task_2
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            Random rnd = new Random();
            int[] ints = new int[100].Select(n => rnd.Next(100)).ToArray();

            string path = Path.Combine(@"C:\DevTools\vs_projects\SSWU_BohdanMykhats_NET_CAMP\Home_task_11\Task_2",
                "root", "mainSequence.txt");

            MergeSorter.GenerateFile(path, ints);
            MergeSorter.SplitByMaxSize(path);
            MergeSorter.MergeSortToFile(path);
        }
    }
}