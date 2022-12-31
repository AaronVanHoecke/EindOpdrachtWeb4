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
        public DateTime ReservatieDetail { get; set; }
        [Required]
        public int Tafelnummer { get; set; }

        public bool Verwijderd { get; set; }

        public ReservatieEF(int reservatieID, RestaurantEF restaurantInfo, GebruikerEF contactPersoon, int aantalPlaatsen, DateTime reservatiedetail, int tafelnummer)
        {
            ID = reservatieID;
            RestaurantInfo = restaurantInfo;
            ContactPersoon = contactPersoon;
            AantalPlaatsen = aantalPlaatsen;
            ReservatieDetail = reservatiedetail;
            Tafelnummer = tafelnummer;
            Verwijderd = false;
        }

        public ReservatieEF(RestaurantEF restaurantInfo, GebruikerEF contactPersoon, int aantalPlaatsen, DateTime reservatiedetail, int tafelnummer)
        {
            RestaurantInfo = restaurantInfo;
            ContactPersoon = contactPersoon;
            AantalPlaatsen = aantalPlaatsen;
            ReservatieDetail = reservatiedetail;
            Tafelnummer = tafelnummer;
            Verwijderd = false;
        }

        public ReservatieEF()
        {
        }
    }
}
