using Microsoft.EntityFrameworkCore;
using RestaurantBL.Interfaces;
using RestaurantBL.Model;
using RestaurantDL.Exceptions;
using RestaurantDL.Mappers;
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
                return ctx.Restaurant.Any(r => r.ResaurantID == restaurant.ID && r.Naam == restaurant.Naam && r.Telefoonnummer == restaurant.Telefoonnummer);
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
            throw new NotImplementedException();
        }

        public List<Tafel> GeefBeschikbareTafelsRestaurant(Restaurant restaurant)
        {
            throw new NotImplementedException();
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
                return ctx.Restaurant.Any(r => r.ResaurantID == restaurant.ID);
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
                //ctx.Tafel.Update(MapTafel.MapToDB(res, ctx)); kijken bij mapper
                //ctx.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new RepositoryException("UpdateTafel - Er is een fout opgetreden", ex);
            }
        }

        public void VerwijderRestaurant(Restaurant restaurant)
        {
            try
            {
                ctx.Restaurant.Remove(MapRestaurant.MapToDB(restaurant, ctx));
                ctx.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new RepositoryException("VerwijderRestaurant - Er is een fout opgetreden", ex);
            }
        }

        public void VerwijderTafel(Restaurant res, int tafelId)
        {
            throw new NotImplementedException();
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
            throw new NotImplementedException();
        }
    }
}
