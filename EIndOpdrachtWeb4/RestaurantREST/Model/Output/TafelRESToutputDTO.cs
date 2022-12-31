namespace RestaurantRESTbeheerder.Model.Output
{
    public class TafelRESToutputDTO
    {
        public int ID { get; private set; }
        public int RestaurantID { get; private set; }
        public int Tafelnummer { get; private set; }
        public int AantalStoelen { get; private set; }

        public TafelRESToutputDTO(int iD, int restaurantID, int aantalStoelen, int tafelnummer)
        {
            ID = iD;
            RestaurantID = restaurantID;
            AantalStoelen = aantalStoelen;
            Tafelnummer = tafelnummer;
        }
    }
}
