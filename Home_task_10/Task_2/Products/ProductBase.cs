namespace Task_2
{
    public abstract class ProductBase
    {
        protected float weight;
        protected float width;
        protected float height;
        protected float length;

        public ProductBase(float weight, float width, float height, float length, string name)
        {
            this.weight = weight;
            this.width = width;
            this.height = height;
            this.length = length;
            Name = name;
        }

        public string Name { get; init; }

        public abstract decimal AcceptPriceCalculator(DeliveryPriceCalculator deliveryPriceCalculator);
    }
}
