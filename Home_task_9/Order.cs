using System;
using System.Collections.Generic;

namespace Home_task_9
{
    public class Order
    {
        public Order(IEnumerable<KeyValuePair<Food, int>> meals, Guid id)
        {
            Meals = new List<KeyValuePair<Food, int>>(meals);
            Id = id;
        }

        public List<KeyValuePair<Food, int>> Meals { get; set; }
        public Guid Id { get; init; }
    }
}
