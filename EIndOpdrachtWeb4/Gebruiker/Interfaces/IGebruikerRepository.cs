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
        Gebruiker VoegGebruikerToe(Gebruiker gebruiker);
        Gebruiker UpdateGebruiker(Gebruiker gebruiker);
        void VerwijderGebruiker(int gebruikerId);
        bool BestaatGebruiker(int gebruikerId);
        bool IsDezelfde(Gebruiker gebruiker);
        Gebruiker GeefGebruiker(int gebruikerId);
        bool GebruikerHeeftReservaties(int gebruikerId);
    }
}
