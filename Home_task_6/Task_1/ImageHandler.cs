using System.Collections;
using System.Text;

namespace Task_1
{// покажіть цю роботу на занятті.
    public class ImageHandler : IEnumerable<int>
    {
        private int[,] _matrix;

        public int[,] Matrix
        {
            get { return _matrix; }
            set
            {
                if (value.GetLength(0) != value.GetLength(1))
                {
                    throw new ArgumentException("The width and the height of the matrix must be the same");
                }
                _matrix = (int[,])value.Clone();
            }
        }

        public ImageHandler(int[,] matrix)
        {
            _matrix = new int[0,0];
            Matrix= matrix;
        }

        private class SnakeImageEnumerator : IEnumerator<int>
        {
            private int[,] _matrix;
            private int x, y;
            private int moveDiag;
            private bool hasMovedByDiag;

            object IEnumerator.Current => _matrix[y, x];
            public int Current => _matrix[y, x];

            public SnakeImageEnumerator(int[,] matrix)
            {
                _matrix = (int[,])matrix.Clone();
                Reset();
            }

            public bool MoveNext()
            {
                int destination = _matrix.GetLength(0) - 1;
                if (x < destination || y < destination)
                {
                    if (hasMovedByDiag)
                    {

                        if ((x == 0 || x == destination) && y != destination)
                        {
                            ++y;
                            moveDiag = -moveDiag;
                            hasMovedByDiag = false;
                            return true;
                        }
                        else if (y == 0 || y == destination)
                        {
                            ++x;
                            moveDiag = -moveDiag;
                            hasMovedByDiag = false;
                            return true;
                        }
                    }

                    x += moveDiag;
                    y += -moveDiag;
                    hasMovedByDiag = true;
                    return true;
                }

                return false;
            }

            public void Reset()
            {
                x = 1;
                y = -1;
                moveDiag = -1;
                hasMovedByDiag = false;
            }

            public void Dispose()
            {
                GC.SuppressFinalize(this);
                _matrix = new int[0, 0];
                x = default;
                y = default;
                hasMovedByDiag = default;
                moveDiag = default;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return new SnakeImageEnumerator(_matrix);
        }

        public IEnumerator<int> GetEnumerator()
        {
            return new SnakeImageEnumerator(_matrix);
        }

        public void UpdateMatrix(int[,] matrix)
        {
            Matrix = matrix;
        }

        public override string ToString()
        {
            StringBuilder output = new StringBuilder();
            int matrixLength = Matrix.GetLength(0);

            output.AppendLine($"Matrix size: {matrixLength}x{matrixLength}");

            for (int i = 0; i < matrixLength; ++i)
            {
                for (int j = 0; j < matrixLength; j++)
                {
                    output.Append($"{_matrix[i, j],-3}");
                }
                output.AppendLine();
            }
            return output.ToString();
        }
    }
}
