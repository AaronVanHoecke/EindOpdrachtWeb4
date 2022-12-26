using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantDL.Model
{
    public class LocatieEF
    {
        [Key]
        public int LocatieId { get; set; }       
        [Required]
        public int Postcode { get; set; }
        [Required]
        public string GemeenteNaam { get; set; }
        public string StraatNaam { get; set; }
        public string Huisnummer { get; set; }

        public LocatieEF(int postcode, string gemeenteNaam)
        {
            Postcode = postcode;
            GemeenteNaam = gemeenteNaam;
        }

        public LocatieEF(int postcode, string gemeenteNaam, string straatNaam, string huisnummer)
        {
            Postcode = postcode;
            GemeenteNaam = gemeenteNaam;
            StraatNaam = straatNaam;
            Huisnummer = huisnummer;
        }

        public LocatieEF()
        {
        }
    }
}
