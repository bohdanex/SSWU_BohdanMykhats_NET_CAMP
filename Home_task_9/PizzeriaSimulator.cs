using System;
using System.Collections.Generic;

namespace Home_task_9
{
    internal class PizzeriaSimulator
    {
        private Queue<Order> orders;
        private Dictionary<string, Cook> cooks;
        private string? firstCooker;
        public event Action<string>? NotifyState;

        public PizzeriaSimulator(IEnumerable<Order> orders, IDictionary<string, Cook> cooks)
        {
            this.orders = new Queue<Order>(orders);
            this.cooks = new Dictionary<string, Cook>(cooks);

            firstCooker = null;
            foreach (var kvp in this.cooks)
            {
                firstCooker ??= kvp.Key;
                kvp.Value.DishIsReady += OnFoodDone;
            }
        }

        public void Simulate()
        {
            Order? order = null;

            if (orders.Count != 0)
            {
               order = orders.Peek();
            }

            if(order != null)
            {
                for (int i = 0; i < order.Meals.Count; ++i)
                {
                    Food key = order.Meals[i].Key;
                    int value = order.Meals[i].Value;
                    bool canHandle = false;

                    if (value > 0)
                    {
                        canHandle = cooks[firstCooker!].TryStartCooking(key);
                    }

                    if (canHandle)
                    {
                        order.Meals[i] = new KeyValuePair<Food, int>(key, --value);
                    }
                }
            }

            UpdateOrders();
        }

        private void UpdateOrders()
        {
            bool allZero = true;
            Order? order = null;

            if (orders.Count != 0)
            {
                order = orders.Peek();
            }

            foreach (var meal in order.Meals)
            {
                if(meal.Value > 0)
                {
                    allZero = false;
                }
            }

            if(allZero == true)
            {
                NotifyState?.Invoke(order.Id.ToString() + " order is done!");
                orders.Dequeue();
            }
        }

        private void OnFoodDone(Cook cooker)
        {
            Food cookedFood = cooker.MealToCook!;
            NotifyState?.Invoke($"{cooker} has cooked {cookedFood}");
            Simulate();
        }

        public void AddCook(Cook cook)
        {
            bool isInsertable = cooks.TryAdd(cook.Surname, cook);

            if (!isInsertable)
            {
                NotifyState?.Invoke("Can not add this member");
            }

            Simulate();
        }

        public void RemoveCook(Cook cook)
        {
            cooks.Remove(cook.Surname);
        }

        public void AddOrder(Order order)
        {
            NotifyState?.Invoke("New order with id: " + order.Id.ToString());
            orders.Enqueue(order);
            Simulate();
        }
    }
}
