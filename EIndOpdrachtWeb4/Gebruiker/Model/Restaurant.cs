using RestaurantBL.Checkers;
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
        public int ID { get; private set; }
        public string Naam { get; private set; }
        public Locatie Locatie { get; set; }
        public string Keuken { get; set; }
        public string Telefoonnummer { get; set; }
        public string Email { get; set; }
        public List<Tafel> Tafels { get; private set; }

        public Restaurant(string naam, Locatie locatie, string keuken, string telefoonnummer, string email)
        {
            ZetNaam(naam);
            Locatie = locatie;
            Keuken = keuken;
            ZetTelefoonnummer(telefoonnummer);
            ZetEmail(email);
        }

        public Restaurant(int iD, string naam, Locatie locatie, string keuken, string telefoonnummer, string email, List<Tafel> tafels)
        {
            ID = iD;
            Naam = naam;
            Locatie = locatie;
            Keuken = keuken;
            Telefoonnummer = telefoonnummer;
            Email = email;
            Tafels = tafels;
        }

        public Restaurant(int iD, string naam, Locatie locatie, string keuken, string telefoonnummer, string email)
        {
            ID = iD;
            Naam = naam;
            Locatie = locatie;
            Keuken = keuken;
            Telefoonnummer = telefoonnummer;
            Email = email;
        }

        public Restaurant()
        {
        }

        public void ZetNaam(string naam)
        {
            if (string.IsNullOrWhiteSpace(naam)) throw new RestaurantException("Naam mag niet leeg zijn");
            Naam = naam;
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

        public void ZetId(int id)
        {
            if (id < 0) throw new RestaurantException("ZetId - Id mag niet kleiner zijn dan 0");
            ID = id;
        }
        
        //public void VoegTafelToe(Tafel tafel)
        //{
        //    if (tafel == null) throw new RestaurantException("VoegTafelToe - Tafel mag niet leeg zijn");
        //    if (Tafels.Select(t => t.Tafelnummer).Contains(tafel.Tafelnummer)) throw new RestaurantException("VoegTafelToe - Tafelnummer bestaat al");
        //    Tafels.Add(tafel);
        //}
    }
}
