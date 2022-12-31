using RestaurantBL.Model;
using RestaurantDL.Exceptions;
using RestaurantDL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantDL.Mappers
{
    public static class MapReservatie
    {
        public static Reservatie MapToDomain(ReservatieEF reservatie)
        {
            try
            {
                return new Reservatie(
                    reservatie.ID,
                    MapRestaurant.MapToDomain(reservatie.RestaurantInfo),
                    MapGebruiker.MapToDomain(reservatie.ContactPersoon),
                    reservatie.AantalPlaatsen,
                    reservatie.ReservatieDetail,
                    reservatie.Tafelnummer);
            }
            catch (Exception ex)
            {
                throw new MapperException("MapToDomain", ex);
            }
        }

        public static ReservatieEF MapToDB(Reservatie res, RestaurantBeheerContext ctx)
        {
            try
            {
                ReservatieEF r = ctx.Reservatie.Find(res.ReservatieID);
                if (r is not null)
                {
                    r.AantalPlaatsen = res.AantalPlaatsen;
                    r.ReservatieDetail = res.ReservatieDetail;
                    r.ContactPersoon = MapGebruiker.MapToDB(res.ContactPersoon, ctx);
                    r.RestaurantInfo = MapRestaurant.MapToDB(res.RestaurantInfo, ctx);
                    r.Tafelnummer = res.Tafelnummer;
                    return r;
                }
                return new ReservatieEF(MapRestaurant.MapToDB(res.RestaurantInfo, ctx), MapGebruiker.MapToDB(res.ContactPersoon, ctx), res.AantalPlaatsen, res.ReservatieDetail, res.Tafelnummer);
            }
            catch (Exception ex)
            {

                throw new MapperException("MapToDB", ex);
            }
        }
    }
}
