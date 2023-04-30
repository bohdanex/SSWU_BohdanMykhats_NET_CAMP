namespace Task_1
{
    internal static class ConsoleAppPresenter
    {
        public static void RunExample()
        {
            try
            {
                int[,] matrix2x2 =
                {
                {1,2 },
                {3,4 },
                };

                int[,] matrix3x3 =
                {
                {1,2,3 },
                {4,5,6 },
                {7,8,9 },
                };

                int[,] matrix6x6 =
                {
                {1,3,4,10,11,21 },
                {2,5,9,12,20,22 },
                {6,8,13,19,23,30 },
                {7,14,18,24,29,31 },
                {15,17,25,28,32,35 },
                {16,26,27,33,34,36 },
                };

                ImageHandler handler = new(matrix2x2);
                Console.WriteLine(handler);
                Console.WriteLine("Result");
                foreach (var item in handler)
                {
                    Console.WriteLine(item);
                }

                Console.WriteLine();
                handler.UpdateMatrix(matrix3x3);
                Console.WriteLine(handler);
                Console.WriteLine("Result");
                foreach (var item in handler)
                {
                    Console.WriteLine(item);
                }

                Console.WriteLine();
                handler.UpdateMatrix(matrix6x6);
                Console.WriteLine(handler);
                Console.WriteLine("Result");
                foreach (var item in handler)
                {
                    Console.WriteLine(item);
                }
            }
            catch (ArgumentException argEx)
            {
                Console.WriteLine(argEx.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
