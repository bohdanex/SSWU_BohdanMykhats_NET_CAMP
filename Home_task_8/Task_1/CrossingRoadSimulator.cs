using System.Threading;
using System.Text;
using CrossRoads.Factory;

namespace CrossRoads
{
    public class CrossingRoadSimulator
    {
        private TimeSpan timer;
        private IntersectionFactory[] intersectionFactories;

        public CrossingRoadSimulator(params IntersectionFactory[] intersectionFactories)
        {
            timer = TimeSpan.FromSeconds(0);
            Intersections = Array.Empty<Intersection>();
            this.intersectionFactories = (IntersectionFactory[])intersectionFactories.Clone();
            CreateIntersections();
        }

        public Intersection[] Intersections { get; private set; }

        private void CreateIntersections()
        {
            Intersections = new Intersection[intersectionFactories.Length];

            for(int i = 0; i < Intersections.Length; ++i)
            {
                intersectionFactories[i].CreateDefaultIntersection();
                Intersections[i] = intersectionFactories[i].Build();
            }
        }

        public void Simulate(Action? StartMessage, Func<Command> ReadCommand, Action<string> Output, Func<string, TimeSpan> SetTimers)
        {
            StartMessage?.Invoke();
            bool isPaused = false;
            bool isStarted = false;
            Command command = 0;

            Thread simulationThread = new(delegate()
            {
                while (true)
                {
                    if (isPaused is false)
                    {
                        StringBuilder output = new(ToString());
                        output.AppendLine();

                        int counter = 1;
                        Thread.Sleep(300);
                        foreach (Intersection intersection in Intersections)
                        {
                            output.AppendLine("Перехрестя №" + counter);
                            foreach (Road road in intersection)
                            {
                                output.AppendLine(String.Format("{0}ний напрямок", road));
                                StringBuilder firstRow = new StringBuilder(String.Format("  {0,-10} |", "На"));
                                StringBuilder secondRow = new StringBuilder(String.Format("  {0,-10} |", "Світлофор"));
                                foreach (Lane lane in road.Lanes)
                                {
                                    firstRow.Append(String.Format(" {0,20} ", lane));
                                    if (lane.IsOpen)
                                    {
                                        secondRow.Append(String.Format(" {0,20} ", lane.TrafficLight.ToString("c",null)));
                                        lane.TrafficLight?.TriggerTimer();
                                    }
                                    else
                                    {
                                        secondRow.Append(String.Format(" {0,20} ", "Рух заборонено"));
                                    }
                                }
                                output.AppendLine(firstRow.ToString());
                                output.AppendLine(secondRow.ToString());
                            }
                            ++counter;
                        }

                        timer += TimeSpan.FromSeconds(1);
                        Output(output.ToString());
                    }
                }
            });            

            while (true)
            {
                command = ReadCommand();
                if(command is Command.Start && isStarted is false)
                {
                    simulationThread.Start();
                    isStarted = true;
                }
                else if(isStarted is true)
                {
                    if (command is Command.Abort)
                    {
                        Environment.Exit(0);
                    }
                    else if (command is Command.Pause)
                    {
                        isPaused = !isPaused;
                    }
                }

                if (command is Command.ChangeRules)
                {
                    isPaused = true;
                    Thread.CurrentThread.Join(1000);
                    for (int i = 0; i < intersectionFactories.Length; ++i)
                    {
                        intersectionFactories[i].SetIndicatorTimer(SetTimers);
                        Intersections[i] = intersectionFactories[i].Build();
                    }
                    isPaused = false;
                }
            }
        }

        public override string ToString()
        {
            return $"t={timer.TotalSeconds} c.";
        }
    }
}
