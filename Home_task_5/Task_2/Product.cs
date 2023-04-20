namespace Task_2
{
    public class Product : Item
    {
        public Product(string name, double height, double width, double length)
        {
            Name = name;
            Height = Validator.ValidateSize(height);
            Width = Validator.ValidateSize(width);
            Length = Validator.ValidateSize(length);
        }
    }
}
