using System.Text;

namespace Task_3
{
    internal class Program
    {
        static void Main()
        {
            Console.OutputEncoding= Encoding.Unicode;
            Console.InputEncoding= Encoding.Unicode;
            ConsoleAppPresenter.RunConsoleApp();
            ConsoleAppPresenter.RunUserApp();
        }
    }
}