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
    public class ReservatieManager
    {
        private IReservatieRepository reservatieRepo;

        public ReservatieManager(IReservatieRepository reservatieRepo)
        {
            this.reservatieRepo = reservatieRepo;
        }

        public int VoegReservatieToe(Reservatie reservatie)
        {
            try
            {
                if (reservatie == null) throw new ReservatieManagerException("Reservatie mag niet null zijn.");
                return reservatieRepo.VoegReservatieToe(reservatie);
            }
            catch (Exception)
            {
                throw new ReservatieManagerException("VoegReservatieToe - Er is een fout opgetreden");
            }
        }

        public void UpdateReservatie(Reservatie reservatie)
        {
            try
            {
                if (reservatie == null) throw new ReservatieManagerException("Reservatie mag niet null zijn.");
                if (!reservatieRepo.BestaatReservatie(reservatie)) throw new ReservatieManagerException("Reservatie bestaat niet.");
                if (reservatieRepo.IsDezelfde(reservatie)) throw new ReservatieManagerException("Reservatie is dezelfde.");
                reservatieRepo.UpdateReservatie(reservatie);
            }
            catch (Exception ex)
            {
                throw new ReservatieManagerException("UpdateReservatie - Er is een fout opgetreden", ex);
            }
        }

        public void VerwijderReservatie(Reservatie reservatie)
        {
            try
            {
                if (reservatie == null) throw new ReservatieManagerException("Reservatie mag niet null zijn.");
                if (!reservatieRepo.BestaatReservatie(reservatie)) throw new ReservatieManagerException("Reservatie bestaat niet.");
                reservatieRepo.VerwijderReservatie(reservatie);
            }
            catch (Exception ex)
            {
                throw new ReservatieManagerException("VerwijderReservatie - Er is een fout opgetreden", ex);
            }
        }

        public IReadOnlyList<Reservatie> GeefReservaties(Restaurant? restaurant, Gebruiker? gebruiker, DateTime? begindatum, DateTime? einddatum)
        {
            if (restaurant != null) return reservatieRepo.GeefReservatiesVanRestaurant(restaurant).AsReadOnly();
            if (gebruiker != null) return reservatieRepo.GeefReservatiesVanGebruiker(gebruiker).AsReadOnly();
            if (begindatum.HasValue || einddatum.HasValue) return reservatieRepo.GeefReservatiesOpDatum(begindatum, einddatum).AsReadOnly();
            return reservatieRepo.GeefReservaties().AsReadOnly();
        }
    }
}
