namespace Task_2
{
    [Flags]
    public enum Season : byte
    {
        None = 0b0000_0000,
        Winter = 0b0000_0001,
        Spring = 0b0000_0010,
        Summer = 0b0000_0100,
        Autumn = 0b0000_1000,
        Versatile = Winter | Spring | Summer | Autumn,
    }
}
