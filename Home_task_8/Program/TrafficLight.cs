namespace CrossRoads
{
    public abstract class TrafficLight : IFormattable, ICloneable
    {
        protected TimeSpan timer;
        protected int activeIndicatorIndex;
        public abstract event Action<Direction>? IndicatorSwitched;
        public TrafficLight(TrafficLightIndicator[] trafficLightIndicators)
        {
            TrafficLightIndicators = (TrafficLightIndicator[])trafficLightIndicators.Clone();
            Reboot();
        }

        public abstract TrafficLightIndicator[] TrafficLightIndicators { get; init; }

        public abstract void TriggerTimer();
        public abstract void Reboot();

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

        public abstract object Clone();
        public abstract string ToString(string? format, IFormatProvider? provider);
    }
}
