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
    public class GebruikerRepository : IGebruikerRepository
    {
        private RestaurantBeheerContext ctx;

        public GebruikerRepository(string connectionString)
        {
            this.ctx = new RestaurantBeheerContext(connectionString);
        }

        private void SaveAndClear()
        {
            ctx.SaveChanges();
            ctx.ChangeTracker.Clear();
        }
        public bool BestaatGebruiker(int gebruikerId)
        {
            try
            {
                return ctx.Gebruiker.Any(g => g.Id == gebruikerId);
            }
            catch (Exception ex)
            {

                throw new RepositoryException("BestaatGebruiker - Er is een fout opgetreden");
            }
        }

        public bool IsDezelfde(Gebruiker gebruiker)
        {
            try
            {
                return ctx.Gebruiker.Any(g => g.Naam == gebruiker.Naam && g.Email == gebruiker.Email && g.Telefoonnummer == gebruiker.Telefoonnummer && g.Locatie == MapLocatie.MapToDB(gebruiker.Locatie, ctx));
            }
            catch (Exception ex)
            {
                throw new RepositoryException("IsDezelfde - Er is een fout opgetreden", ex);
            }
        }

        public Gebruiker UpdateGebruiker(Gebruiker gebruiker)
        {
            try
            {
                ctx.Gebruiker.Update(MapGebruiker.MapToDB(gebruiker, ctx));
                SaveAndClear();
                return MapGebruiker.MapToDomain(ctx.Gebruiker.Include(g => g.Locatie).OrderBy(g => g.Id).Last());
            }
            catch (Exception ex)
            {
                throw new RepositoryException("UpdateGebruiker - Er is een fout opgetreden", ex);
            }
        }

        public void VerwijderGebruiker(int gebruikerId)
        {
            try
            {
                GebruikerEF g = ctx.Gebruiker.Find(gebruikerId);
                g.Verwijderd = true;
                ctx.Gebruiker.Update(g);
                SaveAndClear();
            }
            catch (Exception ex)
            {
                throw new RepositoryException("VerwijderGebruiker - Er is een fout opgetreden", ex);
            }
        }

        public Gebruiker VoegGebruikerToe(Gebruiker gebruiker)
        {
            try
            {
                ctx.Gebruiker.Add(MapGebruiker.MapToDB(gebruiker, ctx));
                SaveAndClear();
                return MapGebruiker.MapToDomain(ctx.Gebruiker.Include(g => g.Locatie).OrderBy(g => g.Id).Last());
            }
            catch (Exception ex)
            {
                throw new RepositoryException("VoegGebruikerToe - Er is een fout opgetreden", ex);
            }
        }

        public Gebruiker GeefGebruiker(int gebruikerId)
        {
            try
            {
                return MapGebruiker.MapToDomain(ctx.Gebruiker.Find(gebruikerId));
            }
            catch (Exception ex)
            {
                throw new RepositoryException("GeefGebruiker - Er is een fout opgetreden", ex);
            }
        }
    }
}
