namespace CrossRoads
{
    public class TrafficLightIndicator : ICloneable
    {
        public TrafficLightIndicator()
        {
            IsActive = false;
            Color = IndicatorColor.Red;
            AllowedDirections = Direction.None;
            Duration = TimeSpan.FromSeconds(5);
        }

        public TrafficLightIndicator(IndicatorColor indicatorColor, Direction directionTo,TimeSpan duration , bool isActive = false)
        {
            IsActive = isActive;
            Color = indicatorColor;
            AllowedDirections = directionTo;
            Duration = duration;
        }

        public bool IsActive { get; set; }
        public IndicatorColor Color { get; init; }
        public Direction AllowedDirections { get; set; }
        public TimeSpan Duration { get; set; }

        public object Clone()
        {
            return MemberwiseClone();
        }
        
        public override string ToString()
        {
            return AllowedDirections.TranslateToUkrainian();
        }
    }
}
