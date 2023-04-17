using System.Text;

namespace Task3
{
    public class SentenceExtractor
    {
        public string[] MatchResult { get; private set; }

        public string Text { get; set; }

        public SentenceExtractor(string text)
        {
            Text = text;
            MatchResult= Array.Empty<string>();
        }

        public void FindSentenceInParenthesis()
        {
            MatchResult = FindSentenceInParenthesis(Text);
        }

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

        public override string ToString()
        {
            StringBuilder output = new();
            foreach (string s in MatchResult)
            {
                output.Append(s);
            }

            return output.ToString();
        }
    }
}