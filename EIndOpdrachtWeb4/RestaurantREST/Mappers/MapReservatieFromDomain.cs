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
                return new ReservatieRESToutputDTO(res.ReservatieID, res.RestaurantInfo, res.ContactPersoon, res.ContactPersoon.Email, res.ContactPersoon.Telefoonnummer, res.AantalPlaatsen, res.Datum, res.Uur, res.Tafelnummer);
            }
			catch (Exception e)
			{
				throw new MapperException("Er is een fout opgetreden", e);
			}
        }
    }
}
