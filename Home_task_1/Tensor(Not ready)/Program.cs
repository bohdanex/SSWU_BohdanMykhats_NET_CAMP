namespace Tensor
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");
        }
    }

    public class Tensor
    {
        private byte _rank;
        private int[] tensor;
        public object this[params int[] index]
        {
            get { /* return the specified index here */ }
            set { /* set the specified index to value here */ }
        }

        public Tensor(byte rank)
        {
            _rank = rank;
            tensor = Array.Empty<int>();
        }
        //Припустимо дано матрицю 10х4
        //приходять індекси 4,2
        private int GetValueFromTensor(params int[] data)
        {
            int selectedIndex = data.Aggregate((a,b) => a + b);
            return tensor[selectedIndex];
        }

        private void SetValueToTensor()
        {

        }
    }
}