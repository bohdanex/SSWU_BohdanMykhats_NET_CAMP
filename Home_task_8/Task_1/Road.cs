namespace CrossRoads
{
    public sealed class Road : ICloneable
    {
        public Road(Direction position, params Lane[] lanes)
        {
            Lanes = (Lane[])lanes.Clone();
            Position = position;
        }

        public Lane[] Lanes { get; init; }
        public Direction Position { get; init; }

        public object Clone()
        {
            Lane[] lanes = new Lane[Lanes.Length];

            for(int i = 0; i < lanes.Length; ++i)
            {
                lanes[i] = (Lane)Lanes[i].Clone();
            }

            return new Road(Position, lanes);
        }

        public override string ToString()
        {
            return Position.TranslateToUkrainian();
        }
    }
}
