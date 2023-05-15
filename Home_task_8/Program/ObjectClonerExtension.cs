namespace CrossRoads
{
    public static class ObjectClonerExtension
    {
        public static TrafficLightIndicator[] CloneIndicators(this TrafficLightIndicator[] indicators)
        {
            TrafficLightIndicator[] indicatorsResult = new TrafficLightIndicator[indicators.Length];

            for(int i = 0; i < indicators.Length; ++i)
            {
                indicatorsResult[i] = (TrafficLightIndicator)indicators[i].Clone();
            }

            return indicatorsResult;
        }
    }
}
