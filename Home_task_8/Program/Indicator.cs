using System.Text;

namespace CrossRoads
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
                _ => "Не визначено"
            };
        }

        public static string TranslateToUkrainian(this Direction direction)
        {
            Direction[] allDirections = Enum.GetValues<Direction>();
            StringBuilder output = new StringBuilder();

            foreach (Direction dir in allDirections)
            {
                if (direction.HasFlag(dir) && dir is not Direction.None)
                {
                    output.Append(' ');
                    output.Append(
                        dir switch
                        {
                            Direction.West => "Захід",
                            Direction.East => "Схід",
                            Direction.North => "Північ",
                            Direction.South => "Південь",
                            _ => ""
                        });
                }
            }
            return output.ToString();
        }
    }
}
