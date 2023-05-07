namespace Home_task_7
{
    public class TrafficLightIndicator
    {
        public TrafficLightIndicator()
        {
            IsActive = false;
            Color = IndicatorColor.Red;
            DirectionTo = Direction.None;
            Duration = TimeSpan.FromSeconds(5);
        }

        public TrafficLightIndicator(IndicatorColor indicatorColor, Direction directionTo,TimeSpan duration , bool isActive = false)
        {
            IsActive = isActive;
            Color = indicatorColor;
            DirectionTo = directionTo;
            Duration = duration;
        }

        public bool IsActive { get; set; }
        public IndicatorColor Color { get; init; }
        public Direction DirectionTo { get; init; }
        public TimeSpan Duration { get; set; }
    }
}
