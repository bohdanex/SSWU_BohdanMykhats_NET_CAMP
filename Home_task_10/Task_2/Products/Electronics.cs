namespace Task_2
{
    public class Electronics : ProductBase
    {

        public Electronics(float weight, float width, float height, float length, string name)
            : base(weight, width, height, length, name) { }

        public override decimal AcceptPriceCalculator(DeliveryPriceCalculator deliveryPriceCalculator)
        {
            return deliveryPriceCalculator.GetBasePrice(weight, height, width, length) +
                    deliveryPriceCalculator.GetElectronicsPrice(height, width, length);
        }
    }
}