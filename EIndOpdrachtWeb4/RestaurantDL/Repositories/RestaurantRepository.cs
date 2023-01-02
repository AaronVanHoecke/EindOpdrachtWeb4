using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Scaffolding.Metadata;
using RestaurantBL.Interfaces;
using RestaurantBL.Model;
using RestaurantDL.Exceptions;
using RestaurantDL.Mappers;
using RestaurantDL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantDL.Repositories
{
    public class RestaurantRepository : IRestaurantRepository
    {
        private RestaurantBeheerContext ctx;

        public RestaurantRepository(string connectionString)
        {
           this.ctx = new RestaurantBeheerContext(connectionString);
        }

        public bool BestaatRestaurant(Restaurant restaurant)
        {
            try
            {
                return ctx.Restaurant.Any(r => r.Naam.Equals(restaurant.Naam) && r.Telefoonnummer.Equals(restaurant.Telefoonnummer));
            }
            catch (Exception ex)
            {
                throw new RepositoryException("BestaatRestaurant - Er is een fout opgetreden", ex);
            }
        }

        public bool BestaatRestaurant(int id)
        {
            try
            {
                return ctx.Restaurant.Any(r => r.RestaurantID == id);
            }
            catch (Exception ex)
            {
                throw new RepositoryException("BestaatRestaurant - Er is een fout opgetreden", ex);
            }
        }

        public bool BestaatTafel(Tafel tafel)
        {
            try
            {
                return ctx.Tafel.Any(t => t.TafelNummer == tafel.Tafelnummer && t.RestaurantID == tafel.RestaurantID);
            }
            catch (Exception ex)
            {
                throw new RepositoryException("BestaatTafel - Er is een fout opgetreden", ex);
            }
        }

        public Tafel GeefBeschikbareTafel(int restaurantId, DateTime datum, int aantalPlaatsen)
        {
            try
            {
                return ctx.Tafel.Where(t => t.RestaurantID == restaurantId && t.AantalStoelen >= aantalPlaatsen)
                    .OrderBy(t => t.AantalStoelen)
                    .ThenBy(t => t.ID)
                    .Where(t => !ctx.Reservatie.Any(r => r.Tafelnummer == t.TafelNummer && r.ReservatieDetail < datum.AddMinutes(90) && r.ReservatieDetail > datum.AddMinutes(-90)))
                    .Select(t => MapTafel.MapToDomain(t))
                    .FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw new RepositoryException("GeefBeschikbareTafel - Er is een fout opgetreden", ex);
            }
        }
        
        public List<Tafel> GeefBeschikbareTafels(DateTime datum, Locatie locatie, string keuken)
        {
            try
            {
                return ctx.Tafel.Where(t =>ctx.Restaurant.Any(r => r.Locatie == MapLocatie.MapToDB(locatie, ctx) && r.Keuken == keuken) && ctx.Reservatie.Any(r => r.ReservatieDetail != datum)).Select(t => MapTafel.MapToDomain(t)).ToList();
            }
            catch (Exception ex)
            {
                throw new RepositoryException("GeefBeschikbareTafels - Er is een fout opgetreden", ex);
            }
        }

        public List<Tafel> GeefBeschikbareTafelsRestaurant(Restaurant restaurant)
        {
            try
            {
                return ctx.Tafel.Where(t =>t.RestaurantID == restaurant.ID).Select(t => MapTafel.MapToDomain(t)).ToList();
            }
            catch (Exception ex)
            {
                throw new RepositoryException("GeefBeschikbareTafelsRestaurant - Er is een fout opgetreden", ex);
            }
        }

        public List<Reservatie> GeefReservatiesOpDatum(int id, DateTime datum)
        {
            try
            {
                return ctx.Reservatie.Where(r => r.RestaurantInfo.RestaurantID == id && r.ReservatieDetail.Date == datum.Date).Include(r => r.RestaurantInfo).ThenInclude(r => r.Locatie).Include(r => r.RestaurantInfo.Tafels).Include(r => r.ContactPersoon).ThenInclude(c => c.Locatie).Select(r => MapReservatie.MapToDomain(r)).ToList();
            }
            catch (Exception ex)
            {
                throw new RepositoryException("GeefREservatiesOpDatum - Er is een fout opgetreden", ex);
            }
        }

        public Restaurant GeefRestaurant(int id)
        {
            try
            {
                return MapRestaurant.MapToDomain(ctx.Restaurant.Include(r => r.Locatie).Include(r => r.Tafels).Where(r => r.RestaurantID == id && r.Verwijderd == false).FirstOrDefault());
            }
            catch (Exception ex)
            {
                throw new RepositoryException("GeefRestaurant - Er is een fout opgetreden", ex);
            }
        }

        public List<Restaurant>GeefRestaurant(int value, string keuken)
        {
            try
            {
                return ctx.Restaurant.Include(r => r.Locatie).Include(r => r.Tafels).Where(r => r.Locatie.Postcode == value && r.Keuken == keuken && r.Verwijderd == false).Select(r => MapRestaurant.MapToDomain(r)).ToList();
            }
            catch (Exception ex)
            {
                throw new RepositoryException("GeefRestaurant - Er is een fout opgetreden");
            }
        }

        public List<Restaurant> GeefRestaurant(string keuken)
        {
            try
            {
                return ctx.Restaurant.Include(r => r.Locatie).Include(r => r.Tafels).Where(r => r.Keuken == keuken && r.Verwijderd == false).Select(r => MapRestaurant.MapToDomain(r)).ToList();
            }
            catch (Exception ex)
            {
                throw new RepositoryException("GeefRestaurant - Er is een fout opgetreden", ex);
            }
        }

        public List<Restaurant> GeefRestaurantPostcode(int postcode)
        {
            try
            {
                return ctx.Restaurant.Include(r => r.Locatie).Include(r => r.Tafels).Where(r => r.Locatie.Postcode == postcode && r.Verwijderd == false).Select(r => MapRestaurant.MapToDomain(r)).ToList();
            }
            catch (Exception ex)
            {
                throw new RepositoryException("GeefRestaurant - Er is een fout opgetreden", ex);
            }
        }

        public List<Restaurant> GeefRestaurants()
        {
            try
            {
                return ctx.Restaurant.Select(r => MapRestaurant.MapToDomain(r)).ToList();
            }
            catch (Exception ex)
            {
                throw new RepositoryException("GeefRestaurants - Er is een fout opgetreden", ex);
            }
        }

        public List<Restaurant> GeefRestaurantsOpDatum(DateTime datum, int aantalPlaatsen)
        {
            try
            {
                return ctx.Restaurant.Include(r => r.Locatie).Include(r => r.Tafels).Where(r => r.Verwijderd == false && r.Tafels.Any(t => t.AantalStoelen >= aantalPlaatsen && !ctx.Reservatie.Any(re => re.Tafelnummer == t.TafelNummer && re.ReservatieDetail == datum))).Select(r => MapRestaurant.MapToDomain(r)).ToList();
            }
            catch (Exception ex)
            {
                throw new RepositoryException("GeefRestaurantsOpDatum - Er is een fout opgetreden", ex);
            }
        }

        public Tafel GeefTafel(int tafelId, int restaurantId)
        {
            try
            {
                return MapTafel.MapToDomain(ctx.Tafel.FirstOrDefault(tafel => tafel.TafelNummer == tafelId && tafel.RestaurantID == restaurantId));
            }
            catch (Exception ex)
            {
                throw new RepositoryException("GeefTafel - Er is een fout opgetreden", ex);
            }
        }

        public List<Tafel> GeefTafelsVanRestaurant(Restaurant restaurant)
        {
            try
            {
                return ctx.Tafel.Where(t => t.RestaurantID == restaurant.ID).Select(t => MapTafel.MapToDomain(t)).ToList();
            }
            catch (Exception ex)
            {
                throw new RepositoryException("GeefTafelsVanRestaurant - Er is een fout opgetreden", ex);
            }
        }

        public List<Restaurant> GetRestaurants(Locatie location, string keuken)
        {
            try
            {
                return ctx.Restaurant.Where(r => r.Keuken == keuken && r.Locatie == MapLocatie.MapToDB(location, ctx)).Select(r => MapRestaurant.MapToDomain(r)).ToList();
            }
            catch (Exception ex)
            {
                throw new RepositoryException("GetRestaurants - Er is een fout opgetreden");
            }
        }

        public bool IsDezelfde(Restaurant restaurant)
        {
            try
            {
                return ctx.Restaurant.Any(r => r.Naam == restaurant.Naam && r.Locatie.StraatNaam == restaurant.Locatie.StraatNaam && r.Email == restaurant.Email && r.Telefoonnummer == restaurant.Telefoonnummer && r.Locatie.Huisnummer == restaurant.Locatie.Huisnummer && r.Locatie.Postcode == restaurant.Locatie.Postcode);
            }
            catch (Exception ex)
            {
                throw new RepositoryException("IsDezelfde - Er is een fout opgetreden", ex);
            }
        }

        public bool IsDezelfde(Tafel tafel)
        {
            try
            {
                return ctx.Tafel.Where(t => t.ID == tafel.ID && t.RestaurantID == tafel.RestaurantID).Any(t => t.AantalStoelen == tafel.AantalStoelen);
            }
            catch (Exception ex)
            {
                throw new RepositoryException("IsDezelfde - Er is een fout opgetreden", ex);
            }
        }

        public Restaurant UpdateRestaurant(Restaurant restaurant)
        {
            try
            {
                ctx.Restaurant.Update(MapRestaurant.MapToDB(restaurant, ctx));
                ctx.SaveChanges();
                return MapRestaurant.MapToDomain(ctx.Restaurant.Include(r => r.Tafels).Where(r => r.RestaurantID == restaurant.ID).FirstOrDefault());
            }
            catch (Exception ex)
            {
                throw new RepositoryException("UpdateRestaurant - Er is een fout opgetreden", ex);
            }
        }

        public Tafel UpdateTafel(int restaurantId, Tafel tafel)
        {
            try
            {

                TafelEF t = MapTafel.MapToDB(tafel, ctx);
                ctx.Tafel.Update(t);
                ctx.SaveChanges();
                return MapTafel.MapToDomain(ctx.Tafel.FirstOrDefault(taf => taf.ID == tafel.ID && taf.RestaurantID == restaurantId));
            }
            catch (Exception ex)
            {
                throw new RepositoryException("UpdateTafel - Er is een fout opgetreden", ex);
            }
        }

        public void VerwijderRestaurant(int restaurantId)
        {
            try
            {
                RestaurantEF r = ctx.Restaurant.Find(restaurantId);
                r.Verwijderd = true;
                ctx.Restaurant.Update(r);
                ctx.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new RepositoryException("VerwijderRestaurant - Er is een fout opgetreden", ex);
            }
        }

        public void VerwijderTafel(int restaurantId, int tafelId)
        {
            try
            {
                TafelEF t = ctx.Tafel.Where(t => t.ID == tafelId && t.RestaurantID == restaurantId).FirstOrDefault();
                t.Verwijderd = true;
                ctx.Tafel.Update(t);
                ctx.SaveChanges();
            }
            catch (Exception ex)
            {

                throw new RepositoryException("VerwijderTafel - Er is een fout opgetreden", ex);
            }

        }

        public Restaurant VoegRestaurantToe(Restaurant restaurant)
        {
            try
            {
                ctx.Restaurant.Add(MapRestaurant.MapToDB(restaurant, ctx));
                ctx.SaveChanges();
                return MapRestaurant.MapToDomain(ctx.Restaurant.OrderBy(r => r.RestaurantID).Last());
            }
            catch (Exception ex)
            {
                throw new RepositoryException("VoegRestaurantToe - Er is een fout opgetreden", ex);
            }
        }

        public Tafel VoegTafelToe(Tafel tafel)
        {
            try
            {
                ctx.Tafel.Add(MapTafel.MapToDB(tafel, ctx));
                ctx.SaveChanges();
                return MapTafel.MapToDomain(ctx.Tafel.OrderBy(t => t.ID).Last());
            }
            catch (Exception ex)
            {
                throw new RepositoryException("VoegTafelToe - Er is een fout opgetreden", ex);
            }
        }


    }
}
