using System.Text;

namespace FindCutInCube
{// Чудово! Такий підхід я і хотіла бачити.
    public class CutInCube
    {
        private bool[,,] _cube;

        public CutInCube()
        {
            _cube = new bool[0,0,0];
        }

        public CutInCube(int cubeSize)
        {
            _cube = new bool[cubeSize,cubeSize,cubeSize];
        }

        public void SetMockCube(bool[,,] cube)
        {
            _cube = cube;
        }

        public void RandomizeMatrix()
        {
            int cubeLength = _cube.GetLength(0);
            Random randomBool = new();

            for (int i = 0; i < cubeLength; ++i)
            {
                for (int j = 0; j < cubeLength; ++j)
                {
                    for (int k = 0; k < cubeLength; ++k)
                    {
                        _cube[k, j, i] = Convert.ToBoolean(randomBool.Next(0, 2));
                    }
                }
            }
        }

        public bool FindDiagonals(out int[] startCoordinates, out int[] endCoordinates)
        {
            int cubeIndexLength = _cube.GetLength(0) - 1;

            startCoordinates = new int[3];
            endCoordinates = new int[3];

            for (int z = 0; z < cubeIndexLength; ++z)
            {
                bool temp_notDiag1 = false;
                bool temp_notDiag2 = false;
                for(int xy = 0; xy < cubeIndexLength; ++xy)
                {
                    temp_notDiag1 |= _cube[z, xy, xy] ;
                    temp_notDiag2 |= _cube[z, cubeIndexLength - xy, xy];
                    //break the loop if 1 (condition = false) exists at one of diaglonales
                    if (temp_notDiag2 && temp_notDiag1) break;
                }
                if (!(temp_notDiag1 & temp_notDiag2))
                {
                    //Here we make sure, these values are different to correct output
                    temp_notDiag1 ^= !temp_notDiag2;
                    startCoordinates[0] = z;
                    startCoordinates[1] = cubeIndexLength * Convert.ToInt32(temp_notDiag1);
                    startCoordinates[2] = cubeIndexLength * Convert.ToInt32(temp_notDiag1);
                    endCoordinates[0] = z;
                    endCoordinates[1] = cubeIndexLength * Convert.ToInt32(temp_notDiag2);
                    endCoordinates[2] = cubeIndexLength * Convert.ToInt32(temp_notDiag2);
                    return true;
                }
            }
            return false;
        }

        public bool FindHorizaontalsAndVerticals(out int[] startCoordinates, out int[] endCoordinates)
        {
            int cubeLength = _cube.GetLength(0);

            startCoordinates = Array.Empty<int>();
            endCoordinates = Array.Empty<int>();

            for (int i = 0; i < cubeLength; ++i)
            {
                for(int j = 0; j < cubeLength; ++j)
                {
                    bool temp_isNotHorizontal = false,
                         temp_isNotVertical = false,
                         temp_isNotStraight = false;

                    for (int k = 0; k < cubeLength; ++k)
                    {
                        temp_isNotHorizontal |= _cube[i,j,k];
                        temp_isNotVertical |= _cube[i,k,j];
                        temp_isNotStraight |= _cube[k,i,j];
                    }
                    if (!temp_isNotHorizontal)
                    {
                        startCoordinates = new int[3] {i,j,0};
                        endCoordinates = new int[3] {i,j,cubeLength};
                        return true;
                    }
                    else if (!temp_isNotVertical)
                    {
                        startCoordinates = new int[3] { i, 0, j };
                        endCoordinates = new int[3] { i, cubeLength, j};
                        return true;
                    }
                    else if (!temp_isNotStraight)
                    {
                        startCoordinates = new int[3] { 0, i, j };
                        endCoordinates = new int[3] { cubeLength, i, j };
                        return true;
                    }
                }
            }
            return false;
        }

        public override string ToString()
        {
            int cubeLength = _cube.GetLength(0);
            StringBuilder output = new();

            for (int z = 0; z < cubeLength; ++z)
            {
                output.Append('{');
                for (int y = 0; y < cubeLength; ++y)
                {
                    output.Append("\n  {");
                    for (int x = 0; x < cubeLength; ++x)
                    {
                        output.Append($" {_cube[z, y, x]} ");
                    }
                    output.Append("}\n");
                }
                output.Append("}\n\n");
            }

            return output.ToString();
        }
    }
}
