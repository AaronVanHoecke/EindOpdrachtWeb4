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
        public bool BestaatGebruiker(Gebruiker gebruiker)
        {
            try
            {
                return ctx.Gebruiker.Any(g => g.Id == gebruiker.Id);
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
                return ctx.Gebruiker.Any(g => g.Id == gebruiker.Id && g.Naam == gebruiker.Naam && g.Email == gebruiker.Email && g.Telefoonnummer == gebruiker.Telefoonnummer);
            }
            catch (Exception ex)
            {
                throw new RepositoryException("IsDezelfde - Er is een fout opgetreden", ex);
            }
        }

        public void UpdateGebruiker(Gebruiker gebruiker)
        {
            try
            {
                ctx.Gebruiker.Update(MapGebruiker.MapToDB(gebruiker, ctx));
                SaveAndClear();
            }
            catch (Exception ex)
            {
                throw new RepositoryException("UpdateGebruiker - Er is een fout opgetreden", ex);
            }
        }

        public void VerwijderGebruiker(Gebruiker gebruiker)
        {
            try
            {
                ctx.Gebruiker.Remove(MapGebruiker.MapToDB(gebruiker, ctx));
                SaveAndClear();
            }
            catch (Exception ex)
            {
                throw new RepositoryException("VerwijderGebruiker - Er is een fout opgetreden", ex);
            }
        }

        public int VoegGebruikerToe(Gebruiker gebruiker)
        {
            try
            {
                ctx.Gebruiker.Add(MapGebruiker.MapToDB(gebruiker, ctx));
                SaveAndClear();
                return ctx.Gebruiker.Last().Id;
            }
            catch (Exception ex)
            {
                throw new RepositoryException("VoegGebruikerToe - Er is een fout opgetreden", ex);
            }
        }
    }
}
