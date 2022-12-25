using RestaurantBL.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace RestaurantBL.Checkers
{
    public class TelefoonChecker
    {
        public static bool CheckTelefoon(string number)
        {
            if (string.IsNullOrWhiteSpace(number)) throw new TelefoonCheckerException("CheckTelefoon - Telefoonnummer mag niet leeg zijn");
            return Regex.IsMatch(number, @"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$");
        }
    }
}
