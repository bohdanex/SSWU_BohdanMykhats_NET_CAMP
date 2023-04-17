namespace HomeTask4
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
            EmailAnalyzer analyzer = new EmailAnalyzer("simple@example.com\n" +
                "other.email-with-hyphen@example.com;x@example.com;tes(t/tes))t@test.com\n" +
                "admin@mailserver1\n" +
                "example@s.example\n" +
                "\" \"@example.org\n" +
                "\"john..doe\"@example.org\n" +
                "\"very.(),:;<>[]\\\".VERY.\\\"very@\\\\ \\\"very\\\".unusual\"@strange.example.com\n" +
                "user%example.com@example.org\n" +
                "postmaster@[123.123.123.123]\n" +
                "postmaster@[IPv6:2001:0db8:85a3:0000:0000:8a2e:0370:7334]\n" +
                "Abc.example.com\n" +
                "A@b@c@example.com\n" +
                "this\\ still\\\"not\\\\allowed@example.com");

            analyzer.VerifyEmailAddress();
            Console.WriteLine(analyzer);
        }

        private static void RunUserApp()
        {
            while (true)
            {
                Console.WriteLine("Please enter emails which are separated with ,(coma), space, ;(semicolon)");
                Console.Write("-> ");
                string enteredEmails = Console.ReadLine()!;
                EmailAnalyzer analyzer = new EmailAnalyzer(enteredEmails);
                analyzer.VerifyEmailAddress();
                Console.WriteLine(analyzer);

                Console.WriteLine("\nPress ESC to exit");
                ConsoleKey key = Console.ReadKey().Key;
                if(key == ConsoleKey.Escape)
                {
                    break;
                }
            }
        }
    }
}