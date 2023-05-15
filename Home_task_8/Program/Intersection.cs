using System.Collections;
using System.Collections.Generic;

namespace CrossRoads
{
    public class Intersection : ICloneable, IEnumerable<Road>
    {
        private Road[] roads;

        public Road this[int index]
        {
            get => roads[index];
        }

        public Intersection() : this(0, 0) { }
#pragma warning disable 8618
        public Intersection(uint minCount = 4, uint maxCount = 4, params Road[] roads)
        {
            SetRoads(minCount, maxCount, roads);
        }
#pragma warning restore
        public void SetRoads(uint minCount = 4, uint maxCount = 4, params Road[] roads)
        {
            this.roads = Verifier.ValidateRoadCount(minCount, maxCount, roads);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public IEnumerator<Road> GetEnumerator()
        {
            foreach (Road road in roads)
            {
                yield return road;
            }
        }

        public object Clone()
        {
            uint roadLength = (uint)roads.Length;
            Road[] roadsCloned = new Road[roadLength];

            for(int i = 0; i < roadLength; ++i)
            {
                roadsCloned[i] = (Road)roads[i].Clone();
            }

            return new Intersection(roadLength, roadLength, roadsCloned);
        }

        protected static class Verifier
        {
            public static Road[] ValidateRoadCount(uint minCount, uint maxCount, params Road[] roads)
            {
                if (roads.Length < minCount || roads.Length > maxCount)
                {
                    throw new ArgumentException($"Road count should be more than {minCount} and less than {maxCount}");
                }

                return (Road[])roads.Clone();
            }
        }
    }
}
