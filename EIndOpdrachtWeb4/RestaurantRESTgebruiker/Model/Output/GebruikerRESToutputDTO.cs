using RestaurantBL.Model;

namespace RestaurantRESTgebruiker.Model.Output
{
    public class GebruikerRESToutputDTO
    {
        public int Id { get; set; }
        public string Naam { get; set; }
        public string Email { get; set; }
        public string Telefoonnummer { get; set; }
        public Locatie Locatie { get; set; }

        public GebruikerRESToutputDTO(int id, string naam, string email, string telefoonnummer, Locatie locatie)
        {
            Id = id;
            Naam = naam;
            Email = email;
            Telefoonnummer = telefoonnummer;
            Locatie = locatie;
        }
    }
}
