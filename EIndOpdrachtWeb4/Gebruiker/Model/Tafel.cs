using RestaurantBL.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantBL.Model
{
    public class Tafel
    {
        public int ID { get; private set; }
        public int AantalStoelen { get; private set; }
        public int RestaurantID { get; private set; }
        public int Tafelnummer { get; private set; }
        public Tafel(int aantalStoelen, int tafelnummer, int restaurantId)
        {
            ZetStoelen(aantalStoelen);
            ZetTafelnummer(tafelnummer);
            RestaurantID = restaurantId;
        }

        public Tafel(int iD, int aantalStoelen, int tafelnummer, int restaurantId) : this(aantalStoelen, tafelnummer, restaurantId)
        {
            ZetId(iD);
        }

        public void ZetStoelen(int aantalStoelen)
        {
            if (aantalStoelen <= 0) throw new TafelException("Aantal stoelen moet groter zijn dan 0");
            AantalStoelen = aantalStoelen;
        }

        public void ZetId(int id)
        {
            if (id <= 0) throw new TafelException("Id moet groter zijn dan 0");
            ID = id;
        }

        public void ZetTafelnummer(int tafelnummer)
        {

            if (tafelnummer <= 0) throw new TafelException("Tafelnummer moet groter zijn dan 0");
            Tafelnummer = tafelnummer;
        }
    }
}
