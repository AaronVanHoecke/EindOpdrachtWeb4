namespace RestaurantRESTgebruiker.Model.Input
{
    public class ReservatieRESTinputDTO
    {
        public int RestaurantID { get; set; }
        public int GebruikerID { get; set; }
        public int AantalPlaatsen { get; set; }
        public DateTime Datum { get; set; }
    }
}
