using RestaurantBL.Managers;
using RestaurantBL.Model;
using RestaurantRESTgebruiker.Exceptions;
using RestaurantRESTgebruiker.Model.Output;

namespace RestaurantRESTgebruiker.Mappers
{
    public static class MapRestaurantFromDomain
    {
        public static RestaurantRESToutputDTO MapFromDomain(int restaurantID, RestaurantManager rm)
        {
            try
            {
                Restaurant restaurant = rm.GeefRestaurant(restaurantID);
                return new RestaurantRESToutputDTO(restaurant.ID, restaurant.Naam, restaurant.Locatie.Postcode, restaurant.Locatie.GemeenteNaam, restaurant.Locatie.StraatNaam, restaurant.Locatie.Huisnummer, restaurant.Keuken, restaurant.Telefoonnummer, restaurant.Email);
            }
            catch (Exception e)
            {
                throw new MapperException("MapFromDomain", e);
            }
        }
    }
}
