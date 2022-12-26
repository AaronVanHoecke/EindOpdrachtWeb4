using RestaurantBL.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantBL.Model
{
    public class Locatie
    {
        public int LocatieId { get; private set; }
        public int Postcode { get; private set; }
        public string GemeenteNaam { get; private set; }
        public string StraatNaam { get; set; }
        public string Huisnummer { get; set; }

        public Locatie(int postcode, string gemeenteNaam)
        {
            ZetPostcode(postcode);
            ZetGemeenteNaam(gemeenteNaam);
        }

        public Locatie(int postcode, string gemeenteNaam, string straatNaam, string huisnummer, int Id) : this(postcode, gemeenteNaam)
        {
            StraatNaam = straatNaam;
            Huisnummer = huisnummer;
            LocatieId = Id;
        }

        public void ZetPostcode(int postcode)
        {
            if (postcode < 1000 || postcode > 9999) throw new LocatieException("ZetPostcode - Postcode is niet correct");
            Postcode = postcode;
        }

        public void ZetGemeenteNaam(string gemeenteNaam)
        {
            if (string.IsNullOrWhiteSpace(gemeenteNaam)) throw new LocatieException("ZetGemeenteNaam - GemeenteNaam mag niet leeg zijn");
            GemeenteNaam = gemeenteNaam;
        }
    }
}
