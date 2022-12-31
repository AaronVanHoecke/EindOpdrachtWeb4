using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantDL.Model
{
    public class RestaurantEF
    {
        [Key]
        public int RestaurantID { get; set; }
        [Required]
        public string Naam { get; set; }
        [Required]
        public LocatieEF Locatie { get; set; }
        [Required]
        public string Keuken { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Telefoonnummer { get; set; }
        public List<TafelEF> Tafels { get; private set; }
        public bool Verwijderd { get; set; }

        public RestaurantEF(int resaurantID, string naam, LocatieEF locatie, string keuken, string email, string telefoonnummer, List<TafelEF> tafels)
        {
            RestaurantID = resaurantID;
            Naam = naam;
            Locatie = locatie;
            Keuken = keuken;
            Email = email;
            Telefoonnummer = telefoonnummer;

            Tafels = tafels;
            Verwijderd = false;
        }

        public RestaurantEF(string naam, LocatieEF locatie, string keuken, string email, string telefoonnummer)
        {
            Naam = naam;
            Locatie = locatie;
            Keuken = keuken;
            Email = email;
            Telefoonnummer = telefoonnummer;
            Tafels = new List<TafelEF>();
            Verwijderd = false;
        }

        public RestaurantEF()
        {
        }
    }
}
