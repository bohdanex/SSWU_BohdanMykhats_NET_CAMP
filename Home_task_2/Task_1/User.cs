namespace WaterTowerSimulator
{
    public class User
    {
        private double _consumptionPerHour;

        //Цей ідентифікатор квартири може знадобитися у майбутньому
        public int AparementId { get; }
        //споживання води за одну годину. Цей стан залежить від самої людини
        public double ConsumptionPerHour 
        {
            get => _consumptionPerHour;
            private set
            {
                _consumptionPerHour= Validator.MinValue(0, value, Simulator.MaxWaterConsumption);
            }
        }

        public User(double waterConsumption, int aparementId)
        {
            ConsumptionPerHour = waterConsumption;
            AparementId = aparementId;
        }

        public override string ToString()
        {
            return $"Apartment #{AparementId} consumes {ConsumptionPerHour} liters per hour";
        }
    }
}
