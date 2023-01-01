using RestaurantBL.Managers;
using RestaurantBL.Model;
using RestaurantRESTbeheerder.Exceptions;
using RestaurantRESTbeheerder.Model.Output;

namespace RestaurantRESTbeheerder.Mappers
{
    public static class MapRestaurantFromDomain
    {
        public static RestaurantRESToutputDTO MapFromDomain(Restaurant restaurant)
        {
            try
            {
                return new RestaurantRESToutputDTO(restaurant.ID, restaurant.Naam, restaurant.Locatie.Postcode, restaurant.Locatie.GemeenteNaam, restaurant.Locatie.StraatNaam, restaurant.Locatie.Huisnummer, restaurant.Keuken, restaurant.Telefoonnummer, restaurant.Email);
            }
            catch (Exception e)
            {
                throw new MapperException("MapFromDomain", e);
            }
        }
    }
}
