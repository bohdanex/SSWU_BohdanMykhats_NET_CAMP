using System.Collections;
using System.Collections.Generic;

namespace Home_task_7
{
    public sealed class TrafficLightVehicular : TrafficLight, IFormattable
    {
        private event Action<Direction>? IndicatorSwitched;
        private bool incrementIndex;
#pragma warning disable 8618
        public TrafficLightVehicular(Direction position, TrafficLightIndicator[] trafficLightIndicators)
        : base(position, trafficLightIndicators) { }
#pragma warning restore
        public override TrafficLightIndicator[] TrafficLightIndicators { get; init; }
        public override Direction Position { get; init; }

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

            IndicatorSwitched?.Invoke(TrafficLightIndicators[activeIndicatorIndex].DirectionTo);

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

        public override string ToString()
        {
            return Position.ToString();
        }

        //принцип Барбари Лісков (коваріантність) - повертається більш
        //конкретний тип (не TrafficLight, а TrafficLightVehicular) 
        public override object Clone()
        {
            return new TrafficLightVehicular(Position, (TrafficLightIndicator[])TrafficLightIndicators.Clone());
        }

        public override string ToString(string? format, IFormatProvider? provider)
        {
            if (format is null)
            {
                return ToString();
            }
            else if (format.ToLower() == "color" || format.ToLower() == "c")
            {
                return $"{TrafficLightIndicators[activeIndicatorIndex].Color.TranslateToUkrainian(),10}";
            }
            else if (format.ToLower() == "direction" || format.ToLower() == "d")
            {
                return $"{Position.TranslateToUkrainian(),10} - {TrafficLightIndicators[activeIndicatorIndex].DirectionTo.TranslateToUkrainian(),-10}";
            }

            return ToString();
        }
    }
}
