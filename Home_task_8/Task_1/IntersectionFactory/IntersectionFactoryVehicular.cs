using System.Numerics;

namespace CrossRoads.Factory
{
    public class IntersectionFactoryVehicular : IntersectionFactory
    {
        public override void CreateIntersection(Func<string, int> CreateLanes, Func<string, Direction> SetDirection)
        {
            const int roadCount = 4; 
            Lane[][] arrayOfLanes = new Lane[roadCount][]
            {
                new Lane[CreateLanes("Введіть кількість смуг для Західного напряму: ")],
                new Lane[CreateLanes("Введіть кількість смуг для Північного напряму: ")],
                new Lane[CreateLanes("Введіть кількість смуг для Східного напряму: ")],
                new Lane[CreateLanes("Введіть кількість смуг для Південного напряму: ")]
            };

            int counter = 1;
            foreach (Lane[] lanes in arrayOfLanes)
            {
                for (int i = 0; i < lanes.Length; ++i)
                {
                    lanes[i] = new Lane();
                    lanes[i].AllowedDirections = SetDirection($"Введіть напрям(и) для смуги №{counter}.{i + 1} розділені комою." +
                        "\nДоступні напрями: West, East, North, South -> ");
                }

                ++counter;
            }

            for(int i = 0; i < roadCount; ++i)
            {
                if(i % 2 == 0)
                {
                    foreach (Lane lane in arrayOfLanes[i])
                    {
                        TrafficLightIndicator[] indicators =
                        {
                            new(IndicatorColor.Red, Direction.None, TimeSpan.FromSeconds(20), false),
                            new(IndicatorColor.Yellow, Direction.None, TimeSpan.FromSeconds(4), false),
                            new(IndicatorColor.Green, lane.AllowedDirections, TimeSpan.FromSeconds(20), true),
                        };

                        lane.TrafficLight = new TrafficLightVehicular(indicators);
                    }
                }
                else
                {
                    foreach (Lane lane in arrayOfLanes[i])
                    {
                        TrafficLightIndicator[] indicators =
                        {
                            new(IndicatorColor.Red, Direction.None, TimeSpan.FromSeconds(20), true),
                            new(IndicatorColor.Yellow, Direction.None, TimeSpan.FromSeconds(4), false),
                            new(IndicatorColor.Green, lane.AllowedDirections, TimeSpan.FromSeconds(20), false),
                        };

                        lane.TrafficLight = new TrafficLightVehicular(indicators);
                    }
                }
            }

            Road roadWest = new(Direction.West, (Lane[])arrayOfLanes[0].Clone());
            Road roadEast = new(Direction.East, (Lane[])arrayOfLanes[2].Clone());
            Road roadNorth = new(Direction.North, (Lane[])arrayOfLanes[1].Clone());
            Road roadSouth = new(Direction.South, (Lane[])arrayOfLanes[3].Clone());

            intersection = new Intersection(4, 4, roadWest, roadEast, roadNorth, roadSouth);
            isCreated = true;
        }

        public override void CreateDefaultIntersection()
        {
            TrafficLightIndicator[] indicatorsGreenWest =
            {
                new(IndicatorColor.Red, Direction.None, TimeSpan.FromSeconds(20), false),
                new(IndicatorColor.Yellow, Direction.None, TimeSpan.FromSeconds(2), false),
                new(IndicatorColor.Green, Direction.East, TimeSpan.FromSeconds(20), true),
            };

            TrafficLightIndicator[] indicatorsGreenEast = indicatorsGreenWest.CloneIndicators();
            indicatorsGreenEast[2].AllowedDirections = Direction.South | Direction.West | Direction.North;

            TrafficLightIndicator[] indicatorsRedSouth = indicatorsGreenEast.CloneIndicators();
            indicatorsRedSouth[0].IsActive = true;
            indicatorsRedSouth[2].IsActive = false;
            indicatorsRedSouth[2].AllowedDirections = Direction.East | Direction.North | Direction.West;

            TrafficLightIndicator[] indicatorsRedNorth1 = indicatorsRedSouth.CloneIndicators();
            indicatorsRedNorth1[2].AllowedDirections = Direction.South | Direction.West;

            TrafficLightIndicator[] indicatorsRedNorth2 = indicatorsRedSouth.CloneIndicators();
            indicatorsRedNorth2[2].AllowedDirections = Direction.South | Direction.East;

            Lane toNorth = new(
                true,
                new TrafficLightVehicular(indicatorsRedSouth),
                Direction.North
                );

            Lane toSouth1 = new(
                true,
                new TrafficLightVehicular(indicatorsRedNorth1),
                Direction.South
                );

            Lane toSouth2 = new(
                true,
                new TrafficLightVehicular(indicatorsRedNorth2),
                Direction.South
                );

            Lane toEast = new(
                true,
                new TrafficLightVehicular(indicatorsGreenWest),
                Direction.East
                );

            Lane toWest = new(
                true,
                new TrafficLightVehicular(indicatorsGreenEast),
                Direction.West
                );

            Road roadEast = new Road(Direction.East, toWest);
            Road roadWest = new Road(Direction.West, toEast);
            Road roadSouth = new Road(Direction.South, toNorth);
            Road roadNorth = new Road(Direction.North, toSouth1, toSouth2);

            intersection = new Intersection(4,4, roadEast, roadWest, roadSouth, roadNorth);
            isCreated = true;
        }

        /// <summary>
        /// Sets the timer for each traffic light indicator color
        /// </summary>
        /// <param name="SetTimer">Pass null as an argument to set timers to default
        /// or call the SetDefaultTimers() method</param>
        public override void SetIndicatorTimer(Func<string, TimeSpan>? SetTimer)
        {
            if (!isCreated)
            {
                throw new NullReferenceException("First create an intersection");
            }

            TimeSpan redLightTimer = SetTimer is null ? TimeSpan.FromSeconds(14) : SetTimer.Invoke("Час червоного світла: "); 
            TimeSpan yellowLightTimer = SetTimer is null ? TimeSpan.FromSeconds(5) : SetTimer.Invoke("Час жовтого світла: ");
            TimeSpan greenLightTimer = SetTimer is null ? TimeSpan.FromSeconds(14) : SetTimer.Invoke("Час зеленого світла: ");

            int i = 0;
            foreach (Road road in intersection!)
            {
                
                foreach (Lane lane in road.Lanes)
                {
                    if (i % 2 == 0)
                    {
                        lane.TrafficLight!.TrafficLightIndicators[2].Duration = redLightTimer;
                        lane.TrafficLight.TrafficLightIndicators[1].Duration = yellowLightTimer;
                        lane.TrafficLight.TrafficLightIndicators[0].Duration = greenLightTimer;
                    }
                    else
                    {
                        lane.TrafficLight!.TrafficLightIndicators[0].Duration = redLightTimer;
                        lane.TrafficLight.TrafficLightIndicators[1].Duration = yellowLightTimer;
                        lane.TrafficLight.TrafficLightIndicators[2].Duration = greenLightTimer;
                    }
                }

                ++i;
            }
        }

        public override void SetDefaultTimers()
        {
            SetIndicatorTimer(null);
        }
    }
}
