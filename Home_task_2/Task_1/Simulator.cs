namespace WaterTowerSimulator
{
    //Симулятор має доступ до керування водонапірною вежею та користувачами
    public class Simulator
    {
        private WaterTower _waterTower;
        //До користувача можна буде звернутися по квартирі (ключ) у якій він живе
        private Dictionary<int, User> _users;

        //Максимальна фізична кількість води, яку може споживати користувач за момент часу
        public static double MaxWaterConsumption { get; private set; }

        public Simulator(WaterTower waterTower, Dictionary<int, User> users, double maxWaterConsumption = 0.5)
        {
            _waterTower = waterTower;
            _users = users;
            MaxWaterConsumption = Validator.MinValue(0.5, maxWaterConsumption);
        }
// 
        public void Simulate() { }
        // а чому не властивість? І чому приватний метод?
        private void ChangeWaterTower(WaterTower waterTower) { }
        
        private void AddUser(int apartmentId, User user) { }
        private void RemoveUser(int apartmentId) { }

        public override string ToString()
        {
            return $"Water tower water volume: {_waterTower.CurrentVolume}";
        }
    }
}
