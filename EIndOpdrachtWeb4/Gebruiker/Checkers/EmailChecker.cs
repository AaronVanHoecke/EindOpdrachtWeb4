using RestaurantBL.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace RestaurantBL.Checkers
{
    public class EmailChecker
    {
        static public bool CheckEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email)) throw new EmailCheckerException("CheckEmail - email mag niet null zijn");
            return Regex.IsMatch(email,
            @"^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\.[a-zA-Z0-9-.]+$");
        }
    }
}
