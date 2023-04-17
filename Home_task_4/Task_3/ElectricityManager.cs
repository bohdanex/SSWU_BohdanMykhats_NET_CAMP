namespace Task_3
{
    public class ElectricityManager
    {
        public ElectricityUsage? ElectricityUsage { get; private set; }

        public ElectricityManager()
        {
            ElectricityUsage = null;
        }

        public ElectricityManager(ElectricityUsage electricityUsageInfo)
        {
            ElectricityUsage = electricityUsageInfo;
        }

        public void ReadFile(uint year, byte quarter, IParser parser)
        {
            string filePath = Path.Combine(@"C:\DevTools\vs_projects\SSWU_BohdanMykhats_NET_CAMP\Home_task_4\Task_3\data\",
                $"consumption_{year}_{quarter}");
            try
            {
                ElectricityUsage = parser.Parse(filePath);
            }
            catch (DirectoryNotFoundException)
            {
                throw;
            }
        }
    }
}
