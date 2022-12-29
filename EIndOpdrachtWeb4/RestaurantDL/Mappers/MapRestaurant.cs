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
                return new Restaurant(db.RestaurantID, db.Naam, MapLocatie.MapToDomain(db.Locatie), db.Keuken, db.Telefoonnummer, db.Email, db.Reserveringen.Select(res => MapReservatie.MapToDomain(res)).ToList(), db.Tafels.Select(t => MapTafel.MapToDomain(t)).ToList());
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
                LocatieEF l = ctx.Locatie.Where(loc => loc.StraatNaam == domain.Locatie.StraatNaam && loc.Huisnummer == domain.Locatie.Huisnummer && loc.GemeenteNaam == domain.Locatie.GemeenteNaam && loc.Postcode == domain.Locatie.Postcode).FirstOrDefault();
                if (l == null) l = MapLocatie.MapToDB(domain.Locatie, ctx);
                if (r != null)
                {
                    r.Naam = domain.Naam;
                    r.Locatie = l;
                    r.Keuken = domain.Keuken;
                    r.Telefoonnummer = domain.Telefoonnummer;
                    r.Email = domain.Email;
                    return r;
                }
                return new RestaurantEF(domain.Naam, MapLocatie.MapToDB(domain.Locatie, ctx), domain.Keuken, domain.Email, domain.Telefoonnummer);
            }
            catch (Exception e)
            {
                throw new MapperException("MapToDB", e);
            }
        }
    }
}
