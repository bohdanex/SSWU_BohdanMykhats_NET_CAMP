namespace Task_2
{
    public static class Validator
    {
        public static double ValidateSize(double size)
        {
            if(size < 1)
            {
                throw new ArgumentException("The height of goods must be more than 1");
            }

            return size;
        }
    }
}
