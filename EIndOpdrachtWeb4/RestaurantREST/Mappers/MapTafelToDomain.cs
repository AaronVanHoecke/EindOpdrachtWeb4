using RestaurantBL.Model;
using RestaurantRESTbeheerder.Exceptions;
using RestaurantRESTbeheerder.Model.Input;

namespace RestaurantRESTbeheerder.Mappers
{
    public static class MapTafelToDomain
    {
        public static Tafel MapToDomain(TafelRESTinputDTO tafel, int restaurantId)
        {
            try
            {
                return new Tafel(tafel.AantalPlaatsen,tafel.TafelNummer ,restaurantId);
            }
            catch (Exception e)
            {
                throw new MapperException("MapToDomain", e);
            }
        }
    }
}
