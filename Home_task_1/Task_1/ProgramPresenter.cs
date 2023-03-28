namespace SpiralNumbers
{
    public static class ProgramPresenter
    {
        public static void ExecuteTaskExample()
        {
            //Create an instance of class Spiral
            Spiral spiral = new();
            //Call BuildSpiral method to form a matrix
            spiral.BuildSpiral();
            //Call ToString() to return class representation
            Console.WriteLine(spiral);

            while (true)
            {
                Console.Write("Matrix width: ");
                bool isValidWidth = int.TryParse(Console.ReadLine(), out int matrixWidth);
                if (!isValidWidth)
                {
                    Console.WriteLine("Incorrect data");
                    continue;
                }
                Console.Write("Matrix height: ");
                bool isValidHeight = int.TryParse(Console.ReadLine(), out int matrixHeight);
                if (!isValidHeight)
                {
                    Console.WriteLine("Incorrect data");
                    continue;
                }
                spiral = new(matrixWidth, matrixHeight);
                spiral.BuildSpiral();
                Console.WriteLine(spiral);

                ConsoleKey pressedKey = ConsoleKey.K;
                while (pressedKey != ConsoleKey.N && pressedKey != ConsoleKey.Y)
                {
                    Console.Write("Do you wish to create a new spiral number matrix?" +
                    "\nPress Y - yes/ N - no keys on your keyboard: ");
                    pressedKey = Console.ReadKey(true).Key;
                    Console.WriteLine(pressedKey);
                }
                if (pressedKey == ConsoleKey.N) break;
            }
        }
    }
}
