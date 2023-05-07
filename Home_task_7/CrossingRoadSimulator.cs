using System.Threading;
using System.Text;

namespace Home_task_7
{
    public class CrossingRoadSimulator
    {
        private TimeSpan timer;
        private readonly IEnumerable<MovementParticipant> participants;

        public CrossingRoadSimulator(IEnumerable<MovementParticipant> movementParticipants)
        {
            participants = movementParticipants;
            timer = TimeSpan.FromSeconds(0);
            TrafficLights = Array.Empty<TrafficLight>();
            SetDefaults();
        }

        public TrafficLight[] TrafficLights { get; private set; }

        public void Simulate(Action StartMessage, Func<Command> ReadCommand, Action<string> Output, Func<string, TimeSpan> GetSeconds)
        {
            StartMessage();
            bool isPaused = false;
            bool isStarted = false;
            Command command = 0;

            Thread simulationThread = new(delegate()
            {
                while (true)
                {
                    if (isPaused is false)
                    {
                        StringBuilder firstRow = new($"{"Світлофор",-10}: ");
                        StringBuilder secondRow = new($"{"Колір",-10}: ");
                        Thread.CurrentThread.Join(1000);

                        foreach (var light in TrafficLights)
                        {
                            firstRow.Append($"{light,23:direction}|");
                            secondRow.Append($"{" ",6}{light,-17:color}|");
                            light.Start();
                        }

                        secondRow.Append('\n' + new string('-', 108));
                        Output(ToString() + '\n' + firstRow.ToString() + '\n' + secondRow.ToString());
                        timer += TimeSpan.FromSeconds(1);
                    }
                }
            });            

            while (true)
            {
                command = ReadCommand();
                if(command is Command.Start && isStarted is false)
                {
                    simulationThread.Start();
                    isStarted = true;
                }
                else if(isStarted is true)
                {
                    if (command is Command.Abort)
                    {
                        Environment.Exit(0);
                    }
                    else if (command is Command.Pause)
                    {
                        isPaused = !isPaused;
                    }
                }

                if (command is Command.ChangeRooles)
                {
                    isPaused = true;
                    for (int i = 0; i < TrafficLights[0].TrafficLightIndicators.Length; ++i)
                    {
                        TimeSpan timeSpan = GetSeconds($"Встановіть значення для індикатора {TrafficLights[0].TrafficLightIndicators[i].Color} кольору ");
                        TrafficLights[0].TrafficLightIndicators[i].Duration = timeSpan;
                        TrafficLights[1].TrafficLightIndicators[i].Duration = timeSpan;
                        TrafficLights[2].TrafficLightIndicators[^(i + 1)].Duration = timeSpan;
                        TrafficLights[3].TrafficLightIndicators[^(i + 1)].Duration = timeSpan;
                    }

                    foreach (TrafficLight light in TrafficLights)
                    {
                        light.Reboot();
                    }

                    isPaused = false;
                }
            }
        }

        public void SetDefaults()
        {
            TrafficLightIndicator[] indicatorsToEast =
            {
                new(IndicatorColor.Red, Direction.None, TimeSpan.FromSeconds(20), false),
                new(IndicatorColor.Yellow, Direction.None, TimeSpan.FromSeconds(2), false),
                new(IndicatorColor.Green, Direction.East, TimeSpan.FromSeconds(20), true),
            };

            TrafficLightIndicator[] indicatorsToWest =
            {
                new(IndicatorColor.Red, Direction.None, TimeSpan.FromSeconds(20), false),
                new(IndicatorColor.Yellow, Direction.None, TimeSpan.FromSeconds(2), false),
                new(IndicatorColor.Green, Direction.West, TimeSpan.FromSeconds(20), true),
            };
            
            TrafficLightIndicator[] indicatorsToNorth =
            {
                new(IndicatorColor.Red, Direction.None, TimeSpan.FromSeconds(20), true),
                new(IndicatorColor.Yellow, Direction.None, TimeSpan.FromSeconds(2), false),
                new(IndicatorColor.Green, Direction.North, TimeSpan.FromSeconds(20), false),
            };

            TrafficLightIndicator[] indicatorsToSouth =
            {
                new(IndicatorColor.Red, Direction.None, TimeSpan.FromSeconds(20), true),
                new(IndicatorColor.Yellow, Direction.None, TimeSpan.FromSeconds(2),false),
                new(IndicatorColor.Green, Direction.South, TimeSpan.FromSeconds(20), false),
            };

            TrafficLights = new TrafficLightVehicular[]
            {
                new(Direction.West, indicatorsToEast, participants),
                new(Direction.East, indicatorsToWest, participants),
                new(Direction.South, indicatorsToNorth, participants),
                new(Direction.North, indicatorsToSouth, participants),
            };
        }

        public override string ToString()
        {
            return $"t={timer.TotalSeconds} c.";
        }

        protected static class CrossingVerifier
        {
            public static TrafficLightVehicular[] VerifyTraficLightCount(TrafficLightVehicular[] trafficLights, byte count = 4)
            {
                if (trafficLights.Length != count)
                {
                    throw new ArgumentException("The traffic light count must equal " + count);
                }

                return (TrafficLightVehicular[])trafficLights.Clone();
            }
        }
    }
}
