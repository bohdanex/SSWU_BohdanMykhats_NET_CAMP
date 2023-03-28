using System.Data;

namespace FindCutInCube
{
    internal class Program
    {
        static void Main(string[] args)
        {
            CutInCube cube = new();
            cube.SetMockMatrix();
            cube.CheckDiagonals();
        }
    }

    public class CutInCube
    {
        private bool[,,] _cube;
        private bool diag1 = false;
        private bool diag2 = false;

        public CutInCube()
        {
            _cube = new bool[0,0,0];
        }

        public CutInCube(int cubeSize)
        {
            _cube = new bool[cubeSize,cubeSize,cubeSize];
        }

        public void SetMockMatrix()
        {
            _cube = new bool[,,]
            {
                {
                    {false,true,true,true},
                    {true,false,true,true},
                    {true,true,false,true},
                    {true,true,true,false},
                },
                {
                    {false,true,true,false},
                    {true,false,false,true},
                    {true,false,false,true},
                    {false,true,true,false },
                },
                {
                    {true,true,true,true},
                    {true,true,true,true},
                    {true,true,true,true},
                    {true,true,true,true },
                },
                {
                    {true,true,true,true},
                    {true,true,true,true},
                    {true,true,true,true},
                    {true,true,true,true },
                },
            };
        }

        public bool FindHole()
        {
            int cubeIndexLength = _cube.GetLength(0) - 1;
            for(int z = 0; z <= cubeIndexLength; ++z)
            {
                for(int x = 0; x <= cubeIndexLength; ++x)
                {
                    for(int y = 0; y <= cubeIndexLength; ++y)
                    {

                    }
                }
            }
            return false;
        }

        public void CheckDiagonals()
        {
            int cubeIndexLength = _cube.GetLength(0) - 1;
            int[] endCoordinates = new int[3];
            int[] startCoordinates = new int[3];
            for (int z = 0; z < cubeIndexLength; ++z)
            {
                diag1 = false;
                diag2 = false;
                for(int xy = 0; xy < cubeIndexLength; ++xy)
                {
                    diag1 |= _cube[z, xy, xy] ;
                    diag2 |= _cube[z, cubeIndexLength - xy, xy];
                    //break the loop if 1 (condition = false) exists at one of diaglonales
                    if (diag2 && diag1) break;
                }
                if (!(diag1 & diag2))
                {
                    //Here we make sure, these values are different to correct output
                    diag1 ^= !diag2;
                    startCoordinates[0] = z;
                    startCoordinates[1] = cubeIndexLength * Convert.ToInt32(diag1);
                    startCoordinates[2] = cubeIndexLength * Convert.ToInt32(diag1);
                    endCoordinates[0] = z;
                    endCoordinates[1] = cubeIndexLength * Convert.ToInt32(diag2);
                    endCoordinates[2] = cubeIndexLength * Convert.ToInt32(diag2);
                    break;
                }
            }
        }

        public void CheckHorizontalsAndVericals()
        {
            int cubeIndexLength = _cube.GetLength(0) - 1;

        }
    }
}