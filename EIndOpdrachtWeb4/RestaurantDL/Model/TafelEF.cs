using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantDL.Model
{
    public class TafelEF
    {
        [Key]
        public int ID { get; set; }
        [Required]
        public int AantalStoelen { get; set; }
        [Required]
        public int RestaurantID { get; set; }
        [Required]
        public int TafelNummer { get; set; }
        public RestaurantEF Restaurant { get; set; }
        public bool Verwijderd { get; set; }

        public TafelEF(int iD, int aantalStoelen, int tafelnummer, int restaurantID)
        {
            ID = iD;
            AantalStoelen = aantalStoelen;
            TafelNummer = tafelnummer;
            RestaurantID = restaurantID;
            Verwijderd = false;
        }

        public TafelEF(int aantalStoelen, int tafelnummer, int restaurantID)
        {
            AantalStoelen = aantalStoelen;
            TafelNummer = tafelnummer;
            RestaurantID = restaurantID;
            Verwijderd = false;
        }

        public TafelEF()
        {
        }
    }
}
