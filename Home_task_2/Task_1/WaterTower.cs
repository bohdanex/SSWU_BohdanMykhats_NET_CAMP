namespace WaterTowerSimulator
{
    public class WaterTower
    {
        private readonly double _capacity;
        private double _currentVolume;
        private Pump _pump;

        public double CurrentVolume 
        { 
            get => _currentVolume;
            private set
            {
                _currentVolume = Validator.MinValue(0, value, _capacity);
                //Реалізація патерну observer: клас Pump стежить за об'єктами класу WaterTower
                //Я міг би використати події, але переконаний, що поки не варто
                // Насправді подійний варіант тут проситься і саме так ми будем цю задачу реалізовувати.
                // паттерн Спостерігач тут доречний.
                _pump.PumpWater(this);
            }
        }
        //Дозвіл на споживання користувачам (перекриття потоку води)
        public bool IsClosed { get; set; }

        //initialVolume = 0 - вежа може не одразу бути заповнена
        public WaterTower(double capacity, Pump pump, double initialVolume = 0)
        {
            CurrentVolume = initialVolume;
            _capacity = Validator.MinValue(0, capacity);
            _pump = pump;
        }

        public void DrainWater(double volume) { }

        //Злити всю воду з вежі
        public void RemoveWater() { }

        public override string ToString()
        {
            return $"Capacity: {_capacity}, сurrent volume: {CurrentVolume}";
        }
    }
}
