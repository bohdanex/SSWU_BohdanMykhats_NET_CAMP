using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task_3.Exceptions
{
    public class ApartmentNotFoundException : ArgumentException
    {
        public ApartmentNotFoundException() : base()
        {

        }

        public ApartmentNotFoundException(string? message) : base(message)
        {

        }
    }
}
