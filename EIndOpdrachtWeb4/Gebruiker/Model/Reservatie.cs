using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantBL.Model
{
    public class Reservatie
    {
        int ReservatieID { get; set; }
        Restaurant RestaurantInfo { get; set; }

        Gebruiker ContactPersoon { get; set; }

        Contactgegevens ContactGegevens { get; set; }

        int AantalPlaatsen { get; set; }

        DateTime Datum { get; set; }

        int Uur { get; set; }

        int Tafelnummer { get; set; }

        public Reservatie(int reservatieID, Restaurant restaurantInfo, Gebruiker contactPersoon, Contactgegevens contactGegevens, int aantalPlaatsen, DateTime datum, int uur, int tafelnummer)
        {
            ReservatieID = reservatieID;
            RestaurantInfo = restaurantInfo;
            ContactPersoon = contactPersoon;
            ContactGegevens = contactGegevens;
            AantalPlaatsen = aantalPlaatsen;
            Datum = datum;
            Uur = uur;
            Tafelnummer = tafelnummer;
        }
    }
}
