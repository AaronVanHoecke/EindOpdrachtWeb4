using RestaurantBL.Model;
using RestaurantRESTbeheerder.Exceptions;
using RestaurantRESTbeheerder.Model.Input;

namespace RestaurantRESTbeheerder.Mappers
{
    public static class MapTafelToDomain
    {
        public static Tafel MapToDomain(TafelRESTinputDTO tafel, int id)
        {
            try
            {
                return new Tafel(tafel.AantalPlaatsen, id, tafel.Beschikbaar);
            }
            catch (Exception e)
            {
                throw new MapperException("MapToDomain", e);
            }
        }
    }
}
