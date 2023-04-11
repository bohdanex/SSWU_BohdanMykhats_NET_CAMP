using System.Runtime.CompilerServices;

namespace WorkWithStrings
{
    public class WorkWithStrings
    {
        public string Text { get; set; }

        public WorkWithStrings(string text)
        {
            Text = text;
        }

        public void ChangeText(string text)
        {
            Text = text;
        }

        //Цей клас має статичні методи. Тому для того, щоб не повторювати код,
        //методи екземплярів будуть викликати статичні методи
        public int? GetSecondOccurenceIndex(string occurrenceText)
        {
            return GetSecondOccurenceIndex(Text, occurrenceText);
        }

        public int CountWordsStartingWithUpperCase()
        {
            return CountWordsStartingWithUpperCase(Text);
        }

        public string ReplaceRepeatingCharacters(string replaceWith)
        {
            return ReplaceRepeatingCharacters(Text, replaceWith);
        }

        public static int? GetSecondOccurenceIndex(string text, string occurrenceText)
        {
            int? occurenceIndex = text.IndexOf(occurrenceText);
            occurenceIndex = text.IndexOf(occurrenceText, occurrenceText.Length + occurenceIndex.Value);

            if (occurenceIndex.Value == -1) return null;

            return occurenceIndex;
        }

        public static int CountWordsStartingWithUpperCase(string text)
        {
            int uppercaseCount = 0;
            char[] splitChars = {' ',',','.','!','?'};
            string[] textToFind = text.Split(splitChars, StringSplitOptions.RemoveEmptyEntries);

            foreach(string word in textToFind)
            {
                if (char.IsUpper(word[0])) uppercaseCount++;
            }

            return uppercaseCount;
        }

        public static string ReplaceRepeatingCharacters(string text, string replaceWith)
        {// складно. Можна простіше...
            int firstLetterOfWordIndex = char.IsLetter(text[0]) ? 0 : -1;
            string outputText = (string)text.Clone();
            bool isDoubleLetter = false;

            for(int letterIndex = 1; letterIndex < outputText.Length; ++letterIndex)
            {
                if (char.IsLetter(outputText[letterIndex]))
                {
                    if (outputText[letterIndex] == outputText[letterIndex - 1])
                    {
                        isDoubleLetter = true;
                    }
                    if (firstLetterOfWordIndex == -1)
                    {
                        firstLetterOfWordIndex = letterIndex;
                    }
                }

                //Дублювання літер може відбутися у кінці рядка
                if (char.IsPunctuation(outputText[letterIndex]) || char.IsWhiteSpace(outputText[letterIndex]) 
                    || letterIndex == outputText.Length-1)
                {
                    if(firstLetterOfWordIndex != -1 && isDoubleLetter)
                    {
                        outputText = outputText.Remove(firstLetterOfWordIndex, 
                            letterIndex - firstLetterOfWordIndex + Convert.ToInt32(letterIndex == outputText.Length - 1));

                        outputText = outputText.Insert(firstLetterOfWordIndex, replaceWith);
                        letterIndex = firstLetterOfWordIndex + replaceWith.Length;
                    }
                    isDoubleLetter = false;
                    firstLetterOfWordIndex = -1;
                    continue;
                }
            }
            return outputText;
        }

        public override string ToString()
        {
            return Text;
        }
    }
}
