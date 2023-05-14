namespace CrossRoads
{
    public class Lane : ICloneable
    {
        public Lane()
        {
            IsOpen = true;
            TrafficLight = null;
            AllowedDirections = Direction.None;
            MovementParticipants = null;
        }

        public Lane(bool isOpen, TrafficLight? trafficLight, Direction allowedDirections = 0,
            IEnumerable<MovementParticipant>? movementParticipants = null)
        {
            IsOpen = isOpen;
            TrafficLight = (TrafficLight?)trafficLight?.Clone();
            AllowedDirections = allowedDirections;

            if (movementParticipants is not null)
            {
                MovementParticipants = new Queue<MovementParticipant>(movementParticipants);

                if(TrafficLight is not null)
                {
                    foreach (MovementParticipant movementParticipant in MovementParticipants)
                    {
                        TrafficLight.IndicatorSwitched += movementParticipant.ReactToIndicator;
                    }
                }
            }
        }

        public bool IsOpen { get; set; }
        public TrafficLight? TrafficLight { get; set; }
        public Direction AllowedDirections { get; set; }
        public Queue<MovementParticipant>? MovementParticipants { get; init; }


        public object Clone()
        {
            return new Lane(IsOpen, (TrafficLight?)TrafficLight?.Clone(), AllowedDirections);
        }

        public override string ToString()
        {
            if(TrafficLight is null)
            {
                return AllowedDirections.TranslateToUkrainian();
            }

            return TrafficLight.ToString("direction", null);
        }
    }
}
