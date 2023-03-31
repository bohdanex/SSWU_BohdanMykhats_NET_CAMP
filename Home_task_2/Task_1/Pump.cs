namespace WaterTowerSimulator
{
    public class Pump
    {
        private readonly int _power;

        public bool IsPumping { get; private set; }

        public Pump(int power)
        {
            _power = power;
            //насос на початку має бути виключеним
            IsPumping = false;
        }

        //Я довго думав чи передавати водонапірну вежу як аргумент
        //чи зробити поле WaterTower. Тому щоб зменшити залежність
        //між класами я вибрав перший варіант
        public void PumpWater(WaterTower waterTower) { }
        public void StartPump(WaterTower waterTower) { }
        public void StopPump() { }

        public override string ToString()
        {
            return $"Pump power: {_power}. IsPumping? - {IsPumping}";
        }
    }
}
