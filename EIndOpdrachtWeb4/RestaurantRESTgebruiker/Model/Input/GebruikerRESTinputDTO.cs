using RestaurantBL.Model;

namespace RestaurantRESTgebruiker.Model.Input
{
    public class GebruikerRESTinputDTO
    {
        public string Naam { get; set; }
        public string Email { get; set; }
        public string Telefoonnummer { get; set; }
        public string Gemeente { get; set; }
        public int Postcode { get; set; }
        public string Straat { get; set; }
        public string Huisnummer { get; set; }
    }
}
