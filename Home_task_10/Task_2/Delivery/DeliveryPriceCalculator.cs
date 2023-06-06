namespace Task_2
{
    public interface DeliveryPriceCalculator
    {
        decimal GetBasePrice(float weight, float height, float width, float length);
        decimal GetProductPrice(DateTime expireDate, decimal basePrice);
        decimal GetClothingPrice(Season season, Gender gender);
        decimal GetElectronicsPrice(float height, float width, float length);
    }
}
