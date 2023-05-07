namespace Home_task_7
{
    public enum IndicatorColor : byte
    {
        Red,
        Yellow,
        Green,
    }

    [System.Flags]
    public enum Direction : byte
    {
        None    =   0b00_00,
        North   =   0b00_01,
        South   =   0b00_10,
        West    =   0b01_00,
        East    =   0b10_00,
    }

    public static class EnumTranslator
    {
        public static string TranslateToUkrainian(this IndicatorColor color)
        {
            return color switch
            {
                IndicatorColor.Red => "Червоний",
                IndicatorColor.Yellow => "Жовтий",
                IndicatorColor.Green => "Зелений",
                _ => "Невизначено"
            };
        }

        public static string TranslateToUkrainian(this Direction direction)
        {
            return direction switch
            {
                Direction.West => "Захід",
                Direction.East => "Схід",
                Direction.North => "Північ",
                Direction.South => "Південь",
                _ => "Немає"
            };
        }
    }
}
