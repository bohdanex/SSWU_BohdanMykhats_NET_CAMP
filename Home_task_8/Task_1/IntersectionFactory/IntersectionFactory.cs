namespace CrossRoads.Factory
{
    public abstract class IntersectionFactory
    {
        protected Intersection? intersection;
        protected bool isCreated;

        public IntersectionFactory()
        {
            intersection = null;
            isCreated = false;
        }

        public abstract void CreateIntersection(Func<string, int> CreateLanes, Func<string, Direction> SetDirection);
        public abstract void CreateDefaultIntersection();
        public abstract void SetIndicatorTimer(Func<string, TimeSpan> SetTimer);
        public abstract void SetDefaultTimers();

        public virtual Intersection Build()
        {
            if(intersection is not null && isCreated)
            {
                return (Intersection)intersection.Clone();
            }
            throw new NullReferenceException();
        }
    }
}
