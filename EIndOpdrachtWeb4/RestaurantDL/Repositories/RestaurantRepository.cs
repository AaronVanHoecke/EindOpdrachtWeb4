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
                return ctx.Tafel.Any(t => t.ID == tafel.ID && t.AantalStoelen == tafel.AantalStoelen && t.Beschikbaar == tafel.Beschikbaar && t.RestaurantID == tafel.RestaurantID);
            }
            catch (Exception ex)
            {
                throw new RepositoryException("BestaatTafel - Er is een fout opgetreden", ex);
            }
        }

        public List<Tafel> GeefBeschikbareTafels(DateTime datum, Locatie locatie, string keuken)
        {
            try
            {            
            return ctx.Tafel.Where(t => t.Beschikbaar == true && ctx.Restaurant.Any(r => r.Locatie == MapLocatie.MapToDB(locatie, ctx) && r.Keuken == keuken) && ctx.Reservatie.Any(r => r.Datum != datum)).Select(t => MapTafel.MapToDomain(t)).ToList();
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
                return ctx.Tafel.Where(t => t.Beschikbaar == true && t.RestaurantID == restaurant.ID).Select(t => MapTafel.MapToDomain(t)).ToList();
            }
            catch (Exception ex)
            {
                throw new RepositoryException("GeefBeschikbareTafelsRestaurant - Er is een fout opgetreden", ex);
            }
        }

        public Restaurant GeefRestaurant(int id)
        {
            try
            {
                return MapRestaurant.MapToDomain(ctx.Restaurant.Find(id));
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
                return ctx.Restaurant.Any(r => r.Naam == restaurant.Naam && r.Locatie.StraatNaam == restaurant.Locatie.StraatNaam && r.Email == restaurant.Email && r.Telefoonnummer == restaurant.Telefoonnummer);
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
                return ctx.Tafel.Any(t => t.ID == tafel.ID);
            }
            catch (Exception ex)
            {
                throw new RepositoryException("IsDezelfde - Er is een fout opgetreden", ex);
            }
        }

        public void UpdateRestaurant(Restaurant restaurant)
        {
            try
            {
                ctx.Restaurant.Update(MapRestaurant.MapToDB(restaurant, ctx));
                ctx.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new RepositoryException("UpdateRestaurant - Er is een fout opgetreden", ex);
            }
        }

        public void UpdateTafel(Restaurant res, Tafel tafel)
        {
            try
            {

                TafelEF t = MapTafel.MapToDB(tafel, ctx);
                ctx.Tafel.Update(t);

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

        public void VerwijderTafel(Restaurant res, int tafelId)
        {
            try
            {
                TafelEF t = ctx.Tafel.Where(t => t.ID == tafelId && t.RestaurantID == res.ID).FirstOrDefault();
                t.Verwijderd = true;
                ctx.Tafel.Update(t);
            }
            catch (Exception ex)
            {

                throw new RepositoryException("VerwijderTafel - Er is een fout opgetreden", ex);
            }

        }

        public void VoegRestaurantToe(Restaurant restaurant)
        {
            try
            {
                ctx.Restaurant.Add(MapRestaurant.MapToDB(restaurant, ctx));
                ctx.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new RepositoryException("VoegRestaurantToe - Er is een fout opgetreden", ex);
            }
        }

        public void VoegTafelToe(Restaurant res, Tafel tafel)
        {
            try
            {
                ctx.Tafel.Add(MapTafel.MapToDB(tafel, ctx));
                ctx.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new RepositoryException("VoegTafelToe - Er is een fout opgetreden", ex);
            }
        }

        
    }
}
