using RestaurantBL.Model;

namespace RestaurantRESTbeheerder.Model.Output
{
    public class ReservatieRESToutputDTO
    {
        public int ReservatieID { get; set; }
        public Restaurant RestaurantInfo { get; set; }

        public Gebruiker ContactPersoon { get; set; }

        public int AantalPlaatsen { get; set; }

        public DateTime Datum { get; set; }

        public int Uur { get; set; }

        public int Tafelnummer { get; set; }

        public ReservatieRESToutputDTO(int reservatieID, Restaurant restaurantInfo, Gebruiker contactPersoon, string email, string telefoonnummer, int aantalPlaatsen, DateTime datum, int uur, int tafelnummer)
        {
            ReservatieID = reservatieID;
            RestaurantInfo = restaurantInfo;
            ContactPersoon = contactPersoon;
            AantalPlaatsen = aantalPlaatsen;
            ContactPersoon.Email = email;
            ContactPersoon.Telefoonnummer = telefoonnummer;
            Datum = datum;
            Uur = uur;
            Tafelnummer = tafelnummer;
        }
    }
}
