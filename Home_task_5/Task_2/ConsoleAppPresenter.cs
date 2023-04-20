using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task_2
{
    internal static class ConsoleAppPresenter
    {
        public static void RunExample()
        {
            List<Item> coffees = GetCoffees();
            List<Item> goods = GetFood();
            List<Item> pcPeriphery = GetPC_Periphery();
            List<Item> laptops = GetLaptops();
            Box coffeeBox = new("Coffeteria", coffees);
            Box goodsBox = new("Goods",goods);
            Box products = new("Products",null);
            products.AddItem(coffeeBox);
            products.AddItem(goodsBox);

            Box boxPeriphery = new("Computers", pcPeriphery);
            Box boxLaptops = new("Laptops", laptops);

            Box electronics = new("Electronics", null);
            electronics.AddItem(boxPeriphery);
            electronics.AddItem(boxLaptops);

            Box supermarket = new("Supermarket", null);
            supermarket.AddItem(electronics);
            supermarket.AddItem(products);

            Mall mall = new(supermarket);
            try
            {
                Console.WriteLine(mall.ToString(true));
            }
            catch(ArgumentNullException ex)
            {
                Console.WriteLine(ex);
            }
            catch (Exception ex2)
            {
                Console.WriteLine(ex2);
            }
        }

        public static void RunUserApp()
        {
            Console.WriteLine("Please enter a shop structure");
            Console.WriteLine("e.g Mall_Name>Subsection>product1(2 3 4), keyboard(2 10 6)");
            string? userData = null;

            while (true)
            {
                Console.Write("-> ");
                userData = Console.ReadLine();
                if (!string.IsNullOrEmpty(userData) || !userData!.Contains("/") || !userData!.Contains("(") || !userData!.Contains(")"))
                {
                    break;
                }
                Console.WriteLine("Enter a valid shop structure");
            }

            Mall? mall = new Mall(MallManager.ZipBoxes(userData)!);
            if(mall is null)
            {
                throw new ArgumentNullException(nameof(Mall));
            }
            try
            {
                Console.WriteLine(mall.ToString(true));
            }
            catch (ArgumentNullException ex)
            {
                Console.WriteLine(ex);
            }
            catch(Exception ex2)
            {
                Console.WriteLine(ex2);
            }
            
            string? productsToFind;
            while (true)
            {
                Console.Write("Enter a path/s to the products (coma separated): ");
                productsToFind = Console.ReadLine();
                if (!string.IsNullOrEmpty(userData))
                {
                    break;
                }
                Console.WriteLine("Enter a valid product");
            }
            string[] paths = productsToFind!.Split(',', StringSplitOptions.TrimEntries);

            Console.WriteLine(MallManager.FindPathToProduct(mall.MainBox, paths));
        }

        private static List<Item> GetCoffees()
        {
            return new List<Item>
            {
                new Product("Kava-Robusta",20, 8, 4),
                new Product("Arabica",10, 4, 4),
                new Product("Coffee La more",20, 8, 4),
                new Product("Intella kava",30, 10, 4),
                new Product("Java'bs",20, 8, 4),
            };
        }

        private static List<Item> GetFood()
        {
            return new List<Item>
            {
                new Product("Bread Ahtirka",3,5,5),
                new Product("Ice Cream - Limi",9,5,5),
                new Product("Ice Verden - Limi",12,5,5),
                new Product("Shake",9,4,4),
                new Product("Butter",3,5,3),
                new Product("DelAlcohol",9,5,5),
                new Product("Vine",15,4,4),
            };
        }

        private static List<Item> GetPC_Periphery()
        {
            return new List<Item>
            {
                new Product("Mouse",4,5,5),
                new Product("Keyboard",2,12,5),
                new Product("Monitor",50,50,5),
                new Product("Processor",1,2,2),
            };
        }

        private static List<Item> GetLaptops()
        {
            return new List<Item>
            {
                new Product("Azuz TUK 505D1",20,25,20),
                new Product("Abbple MacBook maxi",25,30,30),
                new Product("Kiki",18,25,20),
                new Product("GL lg",22,27,21),
            };
        }
    }
}
