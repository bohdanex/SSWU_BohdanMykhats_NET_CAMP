namespace Home_task_7
{
    internal class Program
    {
        private static MovementParticipant[] participants =
                {
                    new MovementParticipantVehicular(Guid.NewGuid(), Direction.West, Direction.East, false),
                    new MovementParticipantVehicular(Guid.NewGuid(), Direction.West, Direction.East, false),
                    new MovementParticipantVehicular(Guid.NewGuid(), Direction.West, Direction.East, true),
                };

        private static void Main()
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            try
            {
                CrossingRoadSimulator simulator = new(participants, GetMockTraffic());
                simulator.Simulate(StartMessage, ReadCommand, CurrentInfo, GetSeconds);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        private static void StartMessage()
        {
            Console.WriteLine("Оберіть опцію натиснувши клавішу на клавіатурі" +
                "\n1. 'ENTER' - почати симуляцію" +
                "\n2. 'P' - пауза/продовжити" +
                "\n3. 'A' - перервати/вихід з програми" +
                "\n4. 'SPACE' - змінити правила перемикання індикаторів світлофору\n");
        }

        private static Command ReadCommand()
        {
            ConsoleKey key = Console.ReadKey().Key;
            return key switch
            {
                ConsoleKey.Enter => Command.Start,
                ConsoleKey.A => Command.Abort,
                ConsoleKey.P => Command.Pause,
                ConsoleKey.Spacebar => Command.ChangeRules,
                _ => Command.None
            };
        }

        private static void CurrentInfo(string info)
        {
            Console.WriteLine(info);
        }

        private static TimeSpan GetSeconds(string message)
        {
            int dataFromConsole;
            do
            {
                Console.Write(message);
                dataFromConsole = Int32.Parse(Console.ReadLine()!);
            }
            while (dataFromConsole < 2);

            return TimeSpan.FromSeconds(dataFromConsole);
        }

        private static TrafficLight[] GetMockTraffic()
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

            return new TrafficLightVehicular[]
            {
                new(Direction.West, indicatorsToEast, participants),
                new(Direction.East, indicatorsToWest, participants),
                new(Direction.South, indicatorsToNorth, participants),
                new(Direction.North, indicatorsToSouth, participants),
            };
        }
    }
}