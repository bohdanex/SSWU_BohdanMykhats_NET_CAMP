namespace CrossRoads
{
    public sealed class TrafficLightVehicular : TrafficLight, IFormattable, ICloneable
    {
        public override event Action<Direction>? IndicatorSwitched;
        private bool incrementIndex;
#pragma warning disable 8618
        public TrafficLightVehicular(TrafficLightIndicator[] trafficLightIndicators)
        : base(trafficLightIndicators) { }
#pragma warning restore
        public override TrafficLightIndicator[] TrafficLightIndicators { get; init; }

        private void SwitchIndicatorActivity()
        {
            activeIndicatorIndex += incrementIndex == true? 1 : -1;
            if(activeIndicatorIndex >= TrafficLightIndicators.Length - 1 || activeIndicatorIndex <= 0)
            {
                incrementIndex = !incrementIndex;
            }

            for (int i = 0; i < TrafficLightIndicators.Length; ++i)
            {
                TrafficLightIndicators[i].IsActive = false;
            }

            IndicatorSwitched?.Invoke(TrafficLightIndicators[activeIndicatorIndex].AllowedDirections);

            TrafficLightIndicators[activeIndicatorIndex].IsActive = true;
            timer = TrafficLightIndicators[activeIndicatorIndex].Duration;
        }

        public override void TriggerTimer()
        {
            timer -= TimeSpan.FromSeconds(1);
            if(timer <= TimeSpan.FromSeconds(0))
            {
                SwitchIndicatorActivity();
            }
        }

        public override void Reboot()
        {
            activeIndicatorIndex = 0;
            incrementIndex = true;

            for (int i = 0; i < TrafficLightIndicators.Length; ++i)
            {
                if (TrafficLightIndicators[i].IsActive)
                {
                    activeIndicatorIndex = i;
                    break;
                }
            }

            if (activeIndicatorIndex == TrafficLightIndicators.Length - 1)
            {
                incrementIndex = false;
            }

            timer = TrafficLightIndicators[activeIndicatorIndex].Duration;
        }

        public override object Clone()
        {
            return new TrafficLightVehicular((TrafficLightIndicator[])TrafficLightIndicators.Clone());
        }

        public override string ToString(string? format, IFormatProvider? provider)
        {
            if (format is null)
            {
                return ToString()!;
            }
            else if (format.ToLower() == "color" || format.ToLower() == "c")
            {
                return $"{TrafficLightIndicators[activeIndicatorIndex].Color.TranslateToUkrainian(),7}";
            }
            else if (format.ToLower() == "direction" || format.ToLower() == "d")
            {
                return $"{TrafficLightIndicators[activeIndicatorIndex],-7}";
            }

            return ToString()!;
        }
    }
}
