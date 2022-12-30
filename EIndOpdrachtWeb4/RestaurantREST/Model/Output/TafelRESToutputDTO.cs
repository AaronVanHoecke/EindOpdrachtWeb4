namespace RestaurantRESTbeheerder.Model.Output
{
    public class TafelRESToutputDTO
    {
        public int ID { get; private set; }
        public int RestaurantID { get; private set; }
        public int AantalStoelen { get; private set; }
        public bool Beschikbaar { get; set; }
        
        public TafelRESToutputDTO(int iD, int restaurantID, int aantalStoelen, bool beschikbaar)
        {
            ID = iD;
            RestaurantID = restaurantID;
            AantalStoelen = aantalStoelen;
            Beschikbaar = beschikbaar;
        }
    }
}
