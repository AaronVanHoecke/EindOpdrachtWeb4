using RestaurantBL.Model;

namespace RestaurantRESTbeheerder.Model.Output
{
    public class ReservatieRESToutputDTO
    {
        public int ReservatieID { get; set; }
        public Restaurant RestaurantInfo { get; set; }

        public Gebruiker ContactPersoon { get; set; }

        public int AantalPlaatsen { get; set; }

        public DateTime ReservatieDetail { get; set; }

        public int Tafelnummer { get; set; }

        public ReservatieRESToutputDTO(int reservatieID, Restaurant restaurantInfo, Gebruiker contactPersoon, int aantalPlaatsen, DateTime reservatiedetail, int tafelnummer)
        {
            ReservatieID = reservatieID;
            RestaurantInfo = restaurantInfo;
            ContactPersoon = contactPersoon;
            ReservatieDetail = reservatiedetail;
            AantalPlaatsen = aantalPlaatsen;
            Tafelnummer = tafelnummer;
        }
    }
}
