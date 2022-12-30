namespace RestaurantRESTbeheerder.Model.Output
{
    public class RestaurantRESToutputDTO
    {
        public int Id { get; set; }
        public string Naam { get; set; }
        public int Postcode { get; set; }
        public string Gemeente { get; set; }
        public string Straat { get; set; }
        public string Huisnummer { get; set; }
        public string Keuken { get; set; }
        public string Telefoonnummer { get; set; }
        public string Email { get; set; }

        public RestaurantRESToutputDTO(int id, string naam, int postcode, string gemeente, string straat, string huisnummer, string keuken, string telefoonnummer, string email)
        {
            Id = id;
            Naam = naam;
            Postcode = postcode;
            Gemeente = gemeente;
            Straat = straat;
            Huisnummer = huisnummer;
            Keuken = keuken;
            Telefoonnummer = telefoonnummer;
            Email = email;
        }
    }
}
