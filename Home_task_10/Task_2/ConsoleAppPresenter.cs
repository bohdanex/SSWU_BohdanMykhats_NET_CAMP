namespace Task_2
{
    public static class ConsoleAppPresenter
    {
        public static void RunApp()
        {
            IEnumerable<ProductBase> products = new List<ProductBase>()
            {
                new Product(0.5f,5,20,5, "Beer", new DateTime(2023, 10, 2)),
                new Product(0.3f,2,4,2, "Butter", new DateTime(2023, 7, 12)),
                new Product(1,14,31,6, "Pasta", new DateTime(2024, 1, 01)),
                new Electronics(3,10,32,15, "Iron"),
                new Electronics(0.3f,2,4,4, "Gaming mouse"),
                new Electronics(0.2f, 2,4,3, "Office mouse"),
                new Electronics(4.05f,50,40,40, "Laptop AA12 + Ryzen 9 5300"),
                new Clothing(0.82f,45,60,0.2f, "Jeans Netkoff", Season.Spring, Gender.Male),
                new Clothing(0.415f,40,20,0.2f, "Shorts", Season.Summer, Gender.Female),
            };

            DeliveryPriceCalculator? deliveryPriceCalculator = null;
            while (deliveryPriceCalculator is null)
            {
                Console.Write("Select mail to calculate the price for delivery:" +
                    "\n 1. MeestExpress" +
                    "\n 2. Nova Poshta" +
                    "\n 3. UkrPoshta" +
                    "\n -> ");

                int choice = Convert.ToInt32(Console.ReadLine());

                if(choice is >= 1 and <= 3)
                {
                    deliveryPriceCalculator = choice switch
                    {
                        1 => new MeestExpressDeliveryCalculator(),
                        2 => new NovaPoshtaDeliveryCalculator(),
                        3 => new UkrPoshtaDeliveryCalculator()
                    };
                }
            };

            OnlineShopSystem onlineShopSystem = new(products, deliveryPriceCalculator);

            Console.WriteLine("Here is the list of all products");
            foreach (var product in onlineShopSystem)
            {
                Console.WriteLine(product);
            }

            Console.Write("Enter a product index (come separated if multiple): ");
            int[]? productIndeces = Console.ReadLine()?.Split(',', StringSplitOptions.TrimEntries)
                .Select(s => Convert.ToInt32(s)).ToArray();

            if(productIndeces is not null)
            {
                foreach (var result in onlineShopSystem.CalculateDeliveryPrice(productIndeces))
                {
                    Console.WriteLine(result);
                }
            }
        }
    }
}
