namespace Home_task_7
{
    public class Crossing
    {
        public Road[] Roads { get; set; }

        public Crossing(Road[] roads, byte minCount = 4, byte maxCount = 4)
        {
            Roads = Verifier.ValidateRoadCount(roads, minCount, maxCount);
        }

        protected static class Verifier
        {
            public static Road[] ValidateRoadCount(Road[] roads, byte minCount, byte maxCount)
            {
                if (roads.Length < minCount || roads.Length > maxCount)
                {
                    throw new ArgumentException($"Road count should be more than {minCount} and less than {maxCount}");
                }

                return (Road[])roads.Clone();
            }
        }
    }
}
