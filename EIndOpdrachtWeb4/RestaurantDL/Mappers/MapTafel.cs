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
    public static class MapTafel
    {
        public static Tafel MapToDomain(TafelEF tafel)
        {
			try
			{
                return new Tafel(tafel.ID, tafel.AantalStoelen, tafel.TafelNummer, tafel.RestaurantID);
            }
			catch (Exception ex)
			{
                throw new MapperException("MapToDomain", ex);
            }
        }

        public static TafelEF MapToDB(Tafel tafel, RestaurantBeheerContext ctx)
        {
            try
            {
                TafelEF t = ctx.Tafel.Find(tafel.ID);
                if (t != null)
                {
                    t.AantalStoelen = tafel.AantalStoelen;
                    t.TafelNummer = tafel.Tafelnummer;
                    t.RestaurantID = tafel.RestaurantID;
                    return t;
                }
                return new TafelEF(tafel.AantalStoelen,tafel.Tafelnummer , tafel.RestaurantID);
            }
            catch (Exception ex)
            {
                throw new MapperException("MapToDB", ex);
            }
        }
    }
}
