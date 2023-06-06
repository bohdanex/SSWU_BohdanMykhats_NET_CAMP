namespace Task_1
{
    public abstract class PaymentCard
    {
        protected string holderName;
        protected byte[] cvv;
        protected decimal balance;

        public PaymentCard(string cardNumber, string holderName, byte[] cvv, decimal balance)
        {
            CardNumber = cardNumber;
            this.holderName = holderName;
            this.cvv = cvv;
            this.balance = balance;
        }

        public virtual string CardNumber { get; set; }
        public abstract IEnumerable<string> CanStartWith { get; }
        public abstract IEnumerable<byte> AllowedLength { get; }
        public override string ToString()
        {
            return $"Card type: {this.GetType().Name}, number - {CardNumber}";
        }
    }
}
