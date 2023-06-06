namespace Task_2
{
    public class Clothing : ProductBase
    {
        public Season Season { get; set; }
        public Gender Gender{ get; set; }

        public Clothing(float weight, float width, float height, float length, string name, Season season, Gender gender)
            : base(weight, width, height, length, name)
        {
            Season = season;
            Gender = gender;
        }

        public override decimal AcceptPriceCalculator(DeliveryPriceCalculator deliveryPriceCalculator)
        {
            return deliveryPriceCalculator.GetBasePrice(weight, height, width, length) + 
                deliveryPriceCalculator.GetClothingPrice(Season, Gender);
        }
    }
}