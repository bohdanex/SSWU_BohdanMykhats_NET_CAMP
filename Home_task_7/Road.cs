namespace Home_task_7
{
    public sealed class Road
    {
        public Lane[] Lanes { get; init; }

        public Road(Lane[] lanes)
        {
            Lanes = (Lane[])lanes.Clone();
        }
    }

    public class Lane
    {
        public Lane(bool isOpen, TrafficLight? trafficLight, Direction allowedDirections = 0)
        {
            IsOpen = isOpen;
            TrafficLight = (TrafficLight?)trafficLight?.Clone();
            AllowedDirections = allowedDirections;
        }

        public bool IsOpen { get; set; }
        public TrafficLight? TrafficLight { get; set; }
        public Direction AllowedDirections { get; set; }
    }
}
