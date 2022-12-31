using RestaurantBL.Managers;
using RestaurantBL.Model;
using RestaurantRESTgebruiker.Exceptions;
using RestaurantRESTgebruiker.Model.Input;

namespace RestaurantRESTgebruiker.Mappers
{
    public static class MapReservatieToDomain
    {
        public static Reservatie MapToDomain(ReservatieRESTinputDTO reservatie, Tafel t, Gebruiker g, Restaurant r)
        {
            try
            {
                return new Reservatie(r, g, reservatie.AantalPlaatsen, reservatie.Datum, t.ID);
            }
            catch (Exception ex)
            {

                throw new MapperException("Reservatie kon niet gemapt worden", ex);
            }
        }
    }
}
