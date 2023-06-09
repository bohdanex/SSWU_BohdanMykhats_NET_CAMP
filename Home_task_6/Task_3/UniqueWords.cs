﻿namespace Task_3
{//Сумарний бал -95
    public static class UniqueWords
    {
        public static IEnumerable<string> UnifyWords(string text)
        {
            char[] splitChars = { ' ', ',', '.', '!', '?', '\n'};
            string[] allWords = text.Split(splitChars, options: StringSplitOptions.RemoveEmptyEntries);
            
            yield return allWords[0];
            for(int wordIndex = 1; wordIndex < allWords.Length; ++wordIndex)
            {
                bool isUnique = true;
                // А множиною скористатись?
                for (int j = wordIndex - 1; j >= 0 ; --j)
                {
                    if (allWords[wordIndex].Equals(allWords[j], StringComparison.InvariantCultureIgnoreCase))
                    {
                        isUnique = false;
                        break;
                    }
                }

                if (isUnique)
                {
                    yield return allWords[wordIndex];
                }
            }
        }

        public static IEnumerable<string> UnifyWords2(string text)
        {
            char[] splitChars = { ' ', ',', '.', '!', '?', '\n' };
            string[] allWords = text.Split(splitChars, options: StringSplitOptions.RemoveEmptyEntries);

            HashSet<string> strings = new HashSet<string>(allWords);
            
            foreach (string word in allWords)
            {
                yield return word;
            }
        }
    }
}
