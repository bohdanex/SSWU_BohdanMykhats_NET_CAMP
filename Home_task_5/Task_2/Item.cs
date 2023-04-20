using System.Text;

namespace Task_2
{
    public abstract class Item : IFormattable
    {
        public string? Name { get; protected set; }
        public double Height { get; protected set; }
        public double Width { get; protected set; }
        public double Length { get; protected set; }


        public override string ToString()
        {
            return $"{this.GetType().Name}: {Name}, Height: {Height}, Width: {Width}, Length: {Length}";
        }

        public string ToString(string? format, IFormatProvider? formatProvider)
        {
            if(format is not null && byte.TryParse(format, out byte offset))
            {
                StringBuilder tabs = new();

                int i = offset;
                while(i > 0)
                {
                    tabs.Append(' ');
                    --i;
                }

                return tabs.ToString() + ToString();
            }

            return ToString();
        }
    }
}
