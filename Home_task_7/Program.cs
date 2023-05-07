namespace Home_task_7
{
    internal class Program
    {
        private static void Main()
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            try
            {
                MovementParticipant[] participants =
                {
                    new MovementParticipantVehicular(Guid.NewGuid(), Direction.West, Direction.East, false),
                    new MovementParticipantVehicular(Guid.NewGuid(), Direction.West, Direction.East, false),
                    new MovementParticipantVehicular(Guid.NewGuid(), Direction.West, Direction.East, true),
                };

                CrossingRoadSimulator simulator = new(participants);
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
                ConsoleKey.Spacebar => Command.ChangeRooles,
                _ => Command.None
            };
        }

        private static void CurrentInfo(string info)
        {
            Console.WriteLine(info);
        }

        private static TimeSpan GetSeconds(string message)
        {
            Console.Write(message);
            int dataFromConsole = 0;

            while (dataFromConsole < 2)
            {
                dataFromConsole = Int32.Parse(Console.ReadLine()!);
            }

            return TimeSpan.FromSeconds(dataFromConsole);
        }
    }
}