using System;
using System.Reflection;

namespace Task_1
{
    public class LiteralPaymentValidator : IPaymentValidator
    {
        public PaymentCard? ValidateCardNumber(string cardInfo)
        {
            int firstSharpIndex = cardInfo.IndexOf('#');
            int secondSharpIndex = cardInfo.IndexOf('#', firstSharpIndex + 1);
            int firstQuoteIndex;
            int secondQuoteIndex;

            if (cardInfo.Contains('“'))
            {
                firstQuoteIndex = cardInfo.IndexOf('“');
                secondQuoteIndex = cardInfo.IndexOf('”');
            }
            else if (cardInfo.Contains('"'))
            {
                firstQuoteIndex= cardInfo.IndexOf("\"");
                secondQuoteIndex= cardInfo.LastIndexOf("\"");
            }
            else
            {
                throw new Exception("Use quotes(\"card_num\" or “card_num”) to determine card number");
            }

            string cardTypeText = cardInfo[(firstSharpIndex + 1) .. secondSharpIndex].Replace(" ", "");
            string cardNumber = cardInfo[(firstQuoteIndex +1) .. secondQuoteIndex].Replace(" ", "");

            Type? cardType = Type.GetType("Task_1." + cardTypeText);
            if(cardType is null)
            {
                throw new Exception("Card type does not exist in current system");
            }
            
            if (!Luna.IsValidNumbers(cardNumber))
            {
                throw new Exception("Wrong card number. Defined by Lune algorithm");
            }

            PaymentCard paymentCard = (PaymentCard)Activator
                                      .CreateInstance(cardType, cardNumber, Guid.NewGuid().ToString(), new byte[] {1,4,1}, 0m)!;

            if (!paymentCard.AllowedLength.Contains((byte)cardNumber.Length))
            {
                return null;
            }

            foreach (string s in paymentCard.CanStartWith)
            {
                if (cardNumber.StartsWith(s))
                {
                    return paymentCard;
                }
            }

            return null;
        }
    }
}
