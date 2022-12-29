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
    public class ReservatieRepository : IReservatieRepository
    {
        private RestaurantBeheerContext ctx;

        public ReservatieRepository(string connectionString)
        {
            this.ctx = new RestaurantBeheerContext(connectionString);
        }
        public bool BestaatReservatie(Reservatie reservatie)
        {
            try
            {
                return ctx.Reservatie.Any(r => r.ID == reservatie.ReservatieID);
            }
            catch (Exception ex)
            {
                throw new RepositoryException("BestaatReservatie - Er is een fout opgetreden", ex);
            }
        }

        public List<Reservatie> GeefReservaties()
        {
            try
            {
                return ctx.Reservatie.Select(r => MapReservatie.MapToDomain(r)).ToList();
            }
            catch (Exception ex)
            {
                throw new RepositoryException("GeefReservaties - Er is een fout opgetreden", ex);
            }
        }

        public List<Reservatie> GeefReservatiesOpDatum(DateTime? begindatum, DateTime? einddatum)
        {
            try
            {
                return ctx.Reservatie.Where(r => r.Datum >= begindatum && r.Datum <= einddatum).Select(r => MapReservatie.MapToDomain(r)).ToList();
            }
            catch (Exception ex)
            {
                throw new RepositoryException("GeefReservatiesOpDatum - Er is een fout opgetreden", ex);
            }
        }

        public List<Reservatie> GeefReservatiesVanGebruiker(Gebruiker gebruiker)
        {
            try
            {
                return ctx.Reservatie.Where(r => r.ContactPersoon.Id == gebruiker.Id).Select(r => MapReservatie.MapToDomain(r)).ToList();
            }
            catch (Exception ex)
            {
                throw new RepositoryException("GeefReservatiesVanGebruiker - Er is een fout opgetreden", ex);
            }
        }

        public List<Reservatie> GeefReservatiesVanRestaurant(Restaurant restaurant)
        {
            try
            {
                return ctx.Reservatie.Where(r => r.RestaurantInfo.RestaurantID == restaurant.ID).Select(r => MapReservatie.MapToDomain(r)).ToList();
            }
            catch (Exception ex)
            {
                throw new RepositoryException("GeefReservatiesVanRestaurant - Er is een fout opgetreden", ex);
            }
        }

        public bool IsDezelfde(Reservatie reservatie)
        {
            try
            {
                return ctx.Reservatie.Any(r => r.ID == reservatie.ReservatieID && r.Datum == reservatie.Datum && r.AantalPlaatsen == reservatie.AantalPlaatsen && r.ContactPersoon.Id == reservatie.ContactPersoon.Id && r.RestaurantInfo.RestaurantID == reservatie.RestaurantInfo.ID);
            }
            catch (Exception ex)
            {
                throw new RepositoryException("IsDezelfde - Er is een fout opgetreden", ex);
            }
        }

        public void UpdateReservatie(Reservatie reservatie)
        {
            try
            {
                ctx.Reservatie.Attach(MapReservatie.MapToDB(reservatie, ctx));
                ctx.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new RepositoryException("UpdateReservatie - Er is een fout opgetreden", ex);
            }
        }

        public void VerwijderReservatie(Reservatie reservatie)
        {
            try
            {
                ReservatieEF res = MapReservatie.MapToDB(reservatie, ctx);
                res.Verwijderd = true;
                ctx.Reservatie.Update(res);
                ctx.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new RepositoryException("VerwijderReservatie - Er is een fout opgetreden", ex);
            }
        }

        public int VoegReservatieToe(Reservatie reservatie)
        {
            try
            {
                ctx.Reservatie.Add(MapReservatie.MapToDB(reservatie, ctx));
                ctx.SaveChanges();
                return ctx.Reservatie.Last().ID;
            }
            catch (Exception ex)
            {
                throw new RepositoryException("VoegReservatieToe - Er is een fout opgetreden", ex);
            }
        }
    }
}
