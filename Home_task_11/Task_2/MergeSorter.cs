using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Task_2
{
    public static class MergeSorter
    {
        public static int[] MergeSort(this int[] ints)
        {
            if (ints.Length <= 1) return ints;

            int length = ints.Length/2;
            return Merge(MergeSort(ints[..length]), MergeSort(ints[length..]));
        }

        private static int[] Merge(int[] leftArray, params int[] rightArray)
        {
            int[] output = new int[leftArray.Length + rightArray.Length];

            int leftSourceIndex = 0;
            int rightSourceIndex = 0;

            while (leftSourceIndex < leftArray.Length && rightSourceIndex < rightArray.Length)
            {
                if (leftArray[leftSourceIndex] < rightArray[rightSourceIndex])
                {
                    AddLeftPart();
                }
                else
                {
                    AddRightPart();
                }
            }

            while(leftSourceIndex < leftArray.Length)
            {
                AddLeftPart();
            }
            while (rightSourceIndex < rightArray.Length)
            {
                AddRightPart();
            }
            
            return output;

            void AddLeftPart()
            {
                output[leftSourceIndex + rightSourceIndex] = leftArray[leftSourceIndex];
                ++leftSourceIndex;
            }

            void AddRightPart()
            {
                output[leftSourceIndex + rightSourceIndex] = rightArray[rightSourceIndex];
                ++rightSourceIndex;
            }
        }

        public static void GenerateFile(string filePath, params int[] ints)
        {
            using TextWriter writer = new StreamWriter(filePath);

            foreach (int i in ints)
            {
                writer.WriteLine(i);
            }
        }

        public static void SplitByMaxSize(string filePath, int elementCount = 9, int maxMemorySize = 3)
        {
            using TextReader reader = new StreamReader(filePath);
            int countIndex = 0;
            string rootFolder = filePath[..^Path.GetFileName(filePath).Length];

            for (int i = 0; i <= (elementCount - 1) / maxMemorySize; ++i)
            {
                int[] ints = new int[maxMemorySize];
                if ((elementCount - (maxMemorySize * i)) < maxMemorySize)
                {
                    ints = new int[elementCount % maxMemorySize];
                }
                for (int j = 0; j < ints.Length; ++j)
                {
                    ints[j] = Convert.ToInt32(reader.ReadLine());
                    ++countIndex;
                }

                using TextWriter writer = new StreamWriter(rootFolder + $"temp_{i}.txt");
                foreach (int number in ints.MergeSort())
                {
                    writer.WriteLine(number);
                }
            }
        }

        public static void MergeSortToFile(string filePath, int elementCount = 10, int maxMemorySize = 3)
        {


            int sourceIndex = 0;
            string rootFolder = filePath[..^Path.GetFileName(filePath).Length];

            while (sourceIndex < elementCount)
            {
                int? minValue = null;
                int minValueFileIndex = 0;
                for (int i = 0; i < elementCount / maxMemorySize; ++i)
                {
                    using TextReader reader = new StreamReader(rootFolder + $"temp_{i}.txt");
                    try
                    {
                        string readData = reader.ReadLine();
                        if (string.IsNullOrEmpty(readData))
                        {
                            continue;
                        }
                        int current = Convert.ToInt32(readData);
                        if (minValue is null || current < minValue)
                        {
                            minValue = current;
                            minValueFileIndex = i;
                        }
                    }
                    catch (Exception) { }
                    finally { }
                }
                if (minValue is null)
                {
                    ++sourceIndex;
                    continue;
                }
                RemoveFirstLine(rootFolder + $"temp_{minValueFileIndex}.txt");
                WriteToFile(filePath, sourceIndex, minValue.Value);
                ++sourceIndex;
            }

            for (int i = 0; i < elementCount / maxMemorySize; ++i)
            {
                File.Delete(rootFolder + $"temp_{i}.txt");
            }
        }

        private static void RemoveFirstLine(string filePath)
        {
            string[] lines = File.ReadAllLines(filePath);
            if (lines.Length == 0) return;
            File.WriteAllLines(filePath, lines[1..]);
        }

        private static void WriteToFile(string filePath, int lineNumber, int value)
        {
            string[] lines = File.ReadAllLines(filePath);

            if (lineNumber >= 0 && lineNumber < lines.Length)
            {
                lines[lineNumber] = value.ToString();

                using (var fileStream = new FileStream(filePath, FileMode.Truncate, FileAccess.Write, FileShare.ReadWrite))
                using (var writer = new StreamWriter(fileStream))
                {
                    foreach (string line in lines)
                    {
                        writer.WriteLine(line);
                    }
                }
            }
        }
    }
}
