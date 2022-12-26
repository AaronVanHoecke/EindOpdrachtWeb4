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
        public string Email { get; set; }
        public string Telefoonnummer { get; set; }
        public Locatie Locatie { get; set; }

        public Gebruiker(int id, string naam, string email, string telefoonnummer, Locatie locatie)
        {
            ZetId(id);
            ZetNaam(naam);
            ZetEmail(email);
            ZetTelefoonnummer(telefoonnummer);
            Locatie = locatie;
        }

        public Gebruiker(string naam, string email, string telefoonnummer, Locatie locatie)
        {
            ZetNaam(naam);
            ZetEmail(email);
            ZetTelefoonnummer(telefoonnummer);
            Locatie = locatie;
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

        public void ZetEmail(string email)
        {

            if (string.IsNullOrWhiteSpace(email)) throw new GebruikerException("ZetEmail - Email mag niet leeg zijn");
            if (!EmailChecker.CheckEmail(email)) throw new GebruikerException("ZetEmail - Email is niet geldig");
            Email = email;
        }

        public void ZetTelefoonnummer(string telefoonnummer)
        {
            if (string.IsNullOrWhiteSpace(telefoonnummer)) throw new GebruikerException("ZetTelefoonnummer - Telefoonnummer mag niet leeg zijn");
            if (!TelefoonChecker.CheckTelefoon(telefoonnummer)) throw new GebruikerException("ZetTelefoonnummer - Telefoonnummer is niet geldig");
            Telefoonnummer = telefoonnummer;
        }
    }
}
