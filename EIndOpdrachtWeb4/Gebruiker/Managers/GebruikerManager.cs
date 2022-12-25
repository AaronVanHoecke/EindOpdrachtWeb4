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

        public int VoegGebruikerToe(Gebruiker gebruiker)
        {
            try
            {
                if (gebruiker == null) throw new GebruikerManagerException("VoegGebruikeToe - Gebruiker mag niet null zijn");
                if (gebruikerRepo.BestaatGebruiker(gebruiker)) throw new GebruikerManagerException("VoegGebruikerToe - Gebruiker bestaat al");
                return gebruikerRepo.VoegGebruikerToe(gebruiker);
            }
            catch (Exception ex)
            {
                throw new GebruikerManagerException("VoegGebruikerToe - Er is een fout opgetreden", ex);
            }
        }

        public void UpdateGebruiker(Gebruiker gebruiker)
        {
            try
            {
                if (gebruiker == null) throw new GebruikerManagerException("UpdateGebruiker - Gebruiker mag niet null zijn");
                if (!gebruikerRepo.BestaatGebruiker(gebruiker)) throw new GebruikerManagerException("UpdateGebruiker - Gebruiker bestaat niet");
                if (gebruikerRepo.IsDezelfde(gebruiker)) throw new GebruikerManagerException("UpdateGebruiker - Gebruiker is dezelfde");
                gebruikerRepo.UpdateGebruiker(gebruiker);
            }
            catch (Exception ex)
            {
                throw new GebruikerManagerException("UpdateGebruiker - Er is een fout opgetreden", ex);
            }
        }

        public void VerwijderGebruiker(Gebruiker gebruiker)
        {
            try
            {
                if (gebruiker == null) throw new GebruikerManagerException("VerwijderGebruiker - Gebruiker mag niet null zijn");
                if (!gebruikerRepo.BestaatGebruiker(gebruiker)) throw new GebruikerManagerException("VerwijderGebruiker - Gebruiker bestaat niet");
                gebruikerRepo.VerwijderGebruiker(gebruiker);
            }
            catch (Exception ex)
            {
                throw new GebruikerManagerException("VerwijderGebruiker - Er is een fout opgetreden", ex);
            }
        }
    }
}

