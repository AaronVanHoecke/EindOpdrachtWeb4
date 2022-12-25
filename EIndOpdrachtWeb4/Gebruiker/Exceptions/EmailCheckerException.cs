using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantBL.Exceptions
{
    public class EmailCheckerException : Exception
    {
        public EmailCheckerException(string message) : base(message)
        {
        }
        public EmailCheckerException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}
