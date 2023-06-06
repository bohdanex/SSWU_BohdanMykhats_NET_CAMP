using System;

namespace Task_1
{
    public static class Luna
    {
        public static bool IsValidNumbers(string numbers)
        {
            numbers = numbers.Replace(" ", "");

            return (numbers.Reverse().Where((c, index) => index % 2 == 1)
                .SelectMany(c =>
                {
                    int temp = Convert.ToByte(c.ToString());
                    temp *= 2;
                    return temp.ToString();
                }).Sum(c => Convert.ToInt32(c.ToString())) + numbers.Reverse().Where((c, index) => index % 2 == 0)
                .Select(c => Convert.ToInt32(c.ToString())).Sum()) % 10 == 0;
        }
    }
}
