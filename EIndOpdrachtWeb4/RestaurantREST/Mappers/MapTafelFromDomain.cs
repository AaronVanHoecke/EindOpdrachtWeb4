using RestaurantBL.Managers;
using RestaurantBL.Model;
using RestaurantDL.Exceptions;
using RestaurantRESTbeheerder.Model.Output;

namespace RestaurantRESTbeheerder.Mappers
{
    public static class MapTafelFromDomain
    {
        public static TafelRESToutputDTO MapFromDomain(Tafel t, RestaurantManager rm)
        {
            try
            {
                return new TafelRESToutputDTO(t.ID, t.RestaurantID, t.AantalStoelen, t.Tafelnummer);
            }
            catch (Exception e)
            {
                throw new MapperException("MapFromDomain", e);
            }
        }
    }
}
