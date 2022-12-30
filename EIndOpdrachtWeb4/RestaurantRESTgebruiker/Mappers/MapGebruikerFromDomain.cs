using RestaurantBL.Managers;
using RestaurantBL.Model;
using RestaurantRESTgebruiker.Exceptions;
using RestaurantRESTgebruiker.Model.Output;

namespace RestaurantRESTgebruiker.Mappers
{
    public static class MapGebruikerFromDomain
    {
        public static GebruikerRESToutputDTO MapFromDomain(int gebruikerId, GebruikerManager gm)
        {
            try
            {
                Gebruiker gebruiker = gm.GeefGebruiker(gebruikerId);
                return new GebruikerRESToutputDTO(gebruiker.Id, gebruiker.Naam, gebruiker.Email, gebruiker.Telefoonnummer, gebruiker.Locatie);
            }
            catch (Exception ex)
            {
                throw new MapperException("Gebruiker kon niet gemapt worden", ex);
            }
        }
    }
}
