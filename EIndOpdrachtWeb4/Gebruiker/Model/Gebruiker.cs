using RestaurantBL.Checkers;
using RestaurantBL.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantBL.Model
{
    public class Gebruiker
    {
        public int Id { get; private set; }
        public string Naam { get; private set; }
        public Contactgegevens Contactgegevens { get; set; }
        public Locatie Locatie { get; set; }

        public Gebruiker(string naam, Contactgegevens contactgegevens, Locatie locatie)
        {
            ZetNaam(naam);
            Contactgegevens = contactgegevens;
            Locatie = locatie;
        }
        
        public Gebruiker(int id, string naam, Contactgegevens contactgegevens, Locatie locatie) : this(naam, contactgegevens, locatie)
        {
            ZetId(id);
        }

        public void ZetNaam(string naam)
        {
            if (string.IsNullOrWhiteSpace(naam)) throw new GebruikerException("ZetNaam - Naam mag niet leeg zijn");
            Naam = naam;
        }

        public void ZetId(int id)
        {
            if (id <= 0) throw new GebruikerException("ZetId - Id < 0");
            Id = id;
        }
    }
}
