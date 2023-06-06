namespace Task_2
{
    public class MeestExpressDeliveryCalculator : DeliveryPriceCalculator
    {
        public decimal GetBasePrice(float weight, float height, float width, float length)
        {
            return Convert.ToDecimal((weight * 0.5f) + (height * 2) + (width * 2) + (length * 2));
        }

        public decimal GetClothingPrice(Season season, Gender gender)
        {
            decimal price = 30;
            int currentSeason = (int)Math.Ceiling((double)(12 / DateTime.Now.Month));
            if ((int)season == currentSeason)
            {
                price += 35;
            }

            return price;
        }

        public decimal GetElectronicsPrice(float height, float width, float length)
        {
            decimal price = 0;
            if (height >= 200 || width >= 200 || length >= 200)
            {
                price += (decimal)((height * 0.3f) + (width * 0.2f) + (length * 0.2f));
            }

            return price;
        }

        public decimal GetProductPrice(DateTime expireDate, decimal basePrice)
        {
            int days = (DateTime.Now - expireDate).Days;

            return days switch
            {
                <= 0 => basePrice * 0.1m,
                > 0 and < 4 => basePrice * 0.5m,
                _ => basePrice
            };
        }
    }
}
