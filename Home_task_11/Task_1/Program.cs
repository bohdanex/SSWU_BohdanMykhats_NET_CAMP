using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace SSWU.HomeTask11
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int[] ints = { 2,462,1,3,5,742,9,6,8,654,2,0,-15,2,7,1,-5123 };
            ints.QuickSort(0).Print();

            "hello world a jkl bc asdasfq".Replace(" ", "").ToList().QuickSort(4).Print();
        }
    }

    public static class EnumerablePrinter
    {
        public static void Print<T>(this IEnumerable<T> collection)
        {
            foreach (var item in collection)
            {
                Console.Write(item + " | ");
            }
            Console.WriteLine();
        }
    }
}