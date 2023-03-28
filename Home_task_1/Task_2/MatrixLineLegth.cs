using System.Text;

namespace LongestHorizontalLine
{
    public class MatrixLineLength
    {
        private HalfByte[,] _matrix;

        public MatrixLineLength(int sizeX = 0, int sizeY = 0)
        {
            _matrix = new HalfByte[sizeX, sizeY];
        }

        public void SetMockMatrix()
        {
            _matrix = new HalfByte[,]
            {
                {2,1,2,2,2,2 },
                {0,3,3,3,2,2 },
                {1,1,3,3,3,2 },
                {0,3,3,3,2,2 },
            };
        }

        public void FillWithRandom(HalfByte maxRandomValue)
        {
            for(int x = 0; x < _matrix.GetLength(0); ++x)
            {
                for (int y = 0; y < _matrix.GetLength(1); ++y)
                {
                    int randomValue = new Random().Next(0, maxRandomValue.ToInt32());
                    _matrix[x,y] = randomValue;
                }
            }
        }

        public int FindLongestLine(out HalfByte colour, out int[] lengthRange, out int yPointPosition)
        {
            //Output
            lengthRange = new int[2];
            yPointPosition = 0;
            colour = 0;

            int maxLength = 1;

            for (int x = 0; x < _matrix.GetLength(0); ++x)
            {
                int currentMaxLength = 1;
                for (int y = 1; y < _matrix.GetLength(1); ++y)
                {
                    if (_matrix[x, y] == _matrix[x, y - 1])
                    {
                        ++currentMaxLength;
                    }
                    else
                    {
                        if(maxLength < currentMaxLength)
                        {
                            maxLength= currentMaxLength;
                            yPointPosition = x;
                            lengthRange[1] = y - 1;
                        }
                        currentMaxLength = 1;
                    }
                }

                if (maxLength < currentMaxLength)
                {
                    maxLength = currentMaxLength;
                    yPointPosition = x;
                    lengthRange[1] = _matrix.GetLength(1) - 1;
                }
            }

            //Formating the output

            //The first x position of the result based on maximum line position
            lengthRange[0] = lengthRange[1] - maxLength + 1;
            //length check
            if (lengthRange[0] == lengthRange[1]) return 0;
            colour = _matrix[yPointPosition, lengthRange[1]];
            return maxLength;
        }

        public override string ToString()
        {
            StringBuilder output = new();

            for (int x = 0; x < _matrix.GetLength(0); ++x)
            {
                for (int y = 0; y < _matrix.GetLength(1); ++y)
                {
                    output.Append($"{_matrix[x, y],-3}");
                }
                output.AppendLine();
            }

            return output.ToString();
        }
    }
}
