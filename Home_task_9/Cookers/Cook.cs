using System;
using System.Timers;

namespace Home_task_9
{
    public abstract class Cook
    {
        protected Timer timer;
        public event Action<Cook>? DishIsReady;
        //ланцюг обов'язків: посилання на наступного кухара
        protected Cook? nextToCooker;

        public Cook(string surname, Cook? nextToCooker = null)
        {
            timer = new();
            timer.AutoReset = false;
            Surname = surname;
            this.nextToCooker = nextToCooker;
            timer.Elapsed += OnTimerElapsed;
            IsBusy = false;
        }

        public virtual Food? MealToCook { get; set; }
        public virtual bool IsBusy { get; protected set; }
        public virtual string Surname { get; init; }

        public abstract bool TryStartCooking(Food foodToCook);

        protected virtual void OnDishIsReady()
        {
            if (MealToCook is not null)
            {
                IsBusy = false;
                DishIsReady?.Invoke(this);
            }
        }
        
        protected virtual void OnTimerElapsed(object? sender, ElapsedEventArgs? args)
        {
            OnDishIsReady();
        }

        public override string ToString()
        {
            return Surname;
        }
    }
}
