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
    public static class MapGebruiker
    {
        public static Gebruiker MapToDomain(GebruikerEF db)
        {
            try
            {
                return new Gebruiker(db.Id, db.Naam, db.Email, db.Telefoonnummer, MapLocatie.MapToDomain(db.Locatie));
            }
            catch (Exception e)
            {
                throw new MapperException("MapToDomain", e);
            }
        }

        public static GebruikerEF MapToDB(Gebruiker domain, RestaurantBeheerContext ctx)
        {
            try
            {
                GebruikerEF g = ctx.Gebruiker.Find(domain.Id);
                LocatieEF l = ctx.Locatie.Where(loc => loc.StraatNaam == domain.Locatie.StraatNaam && loc.Huisnummer == domain.Locatie.Huisnummer && loc.GemeenteNaam == domain.Locatie.GemeenteNaam && loc.Postcode == domain.Locatie.Postcode).FirstOrDefault();
                if (l == null) l = MapLocatie.MapToDB(domain.Locatie, ctx);
                if (g is not null)
                {
                    g.Naam = domain.Naam;
                    g.Email = domain.Email;
                    g.Telefoonnummer = domain.Telefoonnummer;
                    g.Locatie = l;
                    return g;
                }
                return new GebruikerEF(domain.Naam, domain.Email, domain.Telefoonnummer, MapLocatie.MapToDB(domain.Locatie, ctx));
            }
            catch (Exception ex)
            {
                throw new MapperException("MapToDB");
            }
        }
    }
}
