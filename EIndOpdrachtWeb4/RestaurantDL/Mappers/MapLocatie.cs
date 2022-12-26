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
    public static class MapLocatie
    {
        public static Locatie MapToDomain(LocatieEF db)
        {
            try
            {
                return new Locatie(db.Postcode, db.GemeenteNaam, db.StraatNaam, db.Huisnummer, db.LocatieId);
            }
            catch (Exception e)
            {
                throw new MapperException("MapToDomain", e);
            }
        }

        public static LocatieEF MapToDB(Locatie domain, RestaurantBeheerContext ctx)
        {
            try
            {
                LocatieEF l = ctx.Locatie.Find(domain.LocatieId);
                if (l != null)
                {
                    l.Postcode = domain.Postcode;
                    l.GemeenteNaam = domain.GemeenteNaam;
                    l.StraatNaam = domain.StraatNaam;
                    l.Huisnummer = domain.Huisnummer;
                    return l;
                }
                return new LocatieEF(domain.Postcode, domain.GemeenteNaam, domain.StraatNaam, domain.Huisnummer);
            }
            catch (Exception e)
            {
                throw new MapperException("MapToDB", e);
            }
        }
    }
}
