namespace HomeTask4
{
    internal class Program
    {
        static void Main(string[] args)
        {
            EmailAnalyzer analyzer = new EmailAnalyzer(@"postmaster@[IPv6:2001:0db8:85a3:0000:0000:8a2e:0370:7334]");
            analyzer.VerifyEmailAddress(out _);
        }
    }

    public class EmailAnalyzer
    {
        //Приклади валідних емейлів:
        //simple@example.com
        //other.email-with-hyphen@example.com
        //x@example.com
        //test/test@test.com
        //admin@mailserver1
        //example@s.example
        //" "@example.org
        //"john..doe"@example.org
        //"very.(),:;<>[]\".VERY.\"very@\\ \"very\".unusual"@strange.example.com
        //user%example.com@example.org
        //postmaster@[123.123.123.123]
        //postmaster@[IPv6:2001:0db8:85a3:0000:0000:8a2e:0370:7334]
        public string Text { get; set; }

        public EmailAnalyzer(string text = "\0")
        {
            Text= text;
        }

        public string[]? VerifyEmailAddress(out string[]? incorrectAddresses)
        {
            incorrectAddresses = default;

            List<string> correctEmails= new List<string>();
            List<string> incorrectEmails= new List<string>();
            
            string[]? allMails = { @" @asdasd.asdasd..asd" };

            foreach (string email in allMails)
            {
                string localPart;
                string domain;

                SplitByAtSymbol(email, out localPart, out domain);
                IsValidDomain(domain);
                IsValidLocalPart(localPart);
                //1. ASCII check
            }
            return null;
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

            RemoveCommentFromDomain(ref domain);

            if (domain.Contains("..") || domain.Contains("--"))
            {
                return false;
            }

            foreach (char emailChar in domain)
            {
                if (!char.IsLetter(emailChar) && !char.IsDigit(emailChar) && emailChar != '-')
                {
                    return false;
                }
            }

            return true;
        }

        private static string[] SplitByAtSymbol(string email, out string localPart, out string domain)
        {
            string[] splitResult = new string[2];

            for(int charIndex = email.Length - 1; charIndex >= 0; --charIndex)
            {
                if (email[charIndex] == '@')
                {
                    splitResult[0] = email[..charIndex];
                    splitResult[1] = email[(charIndex+1)..];
                    break;
                }
            }

            localPart = splitResult[0];
            domain = splitResult[1];

            if(string.IsNullOrEmpty(localPart) || string.IsNullOrEmpty(domain))
            {
                throw new NullReferenceException("Either the local part or domain of the email must be non-nullable");
            }

            return splitResult;
        }

        private static bool IsAscii(string email)
        {
            foreach(char c in email)
            {
                if (!char.IsAscii(c))
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

        private static void RemoveCommentFromDomain(ref string domain)
        {
            for(int charIndex = 0; charIndex < domain.Length; ++charIndex)
            {
                if (domain[charIndex] == '(')
                {
                    for (int i = charIndex + 1; i < domain.Length; ++i)
                    {
                        if (domain[i] == ')')
                        {
                            domain = domain.Remove(charIndex, i - charIndex + 1);
                            return;
                        }
                    }
                }
            }
        }
        #endregion
        #region Local part email validator
        private static bool IsValidLocalPart(string localPart)
        {

            return true;
        }

        private static bool IsInQuotes(string localPart)
        {
            const char DoubleQuoutes = '"';
            if (!localPart[0].Equals(DoubleQuoutes) && !localPart[^1].Equals(DoubleQuoutes))
            {
                return false;
            }
            return true;
        }
        #endregion
    }
}