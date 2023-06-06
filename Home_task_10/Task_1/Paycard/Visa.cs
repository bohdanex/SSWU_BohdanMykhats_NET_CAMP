namespace Task_1
{
    public class Visa : PaymentCard
    {
        public Visa(string cardNumber, string holderName, byte[] cvv, decimal balance)
                    : base(cardNumber, holderName, cvv, balance) { }

        public override IEnumerable<string> CanStartWith => new string[] { "4" };
        public override IEnumerable<byte> AllowedLength => new byte[] { 13, 16 };
    }
}
