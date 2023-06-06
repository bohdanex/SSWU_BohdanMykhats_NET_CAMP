namespace Task_1
{
    public class MasterCard : PaymentCard
    {
        public MasterCard(string cardNumber, string holderName, byte[] cvv, decimal balance)
            : base(cardNumber, holderName, cvv, balance) { }

        public override IEnumerable<string> CanStartWith => new string[] { "51", "52", "53", "54", "55" };
        public override IEnumerable<byte> AllowedLength => new byte[] { 16 };
    }
}
