using System.Collections;
using System;
using System.Text;

namespace FindTextInBrackets
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string sentance =
                @"Once upon a time, there was a big and strong wolf who loved to howl. {One day}, 
he stumbled upon a little red riding <hood> who was carrying a basket of goodies 
    for her grandmother. The {(wol)} decided to trick the little girl and pretend to be her 
grandmother. He (disguised himself with bonnet, shawl, and glasses. W)hen the little 
red riding hood arrived, the wolf greeted her with a smile and invited her in. 
Unbeknownst to the little girl, the wolf had a plan to eat her. 
(B)ut just as he was about to pounced a brave hunter burse through the door. The hunter
took one look at the wolf and decided to take matters into [his own hands]. ";

            SentenceExtractor.FindSentenceInParenthesis(sentance);
        }
    }

    public static class SentenceExtractor
    {
        public static string[] FindSentenceInParenthesis(string text)
        {
            const string punctuationMarks = ".?!";
            List<int> occuredSentanceInBracketsIndeces = new();
            Dictionary<char,char> bracketPairs = new()
            {
                {'(', ')'},
                {'{', '}'},
                {'[', ']'},
                {'<', '>'},
            };
            string[] textSplitted = SplitBySymbols(text, punctuationMarks);

            for(int sentenceIndex = 0; sentenceIndex < textSplitted.Length && textSplitted.Length > 2; ++sentenceIndex)
            {
                string currentSentence = textSplitted[sentenceIndex];
                for (int charIndex = 0; charIndex < currentSentence.Length; ++charIndex)
                {
                    foreach (char bracketAsKey in bracketPairs.Keys)
                    {
                        if(bracketAsKey.Equals(currentSentence[charIndex]))
                        {
                            //зміщення на дві позиції, бо дужки без вмісту не задовільняють умову
                            for(int i = charIndex + 2; i < currentSentence.Length; ++i)
                            {
                                if (currentSentence[i].Equals(bracketPairs[bracketAsKey]))
                                {
                                    occuredSentanceInBracketsIndeces.Add(sentenceIndex);
                                    //пропустити подальшу перевірку символу з колекції
                                    goto nextSentence;
                                }
                            }
                            break;
                        }
                    }
                }
            nextSentence: ;
            }

            string[] resultSentences = new string[occuredSentanceInBracketsIndeces.Count];

            for (int sentenceIndex = 0; sentenceIndex < resultSentences.Length; sentenceIndex++)
            {
                resultSentences[sentenceIndex] = textSplitted[occuredSentanceInBracketsIndeces[sentenceIndex]];
            }

            return resultSentences;
        }

        private static string[] SplitBySymbols(string text, string symbols)
        {
            List<int> charIndexPositions = new List<int>();

            for (int charIndex = 0; charIndex < text.Length; charIndex++)
            {
                if (symbols.Contains(text[charIndex]))
                {
                    charIndexPositions.Add(charIndex);
                }
            }

            string[] splitResult = new string[charIndexPositions.Count];

            int lastIndex = 0;
            for(int i = 0; i < charIndexPositions.Count; ++i)
            {
                splitResult[i] = text[lastIndex..(charIndexPositions[i] + 1)];
                lastIndex = charIndexPositions[i] + 1;
            }

            return splitResult;
        }
    }
}