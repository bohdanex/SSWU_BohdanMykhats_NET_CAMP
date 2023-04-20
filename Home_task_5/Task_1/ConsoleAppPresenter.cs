using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task_1
{
    internal static class ConsoleAppPresenter
    {
        public static void RunApp()
        {
            Point[] points =
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
            Point[] points2 =
            {
                new Point(3,18),
                new Point(7,14),
                new Point(29,20),
            };
            Point[] points3 =
            {
                new Point(11,32),
                new Point(26,14),
            };
            Point[] pointsOne =
            {
                new Point(32,32),
            };

            QuickShellPerimeter quickShell1 = new QuickShellPerimeter(points);
            QuickShellPerimeter quickShell2 = new QuickShellPerimeter(points2);
            QuickShellPerimeter quickShell3 = new QuickShellPerimeter(points3);
            QuickShellPerimeter quickShellOnePoint = new QuickShellPerimeter(pointsOne);

            Console.WriteLine("(quickShell1)\n" + quickShell1);
            Console.WriteLine("(quickShell2)\n" + quickShell2);
            Console.WriteLine("(quickShell3)\n" + quickShell3);
            Console.WriteLine("(quickShellOnePoint)\n" + quickShellOnePoint);

            Console.WriteLine("\nquickShell1 < quickShell2: " + (quickShell1 < quickShell2));
            Console.WriteLine("quickShell1 > quickShell2: " + (quickShell1 > quickShell2));
            Console.WriteLine("quickShell1 != quickShell3: " + (quickShell1 != quickShell3));
        }
    }
}
