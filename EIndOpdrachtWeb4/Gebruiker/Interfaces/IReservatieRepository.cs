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
        int VoegReservatieToe(Reservatie reservatie);
        void UpdateReservatie(Reservatie reservatie);
        void VerwijderReservatie(Reservatie reservatie);
        List<Reservatie> GeefReservaties();
        List<Reservatie> GeefReservatiesVanRestaurant(Restaurant restaurant);
        List<Reservatie> GeefReservatiesVanGebruiker(Gebruiker gebruiker);
        List<Reservatie> GeefReservatiesOpDatum(DateTime? begindatum, DateTime? einddatum);
        bool BestaatReservatie(Reservatie reservatie);
        bool IsDezelfde(Reservatie reservatie);
    }
}
