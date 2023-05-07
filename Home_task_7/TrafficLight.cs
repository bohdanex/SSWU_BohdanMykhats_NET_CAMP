namespace Home_task_7
{
    public abstract class TrafficLight : IFormattable
    {
        public abstract TrafficLightIndicator[] TrafficLightIndicators { get; set; }

        public abstract void Start();
        public abstract void Reboot();

        public abstract void AddParticipant(MovementParticipantVehicular participant);

        protected static class IndicatorVerifier
        {
            public static TrafficLightIndicator[] VerifyIndicatorCount(TrafficLightIndicator[] indicators, byte minCount = 2)
            {
                if (indicators.Length < minCount)
                {
                    throw new ArgumentException("Indicator count must be more than " + (minCount - 1));
                }

                return indicators;
            }
        }

        public abstract string ToString(string? format, IFormatProvider? provider);
    }
}
