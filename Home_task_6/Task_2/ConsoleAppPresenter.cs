namespace Task_2
{
    internal static class ConsoleAppPresenter
    {
        public static void RunExample()
        {
            int[] ints1 = new int[] { 1, 2, 0, 5 };
            int[] ints2 = new int[] { 8, 4, 4, 4, 2, 1, -8, 5 };
            int[] ints3 = new int[] { 112, 0, -2, -11, 8, 3, 12, 12, 44, 12, 2 };

            IEnumerable<int> enumerable = SortedArrays.Sort(ints1, ints2, ints3);
            IEnumerator<int> enumerator = enumerable.GetEnumerator();

            while (enumerator.MoveNext())
            {
                Console.WriteLine(enumerator.Current);
            }
        }

        public static void RunUserApp()
        {
            List<int[]> listOfInts = new();
            string? readInfo = string.Empty;
            ConsoleKey createNewArrayKey = ConsoleKey.Y;

            while(createNewArrayKey is ConsoleKey.Y)
            {
                Console.Write("Please enter the numbers which are space separated: ");
                readInfo = Console.ReadLine();

                if(readInfo is null)
                {
                    Console.WriteLine("Can not be null!");
                    continue;
                }

                bool dataIsValid = true;
                int[] ints = Array.ConvertAll<string, int>(
                    readInfo.Split(' ', StringSplitOptions.RemoveEmptyEntries),
                    delegate(string text)
                    {
                        if(int.TryParse(text, out int result))
                        {
                            return result;
                        }
                        else
                        {
                            Console.WriteLine("Wrong data format");
                            dataIsValid= false;
                            return 0;
                        }
                    });

                if (!dataIsValid)
                {
                    continue;
                }
                listOfInts.Add(ints);

                Console.Write("Add a new array? (Y - yes/Any key - no): ");
                createNewArrayKey = Console.ReadKey(true).Key;
                Console.WriteLine();
            }

            
            foreach (int item in SortedArrays.Sort(listOfInts.ToArray()))
            {
                Console.WriteLine(item);
            }
        }
    }
}
