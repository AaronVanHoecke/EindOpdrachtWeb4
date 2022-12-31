using RestaurantBL.Model;
using RestaurantRESTgebruiker.Exceptions;
using RestaurantRESTgebruiker.Model.Output;

namespace RestaurantRESTbeheerder.Mappers
{
    public static class MapReservatieFromDomain
    {
        public static ReservatieRESToutputDTO MapFromDomain(Reservatie res)
        {
			try
			{
                return new ReservatieRESToutputDTO(res.ReservatieID, res.RestaurantInfo.ID, res.ContactPersoon.Naam, res.AantalPlaatsen, res.ReservatieDetail, res.Tafelnummer);
            }
			catch (Exception e)
			{
				throw new MapperException("Er is een fout opgetreden", e);
			}
        }
    }
}
