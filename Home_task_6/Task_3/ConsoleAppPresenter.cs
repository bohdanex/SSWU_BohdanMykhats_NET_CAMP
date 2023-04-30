namespace Task_3
{
    internal static class ConsoleAppPresenter
    {
        public static void RunConsoleApp()
        {
            string text = "Напишіть функцію, яка буде приймати рядок тексту і повертати всі унікальні\r\n" +
                "слова з цього рядка, використовуючи ключове слово yield. yield, ЯкА Буде і ТекСССту тЕксту yield";
            foreach (string word in UniqueWords.UnifyWords(text))
            {
                Console.WriteLine(word);
            }
        }

        public static void RunUserApp()
        {
            while (true)
            {
                string? readInfo;
                while (true)
                {
                    Console.Write("Enter a text: ");
                    readInfo = Console.ReadLine();
                    if(readInfo is not null)
                    {
                        break;
                    }
                }

                foreach (string word in UniqueWords.UnifyWords(readInfo))
                {
                    Console.WriteLine(word);
                }

                Console.WriteLine("\nPress any key to continue and ESC to exit");
                ConsoleKey key = Console.ReadKey(false).Key;
                if(key is ConsoleKey.Escape)
                {
                    break;
                }
            }
        }
    }
}
