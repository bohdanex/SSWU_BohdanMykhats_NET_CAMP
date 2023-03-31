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
        private bool notDiag1 = false;
        private bool notDiag2 = false;

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
                notDiag1 = false;
                notDiag2 = false;
                for(int xy = 0; xy < cubeIndexLength; ++xy)
                {
                    notDiag1 |= _cube[z, xy, xy] ;
                    notDiag2 |= _cube[z, cubeIndexLength - xy, xy];
                    //break the loop if 1 (condition = false) exists at one of diaglonales
                    if (notDiag2 && notDiag1) break;
                }
                if (!(notDiag1 & notDiag2))
                {
                    //Here we make sure, these values are different to correct output
                    notDiag1 ^= !notDiag2;
                    startCoordinates[0] = z;
                    startCoordinates[1] = cubeIndexLength * Convert.ToInt32(notDiag1);
                    startCoordinates[2] = cubeIndexLength * Convert.ToInt32(notDiag1);
                    endCoordinates[0] = z;
                    endCoordinates[1] = cubeIndexLength * Convert.ToInt32(notDiag2);
                    endCoordinates[2] = cubeIndexLength * Convert.ToInt32(notDiag2);
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