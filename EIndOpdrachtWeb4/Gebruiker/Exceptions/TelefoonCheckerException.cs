using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantBL.Exceptions
{
    public class TelefoonCheckerException: Exception
    {
        public TelefoonCheckerException(string message) : base(message)
        {
        }
        public TelefoonCheckerException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}
