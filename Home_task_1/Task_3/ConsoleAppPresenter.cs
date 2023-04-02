using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FindCutInCube
{
    public static class ConsoleAppPresenter
    {
        public static void RunExample()
        {
            CutInCube cube = new();
            bool[,,] mockMatrix = new bool[,,]
            {
                {
                    {false, true, true},
                    {true, false, true},
                    {true, true, false},
                },
                {
                    {true, true, true},
                    {true, true, true},
                    {true, true, false},
                },
                {
                    {true, true, false},
                    {false, false, false},
                    {true, true, false},
                },
            };

            while (true)
            {
                Console.Write("Please choose an action:" +
                    "\n 1. Set mock cube;" +
                    "\n 2. Randomize cube." +
                    "\n -> ");
                bool tryReadAction = byte.TryParse(Console.ReadLine(), out byte choosenAction);
                if (!tryReadAction || choosenAction < 1 || choosenAction > 2)
                {
                    Console.WriteLine("You have entered the wrong data!\n");
                    continue;
                }
                if (choosenAction == 1)
                {
                    cube.SetMockCube(mockMatrix);
                }
                else
                {
                    cube = new CutInCube(4);
                    cube.RandomizeMatrix();
                }


                Console.WriteLine(cube);

                bool isDiagonals = cube.FindDiagonals(out int[] startCoords, out int[] endCoords);
                bool isHorizontalOrVertical = cube.FindHorizaontalsAndVerticals(out int[] startCoords2, out int[] endCoords2);

                if (isDiagonals)
                {
                    Console.WriteLine($"The cut is diagonal: {isDiagonals}, " +
                    $"\nstarts at: [{startCoords[0]},{startCoords[1]},{startCoords[2]}] " +
                    $"ends at: [{endCoords[0]},{endCoords[1]},{endCoords[2]}]");
                }
                if (isHorizontalOrVertical)
                {
                    Console.WriteLine($"The cut is vertical or horizontal: {isHorizontalOrVertical}, " +
                    $"\nstarts at: [{startCoords2[0]},{startCoords2[1]},{startCoords2[2]}] " +
                    $"ends at: [{endCoords2[0]},{endCoords2[1]},{endCoords2[2]}]");
                }

                ConsoleKey consoleKey;
                do
                {
                    Console.WriteLine(@"Repeat program? press Y\N on your keyboard: ");
                    consoleKey = Console.ReadKey(true).Key;
                }
                while (consoleKey != ConsoleKey.Y && consoleKey != ConsoleKey.N);
                if(consoleKey == ConsoleKey.N) break;
            }
        }
    }
}
