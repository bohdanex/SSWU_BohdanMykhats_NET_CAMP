using System.Collections;
using System;
using System.Text;

namespace Task3
{
    internal class Program
    {
        static void Main(string[] args)
        {
            RunExample();
            RunUserApp();
        }

        private static void RunExample()
        {
            string sentence =
                @"Once upon a time, there was a big and strong wolf who loved to howl. {One day}, 
he stumbled upon a little red riding <hood> who was carrying a basket of goodies 
    for her grandmother. The {(wol)} decided to trick the little girl and pretend to be her 
grandmother. He (disguised himself with bonnet, shawl, and glasses. W)hen the little 
red riding hood arrived, the wolf greeted her with a smile and invited her in. 
Unbeknownst to the little girl, the wolf had a plan to eat her. 
(B)ut just as he was about to pounced a brave hunter burse through the door. The hunter
took one look at the wolf and decided to take matters into [his own hands]. ";

            Console.WriteLine("Given a text\n\n" + sentence);

            SentenceExtractor sentenceExtractor = new(sentence);
            sentenceExtractor.FindSentenceInParenthesis();
            Console.WriteLine("\nResult\n\n" + sentenceExtractor);
        }

        private static void RunUserApp()
        {
            while (true)
            {
                Console.Write("\nPlease enter a text: ");
                string enteredText = Console.ReadLine()!;

                SentenceExtractor sentenceExtractor = new(enteredText);
                sentenceExtractor.FindSentenceInParenthesis();

                Console.WriteLine("Result\n" + sentenceExtractor);
                Console.Write("Press ESC to exit");

                ConsoleKey key = Console.ReadKey(true).Key;
                if (key == ConsoleKey.Escape)
                {
                    break;
                }
            }
        }
    }
}