namespace Task_2
{
    public class Product : ProductBase
    {
        public DateTime ExpireDate { get; init; }

        public Product(float weight, float width, float height, float length, string name, DateTime expireDate) 
            : base(weight, width, height, length, name)
        {
            ExpireDate = expireDate;
        }

        public override decimal AcceptPriceCalculator(DeliveryPriceCalculator deliveryPriceCalculator)
        {
            return deliveryPriceCalculator
                .GetProductPrice(ExpireDate, deliveryPriceCalculator.GetBasePrice(weight, height, width, length));
        }
    }
}