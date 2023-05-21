using System;
using System.Collections.Generic;
using System.Threading;

namespace Home_task_9
{
    internal static class ProgramPresenter
    {
        internal static void RunConsoleApp()
        {
            Cook pizzaCooker1 = new PizzaCook("Krauf");
            Cook pizzaCooker2 = new PizzaCook("Frenk", pizzaCooker1);
            Cook delicaciesCooker = new DelicaciesCook("Yeager", pizzaCooker2);
            Cook barman = new Barman("Slaypoz", delicaciesCooker);

            IDictionary<string, Cook> workers = new Dictionary<string, Cook>
            {
                {barman.Surname, barman},
                {delicaciesCooker.Surname, delicaciesCooker},
                {pizzaCooker2.Surname, pizzaCooker2},
                {pizzaCooker1.Surname, pizzaCooker1},
            };

            Food pizza1 = new Pizza("Mexicana", TimeSpan.FromSeconds(3));
            Food pizza2 = new Pizza("Margarita", TimeSpan.FromSeconds(4));
            Food bear = new Drink("Bear", TimeSpan.FromSeconds(1));
            Food iceCream = new Delicacies("Choco Ice Cream", TimeSpan.FromSeconds(1));

            List<KeyValuePair<Food, int>> listOfFood = new()
            {
                new KeyValuePair<Food, int>(bear, 5),
                new KeyValuePair<Food, int>(pizza1, 3),
                new KeyValuePair<Food, int>(pizza2, 1),
                new KeyValuePair<Food, int>(iceCream, 5),
            };

            Order order = new(listOfFood, Guid.NewGuid());
            List<Order> orders = new() { order };

            PizzeriaSimulator pizzeria = new(orders, workers);
            pizzeria.NotifyState += Print;
            pizzeria.Simulate();
            Thread.CurrentThread.Join(40000);
        }

        private static void Print(string message)
        {
            Console.WriteLine(message);
        }
    }
}
