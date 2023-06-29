using System.Text.RegularExpressions;

namespace Task_1
{
    public static class ConsoleAppPresenter
    {
        public static void RunApp()
        {
            while (true)
            {
                ConsoleKey pressedKey = ConsoleKey.Enter;
                string? readData = null;

                while(readData is null || !Regex.IsMatch(readData, @"^#\s?[a-zA-Z ]+\s?#cardNumber=(""[0-9 ]{13,16}""|“[0-9 ]{13,16}”)"))
                {
                    Console.Write("Enter a card type and a card number example: #American Express #cardNumber=“378282246310005”\n-> ");
                    readData = Console.ReadLine();
                }

                IPaymentValidator paymentValidator = new LiteralPaymentValidator();
                PaymentCard? paycard = null;
                try
                {
                    paycard = paymentValidator.ValidateCardNumber(readData);
                }

                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    continue;
                }

                if (paycard is not null)
                {
                    Console.WriteLine(paycard);
                    Console.WriteLine("Press Escape to exit or any key to repeat the action");
                    pressedKey = Console.ReadKey(true).Key;
                    if (pressedKey == ConsoleKey.Escape) break;
                }
            }
        }
    }
}
