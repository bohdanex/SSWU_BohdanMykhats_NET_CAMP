﻿namespace Task_2
{
    public static class SortedArrays
    {
        public static IEnumerable<int> Sort(params int[][] arraysOfIntegers)
        {
            //int[][] sourceArray = (int[][])arraysOfIntegers.Clone(); Варіант із клонування займе лишню пам'ять
            int[][] sourceArray = arraysOfIntegers;
            const int intSize = sizeof(int);
            int[] destArray = Array.Empty<int>();
// краще відразу виділити сумарну пам'ять під результат, а не використовувати метод Resize на кожному кроці.
            foreach (int[] array in sourceArray)
            {
                int destArraysSize = destArray.Length;
                Array.Resize(ref destArray, destArraysSize + array.Length);
                Buffer.BlockCopy(array, 0, destArray, destArraysSize * intSize, array.Length * intSize);
            }

            Array.Sort(destArray);
// про ефективність на занятті!
            foreach (int item in destArray)
            {
                yield return item;
            }
        }
    }
}
