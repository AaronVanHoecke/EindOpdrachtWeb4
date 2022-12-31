using RestaurantBL.Model;

namespace RestaurantRESTgebruiker.Model.Output
{
    public class ReservatieRESToutputDTO
    {
        public int ReservatieID { get; set; }
        public int RestaurantID { get; set; }

        public string ContactPersoon { get; set; }

        public int AantalPlaatsen { get; set; }

        public DateTime ReservatieDetail { get; set; }

        public int Tafelnummer { get; set; }

        public ReservatieRESToutputDTO(int reservatieID, int restaurantId, string contactPersoon, int aantalPlaatsen, DateTime reservatiedetail, int tafelnummer)
        {
            ReservatieID = reservatieID;
            RestaurantID = restaurantId;
            ContactPersoon = contactPersoon;
            AantalPlaatsen = aantalPlaatsen;
            ReservatieDetail = reservatiedetail;
            Tafelnummer = tafelnummer;
        }
    }
}
