namespace Events
{
    internal class Program
    {
        static void Main(string[] args)
        {
            TrafficLightVehicular tf = new TrafficLightVehicular();
            tf.AddSubscriber(Handler);
            for (int i = 0; i < 14; i++)
            {
                Console.Write(i + "; ");
                tf.TriggerTimer();
            }
        }

        private static void Handler(object sender, EventArgs e)
        {
            Console.WriteLine("Handled");
        }
    }

    public abstract class TrafficLight
    {
        //Першим варіантом є створення абстрактної\protected події.
        //Але цей випадок не завжди спрацьовує. Компілятор може
        //некоректно їх обробити
        public event EventHandler? SwitchLight;

        //краще створити метод, який буде викликати цю подію.
        //Це дасть наслідникам можливість "викликати" події
        public virtual void OnLightSwitched(object sender, EventArgs e)
        {
            SwitchLight?.Invoke(sender, e);
        }
    }

    public class TrafficLightVehicular : TrafficLight
    {
        private TimeSpan timer;

        public TrafficLightVehicular()
        {
            timer = TimeSpan.FromSeconds(4);
        }

        public void AddSubscriber(EventHandler handler)
        {
            SwitchLight += handler;
        }

        public void TriggerTimer()
        {
            if(timer > TimeSpan.FromSeconds(0))
            {
                timer -= TimeSpan.FromSeconds(1);
            }
            else
            {
                //виклик події базового типу
                OnLightSwitched(this, EventArgs.Empty);
                timer = TimeSpan.FromSeconds(5);
            }
        }
    }
}