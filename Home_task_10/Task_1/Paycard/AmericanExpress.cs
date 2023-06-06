namespace Task_1
{
    public class AmericanExpress : PaymentCard
    {
        public AmericanExpress(string cardNumber, string holderName, byte[] cvv, decimal balance) 
            : base(cardNumber, holderName, cvv, balance) { }

        public override IEnumerable<string> CanStartWith => new string[] { "34", "37" };
        public override IEnumerable<byte> AllowedLength => new byte[] { 15 };
    }
}