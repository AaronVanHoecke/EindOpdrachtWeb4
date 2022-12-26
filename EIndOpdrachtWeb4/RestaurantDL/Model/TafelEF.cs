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
        public bool Beschikbaar { get; set; }
        [Required]
        public int RestaurantID { get; set; }

        public TafelEF(int iD, int aantalStoelen, bool beschikbaar, int restaurantID)
        {
            ID = iD;
            AantalStoelen = aantalStoelen;
            Beschikbaar = beschikbaar;
            RestaurantID = restaurantID;
        }

        public TafelEF(int aantalStoelen, bool beschikbaar, int restaurantID)
        {
            AantalStoelen = aantalStoelen;
            Beschikbaar = beschikbaar;
            RestaurantID = restaurantID;
        }

        public TafelEF()
        {
        }
    }
}
