using RestaurantBL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantBL.Interfaces
{
    public interface IGebruikerRepository
    {
        int VoegGebruikerToe(Gebruiker gebruiker);
        void UpdateGebruiker(Gebruiker gebruiker);
        void VerwijderGebruiker(Gebruiker gebruiker);
        bool BestaatGebruiker(Gebruiker gebruiker);
        bool IsDezelfde(Gebruiker gebruiker);
    }
}
