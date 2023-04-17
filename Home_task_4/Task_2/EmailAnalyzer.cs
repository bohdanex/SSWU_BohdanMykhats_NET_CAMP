using System.Text;

namespace HomeTask4
{
    public class EmailAnalyzer
    {
        public string[] CorrectEmails { get; private set; }
        public string[] IncorrectEmails { get; private set; }

        public string Text { get; set; }
        
        public EmailAnalyzer(string text = "\0")
        {
            Text= text;
            CorrectEmails= new string[0];
            IncorrectEmails= new string[0];
        }

        public void VerifyEmailAddress()
        {
            List<string> correctEmails= new List<string>();
            List<string> incorrectEmails= new List<string>();
            
            string[]? allMails = SplitEmails(Text);

            foreach (string email in allMails)
            {
                string? localPart;
                string? domain;

                SplitByAtSymbol(email, out localPart, out domain);
                
                if(String.IsNullOrEmpty(localPart) || String.IsNullOrEmpty(domain))
                {
                    continue;
                }

                if (!IsASCII(email))
                {
                    incorrectEmails.Add(email);
                    continue;
                }

                if (IsValidDomain(domain) && IsValidLocalPart(localPart))
                {
                    correctEmails.Add(email);
                }
                else
                {
                    incorrectEmails.Add(email);
                }
            }

            this.CorrectEmails = correctEmails.ToArray();
            this.IncorrectEmails = incorrectEmails.ToArray();
        }

        private static bool IsASCII(string email)
        {
            foreach (char c in email)
            {
                if (!char.IsAscii(c))
                {
                    return false;
                }
            }
            return true;
        }

        private static void SplitByAtSymbol(string email, out string? localPart, out string? domain)
        {
            localPart = null;
            domain = null;

            if (!email.Contains('@'))
            {
                return;
            }

            for (int charIndex = email.Length - 1; charIndex >= 0; --charIndex)
            {
                if (email[charIndex] == '@')
                {
                    localPart = email[..charIndex];
                    domain = email[(charIndex + 1)..];
                    break;
                }
            }
        }

        private static void RemoveComment(ref string text)
        {
            for (int charIndex = 0; charIndex < text.Length; ++charIndex)
            {
                if (text[charIndex] == '(')
                {
                    for (int i = charIndex + 1; i < text.Length; ++i)
                    {
                        if (text[i] == ')')
                        {
                            text = text.Remove(charIndex, i - charIndex + 1);
                            return;
                        }
                    }
                }
            }
        }

        #region Domain validator
        private static bool IsValidDomain(string domain)
        {
            if (domain.Length > 63)
            {
                return false;
            }

            if (IsIPv4Domain(domain) || IsIPv6Domain(domain))
            {
                return true;
            }

            if (domain[0] == '-' || domain[^1] == '-' || domain[0] == '.' || domain[^1] == '.')
            {
                return false;
            }

            RemoveComment(ref domain);

            if (domain.Contains("..") || domain.Contains("--"))
            {
                return false;
            }

            foreach (char emailChar in domain)
            {
                if (!char.IsLetterOrDigit(emailChar) && emailChar != '-' && emailChar != '.')
                {
                    return false;
                }
            }

            return true;
        }

        private static bool IsIPv6Domain(string domain)
        {
            string managableDomain = (string)domain.Clone();
            
            if (managableDomain[0] != '[' || managableDomain[^1] != ']')
            {
                return false;
            }

            managableDomain = managableDomain[1..^1];
            managableDomain = managableDomain.Replace("ipv6:","",
                ignoreCase: true,
                culture: null);

            string[] unicastAddresses = managableDomain.Split(':');

            if(unicastAddresses.Length != 8)
            {
                return false;
            }

            foreach (string address in unicastAddresses)
            {
                if(!int.TryParse(
                    s: address, 
                    style: System.Globalization.NumberStyles.HexNumber,
                    provider: null,
                    out _))
                {
                    return false;
                }
            }
            return true;
        }

        private static bool IsIPv4Domain(string domain)
        {
            string managableDomain = (string)domain.Clone();

            if (managableDomain[0] != '[' || managableDomain[^1] != ']')
            {
                return false;
            }

            managableDomain = managableDomain[1..^1];
            string[] unicastAddresses = managableDomain.Split('.');

            if(unicastAddresses.Length != 4)
            {
                return false;
            }

            foreach (string address in unicastAddresses)
            {
                if (!byte.TryParse
                    (
                    s: address,
                    out _ ))
                {
                    return false;
                }
            }
            return true;
        }
        #endregion
        
        #region Local part email validator
        private static bool IsValidLocalPart(string localPart)
        {
            const string AllowedSymbols = "!#$%&'*+-/=?^_`{|}~";

            if (IsInQuotes(localPart))
            {
                return true;
            }

            RemoveComment(ref localPart);

            if (localPart.Contains(".."))
            {
                return false;
            }

            foreach (char c in localPart)
            {
                if(!char.IsLetterOrDigit(c) && !AllowedSymbols.Contains(c) && !c.Equals('.'))
                {
                    return false;
                }
            }

            return true;
        }

        private static bool IsInQuotes(string localPart)
        {
            const char DOUBLE_QUOTES = '"';
            if (!localPart[0].Equals(DOUBLE_QUOTES) || !localPart[^1].Equals(DOUBLE_QUOTES))
            {
                return false;
            }
            return true;
        }
        #endregion
        
        private static string[] SplitEmails(string emails)
        {
            bool matchSymbolAt = false;
            bool isDoubleQuotes = false;
            int firstEmailCharIndex = 0;
            string splitChars = ";, \n";
            List<string> emailsSplitted = new();

            emails = emails.Trim();

            for (int charIndex = 0; charIndex < emails.Length; ++charIndex)
            {
                if (emails[charIndex].Equals('"') && !matchSymbolAt)
                {
                    isDoubleQuotes = true;
                }
                if(charIndex == emails.Length - 1 || (matchSymbolAt && splitChars.Contains(emails[charIndex]) ))
                {
                    emailsSplitted.Add(emails[firstEmailCharIndex..(charIndex + Convert.ToInt32(charIndex == emails.Length - 1))]);
                    matchSymbolAt = false;
                    ++charIndex;
                    firstEmailCharIndex= charIndex;
                    continue;
                }
                if (emails[charIndex].Equals('@'))
                {
                    if (isDoubleQuotes && emails[charIndex - 1] != '"')
                    {
                        continue;
                    }
                    matchSymbolAt= true;
                    isDoubleQuotes= false;
                }
            }

            return emailsSplitted.ToArray();
        }

        public override string ToString()
        {
            StringBuilder outputString = new($"All emails\n{Text}\n\nCorrect emails\n");

            foreach (string email in CorrectEmails)
            {
                outputString.AppendLine(" " + email);
            }

            outputString.AppendLine("\nIncorrect emails");

            foreach (string email in IncorrectEmails)
            {
                outputString.AppendLine(" " + email);
            }

            return outputString.ToString();
        }
    }
}