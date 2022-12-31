using RestaurantBL.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantBL.Model
{
    public class Reservatie
    {
        public int ReservatieID { get; set; }
        public Restaurant RestaurantInfo { get; set; }

        public Gebruiker ContactPersoon { get; set; }

        public int AantalPlaatsen { get; set; }

        public DateTime ReservatieDetail { get; set; }

        public int Tafelnummer { get; set; }

        public Reservatie(int reservatieID, Restaurant restaurantInfo, Gebruiker contactPersoon, int aantalPlaatsen, DateTime reservatiedetail, int tafelnummer)
        {
            ReservatieID = reservatieID;
            RestaurantInfo = restaurantInfo;
            ContactPersoon = contactPersoon;
            AantalPlaatsen = aantalPlaatsen;
            ReservatieDetail = reservatiedetail;
            Tafelnummer = tafelnummer;
        }

        public Reservatie(Restaurant restaurantInfo, Gebruiker contactPersoon, int aantalPlaatsen, DateTime reservatiedetail, int tafelnummer)
        {
            RestaurantInfo = restaurantInfo;
            ContactPersoon = contactPersoon;
            AantalPlaatsen = aantalPlaatsen;
            ReservatieDetail = reservatiedetail;
            Tafelnummer = tafelnummer;
        }

        public void ZetId(int id)
        {
            if (id <= 0) throw new ReservatieException("Id moet groter zijn dan 0");
            ReservatieID = id;
        }
    }
}
