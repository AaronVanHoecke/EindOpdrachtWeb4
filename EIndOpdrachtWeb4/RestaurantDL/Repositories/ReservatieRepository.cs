using Microsoft.EntityFrameworkCore;
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
                return ctx.Reservatie.Any(r => r.ReservatieDetail == reservatie.ReservatieDetail && r.Tafelnummer == reservatie.Tafelnummer && r.RestaurantInfo.RestaurantID == reservatie.RestaurantInfo.ID);
            }
            catch (Exception ex)
            {
                throw new RepositoryException("BestaatReservatie - Er is een fout opgetreden", ex);
            }
        }

        public bool BestaatReservatie(int reservatieId)
        {
            try
            {
                return ctx.Reservatie.Any(r => r.ID == reservatieId);
            }
            catch (Exception ex)
            {
                throw new RepositoryException("BestaatReservatie - Er is een fout opgetreden", ex);
            }
        }

        public Reservatie GeefReservatie(int id)
        {
            try
            {
                return MapReservatie.MapToDomain(ctx.Reservatie.Find(id));
            }
            catch (Exception ex)
            {
                throw new RepositoryException("GeefReservatie - Er is een fout opgetreden", ex);
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
                return ctx.Reservatie.Include(r => r.RestaurantInfo).Include(r => r.ReservatieDetail).Where(r => r.ReservatieDetail.Date >= begindatum.Value.Date && r.ReservatieDetail.Date <= einddatum.Value.Date).Select(r => MapReservatie.MapToDomain(r)).ToList();
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

        public List<Reservatie> GeefReservatiesVanGebruikerOpDatum(int id, DateTime? datum)
        {
            try
            {
                return ctx.Reservatie.Include(r => r.RestaurantInfo).ThenInclude(r => r.Locatie).Include(r => r.RestaurantInfo.Tafels).Include(r => r.ContactPersoon).ThenInclude(c => c.Locatie).Where(r => r.ContactPersoon.Id == id && r.ReservatieDetail.Date == datum.Value.Date).Select(r => MapReservatie.MapToDomain(r)).ToList();
            }
            catch (Exception ex)
            {
                throw new RepositoryException("GeefReservatiesVanGebruikerOpDatum - Er is een fout opgetreden", ex);
            }
        }

        public List<Reservatie> GeefReservatiesVanRestaurant(int restaurantId)
        {
            try
            {
                return ctx.Reservatie.Include(r => r.RestaurantInfo).ThenInclude(r => r.Locatie).Include(r => r.RestaurantInfo.Tafels).Include(r => r.ContactPersoon).ThenInclude(c => c.Locatie).Where(r => r.RestaurantInfo.RestaurantID == restaurantId).Select(r => MapReservatie.MapToDomain(r)).ToList();
            }
            catch (Exception ex)
            {
                throw new RepositoryException("GeefReservatiesVanRestaurant - Er is een fout opgetreden", ex);
            }
        }

        public List<Reservatie> GeefReservatiesVoorgebruikerOpDatum(int id, DateTime? datumB, DateTime? datumE)
        {
            try
            {
                return ctx.Reservatie.Include(r => r.RestaurantInfo).ThenInclude(r => r.Locatie).Include(r => r.RestaurantInfo.Tafels).Include(r => r.ContactPersoon).ThenInclude(c => c.Locatie).Where(r =>r.ContactPersoon.Id == id && r.ReservatieDetail.Date >= datumB && r.ReservatieDetail.Date <= datumE).Select(r => MapReservatie.MapToDomain(r)).ToList();
            }
            catch (Exception)
            {
                throw new RepositoryException("GeefReservatiesVoorgebruikerOpDatum - Er is een fout opgetreden");
            }
        }

        public bool IsDezelfde(Reservatie reservatie)
        {
            try
            {
                return ctx.Reservatie.Any(r => r.ID == reservatie.ReservatieID && r.ReservatieDetail == reservatie.ReservatieDetail && r.AantalPlaatsen == reservatie.AantalPlaatsen && r.ContactPersoon.Id == reservatie.ContactPersoon.Id && r.RestaurantInfo.RestaurantID == reservatie.RestaurantInfo.ID);
            }
            catch (Exception ex)
            {
                throw new RepositoryException("IsDezelfde - Er is een fout opgetreden", ex);
            }
        }

        public bool TafelHeeftReservaties(int tafelnummer)
        {
            try
            {
                return ctx.Reservatie.Any(r => r.Tafelnummer == tafelnummer);
            }
            catch (Exception ex)
            {
                throw new RepositoryException("TafelHeeftReservaties - Er is een fout opgetreden", ex);
            }
        }

        public Reservatie UpdateReservatie(Reservatie reservatie)
        {
            try
            {
                ctx.Reservatie.Update(MapReservatie.MapToDB(reservatie, ctx));
                ctx.SaveChanges();
                return MapReservatie.MapToDomain(ctx.Reservatie.Include(r => r.RestaurantInfo.Tafels).Include(r => r.ContactPersoon).ThenInclude(c => c.Locatie).OrderBy(r => r.ID).Where(r => r.ID == reservatie.ReservatieID).FirstOrDefault());
            }
            catch (Exception ex)
            {
                throw new RepositoryException("UpdateReservatie - Er is een fout opgetreden", ex);
            }
        }

        public void VerwijderReservatie(int reservatieid)
        {
            try
            {
                ReservatieEF res = ctx.Reservatie.Find(reservatieid);
                res.Verwijderd = true;
                ctx.Reservatie.Update(res);
                ctx.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new RepositoryException("VerwijderReservatie - Er is een fout opgetreden", ex);
            }
        }

        public Reservatie VoegReservatieToe(Reservatie reservatie)
        {
            try
            {
                ctx.Reservatie.Add(MapReservatie.MapToDB(reservatie, ctx));
                ctx.SaveChanges();
                return MapReservatie.MapToDomain(ctx.Reservatie.Include(r => r.RestaurantInfo.Tafels).Include(r => r.ContactPersoon).OrderBy(r => r.ID).Last());
            }
            catch (Exception ex)
            {
                throw new RepositoryException("VoegReservatieToe - Er is een fout opgetreden", ex);
            }
        }
    }
}
