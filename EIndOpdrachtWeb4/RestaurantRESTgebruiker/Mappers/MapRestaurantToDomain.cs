using RestaurantBL.Model;
using RestaurantDL.Exceptions;
using RestaurantRESTgebruiker.Model.Input;

namespace RestaurantRESTgebruiker.Mappers
{
    public static  class MapRestaurantToDomain
    {
        public static Restaurant MapToDomain(RestaurantRESTinputDTO restaurant)
        {
            try
            {
                return new Restaurant(restaurant.Naam, new Locatie(restaurant.Postcode, restaurant.Gemeente, restaurant.Straat, restaurant.Huisnummer), restaurant.Keuken, restaurant.Telefoonnummer, restaurant.Email);
            }
            catch (Exception e)
            {
                throw new MapperException("MapToDomain", e);
            }
        }
    }
}
