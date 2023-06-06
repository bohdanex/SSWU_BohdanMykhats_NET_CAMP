namespace Task_2
{
    public class OnlineShopSystem
    {
        private List<ProductBase> products;
        public DeliveryPriceCalculator DeliveryPriceCalculator { get; set; }

        public OnlineShopSystem(IEnumerable<ProductBase> products, DeliveryPriceCalculator deliveryCalculator)
        {
            this.products = products.ToList();
            DeliveryPriceCalculator = deliveryCalculator;
        }

        public void AddProduct(ProductBase product)
        {
            products.Add(product);
        }

        public IEnumerable<string> CalculateDeliveryPrice(params int[] productIndices)
        {
            foreach (int index in productIndices)
            {
                yield return $"{products[index].Name}, " +
                    $"delivery price: {products[index].AcceptPriceCalculator(DeliveryPriceCalculator):C}";
            }
        }

        public IEnumerator<string> GetEnumerator()
        {
            for (int i = 0; i < products.Count; i++)
            {
                yield return $"{i}. {products[i].Name}";
            }
        }
    }
}
