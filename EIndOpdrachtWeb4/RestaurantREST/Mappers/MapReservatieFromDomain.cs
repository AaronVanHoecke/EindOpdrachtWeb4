using RestaurantBL.Model;
using RestaurantRESTbeheerder.Exceptions;
using RestaurantRESTbeheerder.Model.Output;

namespace RestaurantRESTbeheerder.Mappers
{
    public static class MapReservatieFromDomain
    {
        public static ReservatieRESToutputDTO MapFromDomain(Reservatie res)
        {
			try
			{
                return new ReservatieRESToutputDTO(res.ReservatieID, res.RestaurantInfo, res.ContactPersoon, res.AantalPlaatsen, res.ReservatieDetail, res.Tafelnummer);
            }
			catch (Exception e)
			{
				throw new MapperException("Er is een fout opgetreden", e);
			}
        }
    }
}
