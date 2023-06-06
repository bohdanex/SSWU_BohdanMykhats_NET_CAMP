namespace Task_2
{
    public class NovaPoshtaDeliveryCalculator : DeliveryPriceCalculator
    {
        public decimal GetBasePrice(float weight, float height, float width, float length)
        {
            return Convert.ToDecimal((weight * 0.7f) + (height * 1.6) + (width * 1.6) + (length * 1.6)) + 120;
        }

        public decimal GetClothingPrice(Season season, Gender gender)
        {
            decimal price = 0;
            int currentSeason = (int)Math.Ceiling((double)(12 / DateTime.Now.Month));
            if((int)season == currentSeason)
            {
                price += 100;
            }

            return price;
        }

        public decimal GetElectronicsPrice(float height, float width, float length)
        {
            decimal price = 0;
            if (height >= 100 || width >= 100 || length >= 100)
            {
                price += (decimal)((height * 0.2f) + (width * 0.2f) + (length * 0.2f));
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
