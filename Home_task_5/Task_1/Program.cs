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
            List<Point> rightGraphResultPoints = FindShellPoint(rightGraph, points[minIndex], points[maxIndex]);
            List<Point> leftGraphResultPoints = FindShellPoint(leftGraph, points[minIndex], points[maxIndex]);
            return 0;
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

    public readonly record struct Point(int X, int Y)
    {
        public static bool operator >(Point left, Point right)
        {
            if(left.X > right.X && left.Y > right.Y)
            {
                return true;
            }
            return false;
        }

        public static bool operator <(Point left, Point right)
        {
            if (left.X < right.X && left.Y < right.Y)
            {
                return true;
            }
            return false;
        }

        public static bool operator >=(Point left, Point right)
        {
            if (left.X >= right.X && left.Y >= right.Y)
            {
                return true;
            }
            return false;
        }

        public static bool operator <=(Point left, Point right)
        {
            if (left.X <= right.X && left.Y <= right.Y)
            {
                return true;
            }
            return false;
        }
    }
}