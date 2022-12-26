﻿using System;
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

        public DateTime Datum { get; set; }

        public int Uur { get; set; }

        public int Tafelnummer { get; set; }

        public Reservatie(int reservatieID, Restaurant restaurantInfo, Gebruiker contactPersoon, string email, string telefoonnummer, int aantalPlaatsen, DateTime datum, int uur, int tafelnummer)
        {
            ReservatieID = reservatieID;
            RestaurantInfo = restaurantInfo;
            ContactPersoon = contactPersoon;
            AantalPlaatsen = aantalPlaatsen;
            Datum = datum;
            Uur = uur;
            Tafelnummer = tafelnummer;
        }

        public Reservatie(Restaurant restaurantInfo, Gebruiker contactPersoon, string email, string telefoonnummer, int aantalPlaatsen, DateTime datum, int uur, int tafelnummer)
        {
            RestaurantInfo = restaurantInfo;
            ContactPersoon = contactPersoon;
            AantalPlaatsen = aantalPlaatsen;
            Datum = datum;
            Uur = uur;
            Tafelnummer = tafelnummer;
        }
    }
}
