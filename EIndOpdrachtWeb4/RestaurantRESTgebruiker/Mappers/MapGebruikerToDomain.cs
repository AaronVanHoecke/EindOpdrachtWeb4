using RestaurantBL.Model;
using RestaurantRESTgebruiker.Exceptions;
using RestaurantRESTgebruiker.Model.Input;

namespace RestaurantRESTgebruiker.Mappers
{
    public static class MapGebruikerToDomain
    {
        public static Gebruiker MapToDomain(GebruikerRESTinputDTO gebruiker)
        {
            try
            {
                return new Gebruiker(gebruiker.Naam, gebruiker.Email, gebruiker.Telefoonnummer, new Locatie(gebruiker.Postcode, gebruiker.Gemeente, gebruiker.Straat, gebruiker.Huisnummer));
            }
            catch (Exception ex)
            {
                throw new MapperException("Gebruiker kon niet gemapt worden", ex);
            }
        }
    }
}
