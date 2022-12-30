using RestaurantBL.Exceptions;
using RestaurantBL.Interfaces;
using RestaurantBL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantBL.Managers
{
    public class GebruikerManager
    {
        private IGebruikerRepository gebruikerRepo;
        public GebruikerManager(IGebruikerRepository gebruikerRepo)
        {
            this.gebruikerRepo = gebruikerRepo;
        }

        public Gebruiker VoegGebruikerToe(Gebruiker gebruiker)
        {
            try
            {
                if (gebruiker == null) throw new GebruikerManagerException("VoegGebruikeToe - Gebruiker mag niet null zijn");
                if (gebruikerRepo.BestaatGebruiker(gebruiker.Id)) throw new GebruikerManagerException("VoegGebruikerToe - Gebruiker bestaat al");
                return gebruikerRepo.VoegGebruikerToe(gebruiker);
            }
            catch (Exception ex)
            {
                throw new GebruikerManagerException("VoegGebruikerToe - Er is een fout opgetreden", ex);
            }
        }

        public Gebruiker UpdateGebruiker(Gebruiker gebruiker)
        {
            try
            {
                if (gebruiker == null) throw new GebruikerManagerException("UpdateGebruiker - Gebruiker mag niet null zijn");
                if (!gebruikerRepo.BestaatGebruiker(gebruiker.Id)) throw new GebruikerManagerException("UpdateGebruiker - Gebruiker bestaat niet");
                if (gebruikerRepo.IsDezelfde(gebruiker)) throw new GebruikerManagerException("UpdateGebruiker - Gebruiker is dezelfde");
                return gebruikerRepo.UpdateGebruiker(gebruiker);
            }
            catch (Exception ex)
            {
                throw new GebruikerManagerException("UpdateGebruiker - Er is een fout opgetreden", ex);
            }
        }

        public void VerwijderGebruiker(int gebruikerId)
        {
            try
            {
                if (gebruikerId == null) throw new GebruikerManagerException("VerwijderGebruiker - Gebruiker mag niet null zijn");
                if (!gebruikerRepo.BestaatGebruiker(gebruikerId)) throw new GebruikerManagerException("VerwijderGebruiker - Gebruiker bestaat niet");
                gebruikerRepo.VerwijderGebruiker(gebruikerId);
            }
            catch (Exception ex)
            {
                throw new GebruikerManagerException("VerwijderGebruiker - Er is een fout opgetreden", ex);
            }
        }

        public Gebruiker GeefGebruiker(int gebruikerId)
        {
            try
            {
                if (gebruikerId < 0) throw new GebruikerManagerException("GeefGebruiker - GebruikerId mag niet kleiner dan 0 zijn");
                return gebruikerRepo.GeefGebruiker(gebruikerId);
            }
            catch (Exception ex)
            {
                throw new GebruikerManagerException("GeefGebruiker - Er is een fout opgetreden", ex);
            }
        }

        public bool BestaatGebruiker(int id)
        {
            try
            {
                if (id < 0) throw new GebruikerManagerException("BestaatGebruiker - Id mag niet kleiner dan 0 zijn");
                return gebruikerRepo.BestaatGebruiker(id);
            }
            catch (Exception ex)
            {
                throw new GebruikerManagerException("BestaatGebruiker - Er is een fout opgetreden", ex);
            }
        }
    }
}

