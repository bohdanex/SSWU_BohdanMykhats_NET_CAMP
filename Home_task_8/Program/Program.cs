using CrossRoads.Factory;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace CrossRoads
{
    internal class Program
    {
        private static void Main()
        {
            //MyClass myClass = new MyClass() { Id = 1, Name = "Mykola" };
            //Type myClassType = typeof(MyClass);
            //const string path = @"../../../myClass.txt";

            //using Stream stream = File.OpenWrite(path);

            //BinaryFormatter bf = new BinaryFormatter();
            //bf.Serialize(stream, myClass);

            //stream.Close();

            //MyClass myClassDeserealized = (MyClass)bf.Deserialize(File.OpenRead(path));
            //Console.WriteLine(myClass);
            //MyClass myClass = new MyClass() { Id = 1, Name = "Mykola" };
            //string jsonMyClass = JsonConvert.SerializeObject(myClass);
            //MyClass fromJson = JsonConvert.DeserializeObject<MyClass>(jsonMyClass);
            //Console.WriteLine(fromJson);

            MyClass[] myClasses =
            {
                new(){ Id = 1, Name = "Class1" },
                new(){ Id = 2, Name = "Class2" },
                new(){ Id = 3, Name = "Class3" },
                new(){ Id = 4, Name = "Class4" },
            };

            MyClassCollection classCollection = new(myClasses);
            classCollection.Deserealize(classCollection.JsonSerialized());

            string test = JsonConvert.SerializeObject(classCollection);
            Console.WriteLine(test);
            Console.WriteLine(classCollection);

            string collectionAsJson = JsonConvert.SerializeObject(classCollection);
            Console.WriteLine(collectionAsJson);

            MyClassCollection collectionFromJson = JsonConvert.DeserializeObject<MyClassCollection>(collectionAsJson);
            Console.WriteLine(collectionFromJson);





            //Console.OutputEncoding = System.Text.Encoding.UTF8;

            //try
            //{
            //    IntersectionFactory intersectionFactory1 = new IntersectionFactoryVehicular();
            //    IntersectionFactory intersectionFactory2 = new IntersectionFactoryVehicular();

            //    CrossingRoadSimulator simulator = new(intersectionFactory1, intersectionFactory2);
            //    simulator.Simulate(StartMessage, ReadCommand, CurrentInfo, SetTimer);
            //}
            //catch (Exception ex)
            //{
            //    Console.WriteLine(ex);
            //}
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

        private static TimeSpan SetTimer(string message)
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

        private static int EnterLaneCount(string laneCountInfo)
        {
            bool isValidData = false;
            int laneCount = 0;

            while (!isValidData || laneCount < 1)
            {
                Console.Write(laneCountInfo);
                isValidData = int.TryParse(Console.ReadLine(), out laneCount);
            }
            
            return laneCount;
        }

        private static Direction SetTrafficLightDirection(string directionTextInfo)
        {
            string? readConsoleData = null;
            bool isValidData = false;
            Direction direction = default;

            while(readConsoleData is null || !isValidData || direction is 0)
            {
                Console.Write(directionTextInfo);
                readConsoleData = Console.ReadLine();

                if (readConsoleData is not null && readConsoleData.Contains(','))
                {
                    string[] directions = readConsoleData.Split(',', StringSplitOptions.TrimEntries);
                    readConsoleData = string.Join(", ", directions);
                }

                isValidData = Enum.TryParse<Direction>(readConsoleData, true, out direction);
            }

            return direction;
        }
    }
}