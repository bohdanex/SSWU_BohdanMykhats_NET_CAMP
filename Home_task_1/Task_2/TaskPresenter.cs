using static System.Console;

namespace LongestHorizontalLine
{
    public static class TaskPresenter
    {
        public static void ExecuteTask()
        {

            //Set the matrix height and width
            MatrixLineLength mtl = new(4, 4);
            //Fill matrix with random values 'FillWithRandom()' or mock 'SetMockMatrix()' values
            mtl.FillWithRandom(15);
            //return matrix as string by calling ToString() method;
            WriteLine(mtl);

            WriteLine(
                $"Longest line width: {mtl.FindLongestLine(out HalfByte colour, out int[] x1, out int y)}" +
                $", range {x1[0]} - {x1[1]}, y={y}," +
                $" colour={Enum.GetName(typeof(Colours), colour.ToInt32())}({colour})");

            while (true)
            {
                Write(
                    "\n1 - Fill the matrix with random elements;" +
                    "\n2 - Use mock matrix." +
                    "\nChoose operation (type 1 or 2): ");

                bool isValidCommand = byte.TryParse(ReadLine(), out byte selectedCommand);
                if (!isValidCommand && selectedCommand < 1 && selectedCommand > 2)
                {
                    WriteLine("Invalid command");
                    continue;
                }
                //Fill the matrix with random elements
                if (selectedCommand == 1)
                {
                    while (true)
                    {
                        Write("Enter matrix width: ");
                        bool isValidWidth = int.TryParse(ReadLine(), out int matrixWidth);
                        if (!isValidWidth)
                        {
                            WriteLine("Invalid data");
                            continue;
                        }

                        Write("Matrix height: ");
                        bool isValidHeight = int.TryParse(ReadLine(), out int matrixHeight);
                        if (!isValidHeight)
                        {
                            WriteLine("Invalid data");
                            continue;
                        }
                        mtl = new MatrixLineLength(matrixHeight, matrixWidth);
                        break;
                    }

                    while (true)
                    {
                        Write("Maximum random value(0-15): ");
                        bool isValidRandomValue = int.TryParse(ReadLine(), out int matrixRandomValue);
                        if (!isValidRandomValue)
                        {
                            WriteLine("Enter value between 0 and 15");
                            continue;
                        }
                        mtl.FillWithRandom(matrixRandomValue);
                        break;
                    }
                }
                else
                {
                    mtl.SetMockMatrix();
                }

                WriteLine(mtl);
                WriteLine(
                $"Longest line width: {mtl.FindLongestLine(out colour, out x1, out y)}" +
                $", range {x1[0]} - {x1[1]}, y={y}," +
                $" colour={Enum.GetName(typeof(Colours), colour.ToInt32())}({colour})");

                ConsoleKey pressedKey = ConsoleKey.C;
                while (pressedKey != ConsoleKey.Y && pressedKey != ConsoleKey.N)
                {
                    Write("Do you want to repeat action?\n" +
                        "Press a key on your keyboard (Y - yes, N - no): ");
                    pressedKey = ReadKey().Key;
                }

                if (pressedKey == ConsoleKey.N) break;
            }
        }
    }
}
