namespace WorkWithStrings
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Example of method ReplaceRepeatingCharacters. Given a text:" +
                "\n\"aaaaa. ,   kkoas, sad    lk.asdddd\", replace with: \"TestString\"");
            Console.WriteLine("Result: " + WorkWithStrings.ReplaceRepeatingCharacters("aaaaa. ,   kkoas, sad    lk.asdddd", "TestString"));

            while (true)
            {
                int? secondOccurence= null;
                int upperCaseCount = 0;
                string? enteredText = null;

                Console.WriteLine("\nThis app do the following operations with text:" +
                    "\n 1. Finds the index of the second occurrence of the given substring in the text;" +
                    "\n 2. Returns the number of Uppercase words in the text;" +
                    "\n 3. Replace all words containing doubling of letters with the given one.");

                while (true)
                {
                    Console.Write("\nPlease enter a text\n -> ");
                    enteredText = Console.ReadLine();

                    if (enteredText == null || string.IsNullOrEmpty(enteredText)) continue;

                    break;
                }

                upperCaseCount = WorkWithStrings.CountWordsStartingWithUpperCase(enteredText);
                Console.WriteLine("\nUppercase word count: " + upperCaseCount);

                Console.Write("\nPlease enter a substring for the first option -> ");
                secondOccurence = WorkWithStrings.GetSecondOccurenceIndex(enteredText, Console.ReadLine());
                Console.WriteLine("The index of the second occurrence is " + secondOccurence);

                Console.Write("\nPlease enter a substring to be replaced with doubings: -> ");
                Console.WriteLine("Result: " + WorkWithStrings.ReplaceRepeatingCharacters(enteredText, Console.ReadLine()));

                ConsoleKey consoleKey;
                do
                {
                    Console.WriteLine("\nDo you want to run this again? press Y/N -> ");
                    consoleKey = Console.ReadKey(true).Key;
                }
                while (consoleKey != ConsoleKey.Y && consoleKey != ConsoleKey.N);

                if (consoleKey == ConsoleKey.N) break;
            }
        }
    }
}