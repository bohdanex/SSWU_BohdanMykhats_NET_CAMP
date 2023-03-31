namespace WaterTowerSimulator
{
    //Поки що усі методи без реалізації. Тому
    //блоки try-catch ніде не будуть застосовані
    public static class Validator
    {
        public static double MinValue(double minimum, double value)
        {
            if(value < minimum) 
                throw new ArgumentException("The value must me be greater or equal " + minimum);

            return value;
        }

        public static double MinValue(double minimum, double value, double maxValue)
        {
            if (value < minimum)
                throw new ArgumentException("The value must me be greater or equal " + minimum);
            if(value > maxValue) value= maxValue;
            return value;
        }
    }
}
