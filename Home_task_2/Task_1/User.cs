namespace WaterTowerSimulator
{
    public class User
    {
        //Цей ідентифікатор квартири може знадобитися у майбутньому
        public int AparementId { get; }
        //споживання води за одну годину. Цей стан залежить від самої людини.
        // Симулятор цим процесом керує. Він пропонує різні системи як може споживатись вода, різні стратегії.
        public double ConsumptionPerHour { get; private set; }

        public User(double waterConsumption, int aparementId)
        {
            ConsumptionPerHour = Validator.MinValue(0, waterConsumption, Simulator.MaxWaterConsumption);
            AparementId = aparementId;
        }

        public override string ToString()
        {
            return $"Apartment #{AparementId} consumes {ConsumptionPerHour} liters per hour";
        }
    }
}
