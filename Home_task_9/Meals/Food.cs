using System;

namespace Home_task_9
{
    public abstract class Food
    {
        public Food(string name, TimeSpan cookTime)
        {
            Name = name;
            this.CookTime = cookTime;
        }

        public TimeSpan CookTime { get; }
        public string Name { get; init; }

        public override string ToString()
        {
            return this.GetType().Name + ": " + Name;
        }
    }
}
