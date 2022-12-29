using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantDL.Model
{
    public class ReservatieEF
    {
        [Key]
        public int ID { get; set; }
        [Required]
        public RestaurantEF RestaurantInfo { get; set; }
        [Required]
        public GebruikerEF ContactPersoon { get; set; }
        [Required]
        public int AantalPlaatsen { get; set; }
        [Required]
        public DateTime Datum { get; set; }
        [Required]
        public int Uur { get; set; }
        [Required]
        public int Tafelnummer { get; set; }

        public bool Verwijderd { get; set; }

        public ReservatieEF(int reservatieID, RestaurantEF restaurantInfo, GebruikerEF contactPersoon, int aantalPlaatsen, DateTime datum, int uur, int tafelnummer)
        {
            ID = reservatieID;
            RestaurantInfo = restaurantInfo;
            ContactPersoon = contactPersoon;
            AantalPlaatsen = aantalPlaatsen;
            Datum = datum;
            Uur = uur;
            Tafelnummer = tafelnummer;
            Verwijderd = false;
        }

        public ReservatieEF(RestaurantEF restaurantInfo, GebruikerEF contactPersoon, int aantalPlaatsen, DateTime datum, int uur, int tafelnummer)
        {
            RestaurantInfo = restaurantInfo;
            ContactPersoon = contactPersoon;
            AantalPlaatsen = aantalPlaatsen;
            Datum = datum;
            Uur = uur;
            Tafelnummer = tafelnummer;
            Verwijderd = false;
        }

        public ReservatieEF()
        {
        }
    }
}
