using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantBL.Exceptions
{
    public class ReservatieException : Exception
    {
        public ReservatieException(string message) : base(message)
        {
        }
        public ReservatieException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}
