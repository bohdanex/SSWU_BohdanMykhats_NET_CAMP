namespace Task_1
{
    public interface IPaymentValidator
    {
        PaymentCard? ValidateCardNumber(string cardInfo);
    }
}
