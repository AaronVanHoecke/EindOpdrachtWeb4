using RestaurantBL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantBL.Interfaces
{
    public interface IReservatieRepository
    {
        Reservatie VoegReservatieToe(Reservatie reservatie);
        Reservatie UpdateReservatie(Reservatie reservatie);
        void VerwijderReservatie(int reservatieid);
        List<Reservatie> GeefReservaties();
        List<Reservatie> GeefReservatiesVanRestaurant(int restaurantId);
        List<Reservatie> GeefReservatiesVanGebruiker(Gebruiker gebruiker);
        List<Reservatie> GeefReservatiesOpDatum(DateTime? begindatum, DateTime? einddatum);
        bool BestaatReservatie(int reservatieId);
        bool IsDezelfde(Reservatie reservatie);
        bool BestaatReservatie(Reservatie reservatie);
        Reservatie GeefReservatie(int id);
        List<Reservatie> GeefReservatiesVoorgebruikerOpDatum(int id, DateTime? datumB, DateTime? datumE);
        List<Reservatie> GeefReservatiesVanGebruikerOpDatum(int id, DateTime? datum);
        bool TafelHeeftReservaties(int tafelnummer);
    }
}
