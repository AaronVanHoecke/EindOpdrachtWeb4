using RestaurantBL.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantBL.Model
{
    public class Restaurant
    {
        public string Naam { get; private set; }
        public Locatie Locatie { get; set; }
        public string Keuken { get; set; }
        public Contactgegevens Contactgegevens { get; set; }
        private List<Reservatie> Reserveringen { get; set; }
        private List<Tafel> Tafels { get; set; }

        public Restaurant(string naam, Locatie locatie, string keuken, Contactgegevens contactgegevens)
        {
            ZetNaam(naam);
            Locatie = locatie;
            Keuken = keuken;
            Contactgegevens = contactgegevens;
            Reserveringen = new List<Reservatie>();
            Tafels = new List<Tafel>();
        }

        public void ZetNaam(string naam)
        {
            if (!string.IsNullOrWhiteSpace(naam)) throw new RestaurantException("Naam mag niet leeg zijn");
            Naam = naam;
        }
    }
}
