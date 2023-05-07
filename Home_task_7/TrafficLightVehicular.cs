using System.Collections;
using System.Collections.Generic;

namespace Home_task_7
{
    public sealed class TrafficLightVehicular : TrafficLight, IFormattable
    {
        private event Action<Direction>? IndicatorSwitched;
        private Queue<MovementParticipant> participants;
        private bool incrementIndex;

        public TrafficLightVehicular(Direction position, TrafficLightIndicator[] trafficLightIndicators, IEnumerable participants)
        {
            Position = position;
            TrafficLightIndicators = (TrafficLightIndicator[])IndicatorVerifier.VerifyIndicatorCount(trafficLightIndicators).Clone();

            Reboot();

            foreach (MovementParticipant participant in participants)
            {
                AddParticipant(participant);
            }
        }

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

            TrafficLightIndicators[activeIndicatorIndex].IsActive = true;
            timer = TrafficLightIndicators[activeIndicatorIndex].Duration;
        }

        public override void Start()
        {
            if(participants.Count > 0)
            {
                IndicatorSwitched?.Invoke(TrafficLightIndicators[activeIndicatorIndex].DirectionTo);
                MovementParticipant firstParticipant = participants.Peek();
                if (firstParticipant.Intension == TrafficLightIndicators[activeIndicatorIndex].DirectionTo)
                {
                    IndicatorSwitched -= firstParticipant.ReactToIndicator;
                    participants.Dequeue();
                }
            }
            
            timer -= TimeSpan.FromSeconds(1);
            if(timer <= TimeSpan.FromSeconds(0))
            {
                SwitchIndicatorActivity();
            }
        }

        public override void Reboot()
        {
            participants = new();
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

        public override void AddParticipant(MovementParticipant participant)
        {
            MovementParticipant clonedParticipant = (MovementParticipant)participant.Clone();

            if (clonedParticipant.CurrentDirection != Position)
            {
                return;
            }

            bool matchFinalDirection = false; 
            foreach (TrafficLightIndicator indicator in TrafficLightIndicators)
            {
                if(clonedParticipant.Intension == indicator.DirectionTo)
                {
                    matchFinalDirection = true;
                }
            }

            if (!matchFinalDirection)
            {
                return;
            }

            IndicatorSwitched += clonedParticipant.ReactToIndicator;
            participants.Enqueue(clonedParticipant);
        }

        public override string ToString()
        {
            return Position.ToString();
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
