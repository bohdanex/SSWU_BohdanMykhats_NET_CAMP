using System.Text;

namespace SpiralNumbers
{
    public class Spiral
    {
        private readonly int[,] _matrix;

        public Spiral(int sizeX = 6, int sizeY = 4)
        {
            _matrix = new int[sizeX, sizeY];
        }

        /// <summary>
        /// Builds a matrix in which numbers represented as a spiral
        /// </summary>
        public void BuildSpiral()
        {
            int i = 1,
                leftOffset = 0,
                rightOffset = _matrix.GetLength(0) - 1,
                topOffset = 0,
                bottomOffset = _matrix.GetLength(1) - 1;

            while (i <= _matrix.Length)
            {
                //Move down
                for (int j = topOffset; j <= bottomOffset && i <= _matrix.Length; ++j)
                {
                    _matrix[leftOffset, j] = i++;
                }
                ++leftOffset;

                //Move right
                for (int j = leftOffset; j <= rightOffset && i <= _matrix.Length; ++j)
                {
                    _matrix[j, bottomOffset] = i++;
                }
                --bottomOffset;

                //Move up
                for (int j = bottomOffset; j >= topOffset && i <= _matrix.Length; --j)
                {
                    _matrix[rightOffset, j] = i++;
                }
                --rightOffset;

                //Move left
                for (int j = rightOffset; j >= leftOffset && i <= _matrix.Length; --j)
                {
                    _matrix[j, topOffset] = i++;
                }
                ++topOffset;
            }
        }

        public override string ToString()
        {
            StringBuilder spiralOutput = new();
            Console.WriteLine($"Matrix size: {this._matrix.GetLength(0)}x{this._matrix.GetLength(1)}");
            for (int i = 0; i < _matrix.GetLength(1); ++i)
            {
                for (int j = 0; j < _matrix.GetLength(0); ++j)
                {
                    spiralOutput.Append($"{_matrix[j, i],-4}");
                }
                spiralOutput.AppendLine("\n");
            }
            return spiralOutput.ToString();
        }
    }
}
