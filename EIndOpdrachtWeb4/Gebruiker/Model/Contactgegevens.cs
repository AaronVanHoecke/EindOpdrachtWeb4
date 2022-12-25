using RestaurantBL.Checkers;
using RestaurantBL.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantBL.Model
{
    public class Contactgegevens
    {
        public string Email { get; set; }
        public string Telefoonnummer { get; set; }

        public Contactgegevens(string email, string telefoon)
        {
            Email = email;
            Telefoonnummer = telefoon;
        }

        public void ZetEmail(string email)
        {
            if (!EmailChecker.CheckEmail(email)) throw new EmailCheckerException("ZetEmail - Email is niet geldig");
            Email = email;
        }

        public void ZetTelefoon(string telefoonnummer)
        {
            if (!TelefoonChecker.CheckTelefoon(telefoonnummer)) throw new TelefoonCheckerException("ZetTelefoon - Telefoonnummer is niet geldig");
            Telefoonnummer = telefoonnummer;  
        }
    }
}
