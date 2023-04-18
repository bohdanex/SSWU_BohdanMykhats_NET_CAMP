using System.Collections.Generic;

namespace Task_1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            List<Point> points = new()
            {
                new Point(28,18),
                new Point(26,14),
                new Point(22,18),
                new Point(34,16),
                new Point(24,15),
                new Point(24,22),
                new Point(32,22),
                new Point(28,26),
                new Point(23,21),
            };
            QuickShell fastShell = new QuickShell();
            QuickShell.FindPerimeter(points);
        }
    }

    public class QuickShell
    {
        private Point[] _points;

        public Point[] Points
        {
            get => _points;
            set
            {
                _points = value;
            }
        }
        public double Perimeter { get; private set; }

        public QuickShell()
        {
            _points= new Point[0];
        }

        public QuickShell(Point[] points)
        {
            _points = points;
        }

        public static double FindPerimeter(List<Point> points)
        {
            MinMaxPointIndexes(points, out int minIndex, out int maxIndex);
            SplitGraph(points, points[minIndex], points[maxIndex], out List<Point> leftGraph, out List<Point> rightGraph);
            List<Point> graphResult = FindShellPoint(leftGraph, points[minIndex], points[maxIndex]);
            graphResult.AddRange(FindShellPoint(rightGraph, points[minIndex], points[maxIndex]));

            graphResult.Sort((p1, p2) => p1.X.CompareTo(p2.X));
            graphResult.Sort((p1, p2) => p1.Y.CompareTo(p2.Y));

            double perimeter = 0;
            int n = graphResult.Count;

            for (int i = 0; i < n; i++)
            {
                Point p1 = graphResult[i];
                Point p2 = graphResult[(i + 1) % n]; 
                perimeter += Math.Sqrt(Math.Pow(p2.X - p1.X, 2) + Math.Pow(p2.Y - p1.Y, 2));
            }

            return perimeter;
        }

        private static void SplitGraph(List<Point> points, Point leftPoint, Point rightPoint, out List<Point> leftGraph, out List<Point> rightGraph)
        {
            rightGraph = new();
            leftGraph = new();
            for (int i = 0; i < points.Count; ++i)
            {
                double left = ((double)points[i].X - leftPoint.X) / (rightPoint.X - leftPoint.X);
                double right = ((double)points[i].Y - leftPoint.Y) / (rightPoint.Y - leftPoint.Y);
                if (left <= right)
                {
                    leftGraph.Add(points[i]);
                }
                if(left >= right)
                {
                    rightGraph.Add(points[i]);
                }
            }
        }

        private static void MinMaxPointIndexes(List<Point> points, out int minIndex, out int maxIndex)
        {
            minIndex = 0;
            maxIndex = 0;
            int min_X_Value = points[0].X;
            int max_X_Value = points[0].X;
            
            for (int i = 1; i < points.Count; ++i)
            {
                if (points[i].X < min_X_Value)
                {
                    min_X_Value = points[i].X;
                    minIndex= i;
                }
                else if (points[i].X > max_X_Value)
                {
                   max_X_Value= points[i].X;
                   maxIndex= i;
                }
            }
        }

        private static List<Point> FindShellPoint(List<Point> points, Point leftPoint, Point rightPoint)
        {
            if(points.Count <= 3)
            {
                return points;
            }

            Point farthestPoint = new();
            Point center = new( (leftPoint.X + rightPoint.X) / 2, (leftPoint.Y + rightPoint.Y) / 2 );
            List<Point> resultPoints = new();

            double maxLength = 0;
            for (int i = 0; i < points.Count; ++i)
            {
                double currentLength = Math.Sqrt(Math.Abs((points[i].X - center.X) ^ 2 + (points[i].Y - center.Y) ^ 2));
                if(maxLength < currentLength && points[i] != leftPoint && points[i] != rightPoint)
                {
                    maxLength = currentLength;
                    farthestPoint = points[i];
                }
            }

            SplitGraph(points, leftPoint, farthestPoint, out List<Point> leftGraph, out _);
            resultPoints.AddRange(FindShellPoint(leftGraph, leftPoint, farthestPoint));
            if (points.Count == 4) return resultPoints;
            SplitGraph(points, farthestPoint, rightPoint, out _, out List<Point> rightGraph);
            resultPoints.AddRange(FindShellPoint(rightGraph, leftPoint, farthestPoint));

            return resultPoints;
        }

        public static bool operator ==(QuickShell left, QuickShell right)
        {
            throw new NotImplementedException();
        }
        public static bool operator !=(QuickShell left, QuickShell right)
        {
            throw new NotImplementedException();
        }

        public static bool operator <=(QuickShell left, QuickShell right)
        {
            throw new NotImplementedException();
        }
        public static bool operator >=(QuickShell left, QuickShell right)
        {
            throw new NotImplementedException();
        }

        public static bool operator <(QuickShell left, QuickShell right)
        {
            throw new NotImplementedException();
        }
        public static bool operator >(QuickShell left, QuickShell right)
        {
            throw new NotImplementedException();
        }
    }

    public readonly record struct Point(int X, int Y);
}