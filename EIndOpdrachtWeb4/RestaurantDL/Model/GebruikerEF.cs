using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantDL.Model
{
    public class GebruikerEF
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Naam { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Telefoonnummer { get; set; }
        [Required]
        public LocatieEF Locatie { get; set; }

        public bool Verwijderd { get; set; }

        public GebruikerEF(int id, string naam, string email, string telefoonnummer, LocatieEF locatie)
        {
            Id = id;
            Naam = naam;
            Email = email;
            Telefoonnummer = telefoonnummer;
            Locatie = locatie;
            Verwijderd = false;
        }
        public GebruikerEF(string naam, string email, string telefoonnummer, LocatieEF locatie)
        {
            Naam = naam;
            Email = email;
            Telefoonnummer = telefoonnummer;
            Locatie = locatie;
            Verwijderd = false;
        }

        public GebruikerEF()
        {
        }
    }
}
