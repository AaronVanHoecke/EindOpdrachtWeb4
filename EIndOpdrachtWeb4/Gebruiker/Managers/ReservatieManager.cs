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

        public Reservatie VoegReservatieToe(Reservatie reservatie)
        {
            try
            {
                if (reservatie == null) throw new ReservatieManagerException("Reservatie mag niet null zijn.");
                if (reservatieRepo.BestaatReservatie(reservatie)) throw new ReservatieManagerException("Reservatie bestaat al.");
                return reservatieRepo.VoegReservatieToe(reservatie);
            }
            catch (Exception)
            {
                throw new ReservatieManagerException("VoegReservatieToe - Er is een fout opgetreden");
            }
        }

        public Reservatie UpdateReservatie(Reservatie reservatie)
        {
            try
            {
                if (reservatie == null) throw new ReservatieManagerException("Reservatie mag niet null zijn.");
                if (!reservatieRepo.BestaatReservatie(reservatie.ReservatieID)) throw new ReservatieManagerException("Reservatie bestaat niet.");
                if (reservatie.ReservatieDetail < DateTime.Now) throw new ReservatieManagerException("Reservatie is al geweest.");
                if (reservatieRepo.IsDezelfde(reservatie)) throw new ReservatieManagerException("Reservatie is dezelfde.");
                return reservatieRepo.UpdateReservatie(reservatie);
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
                if (!reservatieRepo.BestaatReservatie(reservatie.ReservatieID)) throw new ReservatieManagerException("Reservatie bestaat niet.");
                if (reservatie.ReservatieDetail < DateTime.Now) throw new ReservatieManagerException("Reservatie is al geweest.");
                reservatieRepo.VerwijderReservatie(reservatie.ReservatieID);
            }
            catch (Exception ex)
            {
                throw new ReservatieManagerException("VerwijderReservatie - Er is een fout opgetreden", ex);
            }
        }

        public IReadOnlyList<Reservatie> GeefReservaties(Restaurant? restaurant, Gebruiker? gebruiker, DateTime? begindatum, DateTime? einddatum)
        {
            if (restaurant != null) return reservatieRepo.GeefReservatiesVanRestaurant(restaurant.ID).AsReadOnly();
            if (gebruiker != null) return reservatieRepo.GeefReservatiesVanGebruiker(gebruiker).AsReadOnly();
            if (begindatum.HasValue || einddatum.HasValue) return reservatieRepo.GeefReservatiesOpDatum(begindatum, einddatum).AsReadOnly();
            return reservatieRepo.GeefReservaties().AsReadOnly();
        }

        public Reservatie GeefReservatie(int id)
        {
            if (id <= 0) throw new ReservatieManagerException("Id moet groter zijn dan 0.");
            if (!reservatieRepo.BestaatReservatie(id)) throw new ReservatieManagerException("Reservatie bestaat niet.");
            return reservatieRepo.GeefReservatie(id);
        }

        public bool BestaatReservatie(int id)
        {
            if (id <= 0) throw new ReservatieManagerException("Id moet groter zijn dan 0.");
            return reservatieRepo.BestaatReservatie(id);
        }

        public List<Reservatie> GeefReservatiesVoorgebruikerOpDatum(Gebruiker g, DateTime? datumB, DateTime? datumE)
        {
            if (g.Id <= 0) throw new ReservatieManagerException("Id moet groter zijn dan 0.");
            if (datumB.HasValue && datumE.HasValue) return reservatieRepo.GeefReservatiesVoorgebruikerOpDatum(g.Id, datumB, datumE);
            if (datumB.HasValue) return reservatieRepo.GeefReservatiesVanGebruikerOpDatum(g.Id, datumB);
            if (datumE.HasValue) return reservatieRepo.GeefReservatiesVanGebruikerOpDatum(g.Id, datumE);
            return reservatieRepo.GeefReservatiesVanGebruiker(g);
        }
    }
}
