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
        public int AantalStoelen { get; private set; }
        public int ID { get; private set; }
        public bool Beschikbaar { get; set; }
        public Tafel(int aantalStoelen, int id, bool beschikbaar)
        {
            ZetStoelen(aantalStoelen);
            ZetId(id);
            Beschikbaar = beschikbaar;
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
    }
}
