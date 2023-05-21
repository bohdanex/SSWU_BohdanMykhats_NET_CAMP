using System;
using System.Timers;

namespace Home_task_9
{
    public class Barman : Cook
    {
        public Barman(string surname, Cook? nextToCooker = null) : base(surname, nextToCooker) { }

        public override bool TryStartCooking(Food foodToCook)
        {
            if (foodToCook is Drink && !IsBusy)
            {
                MealToCook = foodToCook;
                IsBusy = true;
                timer.Interval = MealToCook!.CookTime.TotalMilliseconds;
                timer.Start();

                return true;
            }
            else if (nextToCooker is not null)
            {
                return nextToCooker.TryStartCooking(foodToCook);
            }

            return false;
        }
    }
}
