using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace LongestHorizontalLine
{
    //Ця структура передбачає приствоєня числу 16-ти байтове число
    public struct HalfByte
    {
        private bool[] _bits;

        public HalfByte()
        {
            _bits = new bool[4];
        }

        public HalfByte(int value)
        {
            _bits = ToHalfByte(value);
        }

        //Converts any integer to a HalfByte
        public static bool[] ToHalfByte(int number)
        {
            if (number > 0b1111)
                throw new ArgumentException("The value must be in range of (0 - 15)");

            string bitData = Convert.ToString(number, 2);

            char[] chars = bitData.ToCharArray();
            bool[] bits = new bool[4];

            for (int i = 0; i < chars.Length; ++i)
            {
                //supports shift assignment
                if (chars[i] == '1')
                {
                    bits[bits.Length - chars.Length + i] = true;
                    continue;
                }

            }

            return bits;
        }

        //Returns the string that shows actual array with zero and ones
        public string ToBits()
        {
            StringBuilder sb = new();

            foreach (bool boolean in _bits)
            {
                if (boolean)
                {
                    sb.Append('1');
                    continue;
                }
                sb.Append('0');
            }

            return sb.ToString();
        }

        public int ToInt32()
        {
            return Int32.Parse(this.ToString());
        }

        //overriding the ToString() method
        //represents HalfByte in decimal number system
        public override string ToString()
        {
            string bitsString = ToBits();

            return Convert.ToByte(bitsString, 2).ToString();
        }

        public override bool Equals([NotNullWhen(true)] object? obj)
        {
            byte thisHalfByte = Byte.Parse(this.ToString());
            //unboxing!
            byte otherHalfByte = Convert.ToByte(((HalfByte)obj!).ToString());
            return thisHalfByte.Equals(otherHalfByte);
        }

        //assign any integer value to HalfByte: HalfByte hf = 12;
        public static implicit operator HalfByte(int number)
        {
            return new HalfByte(number);
        }

        public static bool operator ==(HalfByte left, HalfByte right)
        {
            return left.Equals(right);
        }
        public static bool operator !=(HalfByte left, HalfByte right)
        {
            return !(left == right);
        }
        public static bool operator >=(HalfByte left, HalfByte right)
        {
            byte leftVal = Convert.ToByte(left.ToString());
            byte rightVal = Convert.ToByte(right.ToString());
            return leftVal >= rightVal;
        }
        public static bool operator <=(HalfByte left, HalfByte right)
        {
            byte leftVal = Convert.ToByte(left.ToString());
            byte rightVal = Convert.ToByte(right.ToString());
            return leftVal <= rightVal;
        }

        public static bool operator >(HalfByte left, HalfByte right)
        {
            byte leftVal = Convert.ToByte(left.ToString());
            byte rightVal = Convert.ToByte(right.ToString());
            return leftVal > rightVal;
        }
        public static bool operator <(HalfByte left, HalfByte right)
        {
            byte leftVal = Convert.ToByte(left.ToString());
            byte rightVal = Convert.ToByte(right.ToString());
            return leftVal < rightVal;
        }
    }
}
