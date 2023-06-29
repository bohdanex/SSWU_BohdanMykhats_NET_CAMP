using System;
using System.Collections.Generic;
using System.Linq;

namespace SSWU.HomeTask11
{
    public static class QuickSorter
    {
        public static IEnumerable<T> QuickSort<T>(this IList<T> collection, int pivot) where T : IComparable<T>
        {
            if(pivot < 0)
            {
                throw new ArgumentOutOfRangeException("Wrong pivot");
            }

            if (collection.Count <= 1) return collection;

            int i = -1;
            int j = 0;
            //поміняти місцями якір і останній елемент колекції
            (collection[^1], collection[pivot]) = (collection[pivot], collection[^1]);

            int destination = collection.Count - 1;
            while(j < destination)
            {
                if (collection[j].CompareTo(collection[^1]) < 0)
                {
                    ++i;
                    //якщо елемент менший від якоря, то поміняти arr[i] та arr[j] місцями
                    (collection[i], collection[j]) = (collection[j], collection[i]);
                }
                ++j;
            }
            //ставимо якір на місце, у такому випадку елементи менші від якоря знаходяться ліворуч, більші - праворуч
            ++i;
            (collection[i], collection[^1]) = (collection[^1], collection[i]);

            if (collection.Count <= 2) return collection;

            T[] leftArray = collection.ToArray()[..i];
            T[] rightArray = collection.ToArray()[(i + 1)..];
            
            int leftPivot = pivot == destination ? leftArray.Length - 1 :
                                pivot == 0 ? pivot :
                                    leftArray.Length / 2;

            int right = pivot == destination ? rightArray.Length - 1 :
                                pivot == 0 ? pivot :
                                    rightArray.Length / 2;

            return leftArray.QuickSort(leftPivot)
                .Concat(rightArray.QuickSort(right / 2).Prepend(collection[i]));
        }
    }
}
