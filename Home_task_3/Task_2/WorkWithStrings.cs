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
        {
            char[] splitChars = { ' ', ',', '.', '!', '?' };
            string[] textToFind = text.Split(splitChars, StringSplitOptions.RemoveEmptyEntries);

            for(int wordIndex = 0; wordIndex < textToFind.Length; ++wordIndex)
            {
                string currentWord = textToFind[wordIndex];

                for (int letterIndex = 1; letterIndex < currentWord.Length; ++letterIndex)
                {
                    if (currentWord[letterIndex] == currentWord[letterIndex - 1])
                    {
                        textToFind[wordIndex] = replaceWith;
                        break;
                    }
                }
            }

            string output = string.Join(" ", textToFind);
            return output;
        }

        public override string ToString()
        {
            return Text;
        }
    }
}