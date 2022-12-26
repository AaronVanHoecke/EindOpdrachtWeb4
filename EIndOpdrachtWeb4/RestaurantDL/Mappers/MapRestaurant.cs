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
    public static class MapRestaurant
    {
        public static Restaurant MapToDomain(RestaurantEF db)
        {
            try
            {
                return new Restaurant(db.ResaurantID, db.Naam, MapLocatie.MapToDomain(db.Locatie), db.Keuken, db.Telefoonnummer, db.Email, db.Reserveringen.Select(res => MapReservatie.MapToDomain(res)).ToList(), db.Tafels.Select(t => MapTafel.MapToDomain(t)).ToList());
            }
            catch (Exception e)
            {
                throw new MapperException("MapToDomain", e);
            }
        }

        public static RestaurantEF MapToDB(Restaurant domain, RestaurantBeheerContext ctx)
        {
            try
            {
                RestaurantEF r = ctx.Restaurant.Find(domain.ID);
                if (r != null)
                {
                    r.Naam = domain.Naam;
                    r.Locatie = MapLocatie.MapToDB(domain.Locatie, ctx);
                    r.Keuken = domain.Keuken;
                    r.Telefoonnummer = domain.Telefoonnummer;
                    r.Email = domain.Email;
                    return r;
                }
                return new RestaurantEF(domain.Naam, MapLocatie.MapToDB(domain.Locatie, ctx), domain.Keuken, domain.Telefoonnummer, domain.Email);
            }
            catch (Exception e)
            {
                throw new MapperException("MapToDB", e);
            }
        }
    }
}
