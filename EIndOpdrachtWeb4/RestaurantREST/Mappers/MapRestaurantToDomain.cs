using RestaurantBL.Model;
using RestaurantDL.Exceptions;
using RestaurantDL.Model;
using RestaurantRESTbeheerder.Model.Input;

namespace RestaurantRESTbeheerder.Mappers
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
