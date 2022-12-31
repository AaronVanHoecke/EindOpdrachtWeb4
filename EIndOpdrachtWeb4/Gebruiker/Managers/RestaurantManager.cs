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
    public class RestaurantManager
    {
        private IRestaurantRepository restaurantRepo;
        private IReservatieRepository reservatieRepo;
        public RestaurantManager(IRestaurantRepository restaurantRepo, IReservatieRepository reservatieRepo)
        {
            this.restaurantRepo = restaurantRepo;
            this.reservatieRepo = reservatieRepo;
        }

        public Restaurant VoegRestaurantToe(Restaurant restaurant)
        {
            try
            {
                if (restaurant == null) throw new RestaurantManagerException("VoegRestaurantToe - Restaurant mag niet null zijn");
                if (restaurantRepo.BestaatRestaurant(restaurant)) throw new RestaurantManagerException("VoegRestaurantToe - Restaurant bestaat al");
                return restaurantRepo.VoegRestaurantToe(restaurant);
            }
            catch (Exception ex)
            {
                throw new RestaurantManagerException("VoegRestaurantToe - Er is een fout opgetreden", ex);
            }
        }

        public Restaurant UpdateRestaurant(Restaurant restaurant)
        {
            try
            {
                if (restaurant == null) throw new RestaurantManagerException("UpdateRestaurant - Restaurant mag niet null zijn");
                if (!restaurantRepo.BestaatRestaurant(restaurant.ID)) throw new RestaurantManagerException("UpdateRestaurant - Restaurant bestaat niet");
                if (restaurantRepo.IsDezelfde(restaurant)) throw new RestaurantManagerException("UpdateRestaurant - Restaurant is dezelfde");
                return restaurantRepo.UpdateRestaurant(restaurant);
            }
            catch (Exception ex)
            {
                throw new RestaurantManagerException("UpdateRestaurant - Er is een fout opgetreden", ex);
            }
        }

        public void VerwijderRestaurant(int restaurantId)
        {
            try
            {
                if (restaurantId == null) throw new RestaurantManagerException("VerwijderRestaurant - RestaurantId mag niet null zijn");
                if (reservatieRepo.GeefReservatiesVanRestaurant(restaurantId).Any()) throw new RestaurantManagerException("VerwijderRestaurant - Restaurant heeft nog reservaties");
                if (!restaurantRepo.BestaatRestaurant(restaurantId)) throw new RestaurantManagerException("VerwijderRestaurant - Restaurant bestaat niet");
                restaurantRepo.VerwijderRestaurant(restaurantId);
            }
            catch (Exception ex)
            {
                throw new RestaurantManagerException("VerwijderRestaurant - Er is een fout opgetreden", ex);
            }
        }

        public Tafel VoegTafelToe(Tafel tafel)
        {

            try
            {
                if (tafel == null) throw new RestaurantManagerException("VoegTafelToe - Tafel mag niet null zijn");
                if (!restaurantRepo.BestaatRestaurant(tafel.RestaurantID)) throw new RestaurantManagerException("VoegTafelToe - Restaurant bestaat niet");
                if (restaurantRepo.BestaatTafel(tafel)) throw new RestaurantManagerException("VoegTafelToe - Tafelbestaat al");
                return restaurantRepo.VoegTafelToe(tafel);
            }
            catch (Exception ex)
            {
                throw new RestaurantManagerException("VoegTafelToe - Er is een fout opgetreden", ex);
            }
        }

        public Tafel UpdateTafel(int restaurantId, Tafel tafel)
        {
            try
            {
                if (restaurantId == null) throw new RestaurantManagerException("UpdateTafel - Restaurant mag niet null zijn");
                if (!restaurantRepo.BestaatRestaurant(restaurantId)) throw new RestaurantManagerException("UpdateTafel - Restaurant bestaat niet");
                if (!restaurantRepo.BestaatTafel(tafel)) throw new RestaurantManagerException("UpdateTafel - Tafel bestaat niet");
                if (restaurantRepo.IsDezelfde(tafel)) throw new RestaurantManagerException("UpdateTafel - Tafel is dezelfde");
                return restaurantRepo.UpdateTafel(restaurantId, tafel);
            }
            catch (Exception ex)
            {
                throw new RestaurantManagerException("UpdateTafel - Er is een fout opgetreden", ex);
            }
        }

        public void VerwijderTafel(int restaurantId, Tafel tafel)
        {
            try
            {
                if (restaurantId == null) throw new RestaurantManagerException("VerwijderTafel - Restaurant mag niet null zijn");
                if (tafel == null) throw new RestaurantManagerException("VerwijderTafel - Tafel mag niet null zijn");
                if (!restaurantRepo.BestaatRestaurant(restaurantId)) throw new RestaurantManagerException("VerwijderTafel - Restaurant bestaat niet");
                if (!restaurantRepo.BestaatTafel(tafel)) throw new RestaurantManagerException("VerwijderTafel - Tafel bestaat niet");
                if (reservatieRepo.TafelHeeftReservaties(tafel.Tafelnummer)) throw new RestaurantManagerException("VerwijderTafel - Tafel heeft nog reservaties");
                restaurantRepo.VerwijderTafel(restaurantId, tafel.ID);
            }
            catch (Exception ex)
            {
                throw new RestaurantManagerException("VerwijderTafel - Er is een fout opgetreden", ex);
            }
        }

        public bool BestaatRestaurant(int id)
        {
            try
            {
                if (id == null) throw new RestaurantManagerException("BestaatRestaurant - ID mag niet null zijn");
                return restaurantRepo.BestaatRestaurant(id);
            }
            catch (Exception ex)
            {
                throw new RestaurantManagerException("BestaatRestaurant - Er is een fout opgetreden", ex);
            }
        }

        public Restaurant GeefRestaurant(int id)
        {
            try
            {
                if (id == null) throw new RestaurantManagerException("GetRestaurant - ID mag niet null zijn");
                if (!restaurantRepo.BestaatRestaurant(id)) throw new RestaurantManagerException("GetRestaurant - Restaurant bestaat niet");
                return restaurantRepo.GeefRestaurant(id);
            }
            catch (Exception ex)
            {
                throw new RestaurantManagerException("GetRestaurant - Er is een fout opgetreden", ex);
            }
        }

        public List<Reservatie> GeefReservatiesOpDAtum(int id, DateTime datum)
        {
            try
            {
                if (id == null) throw new RestaurantManagerException("GetReservatiesOpDatum - ID mag niet null zijn");
                if (!restaurantRepo.BestaatRestaurant(id)) throw new RestaurantManagerException("GetReservatiesOpDatum - Restaurant bestaat niet");
                return restaurantRepo.GeefReservatiesOpDatum(id, datum);
            }
            catch (Exception ex)
            {
                throw new RestaurantManagerException("GetReservatiesOpDatum - Er is een fout opgetreden", ex);
            }
        }

        public Tafel GeefTafel(int tafelId, int restaurantId)
        {
            if (tafelId == null) throw new RestaurantManagerException("GeefTafel - Tafel mag niet null zijn");
            if (restaurantId == null) throw new RestaurantManagerException("GeefTafel - Restaurant mag niet null zijn");
            if (!restaurantRepo.BestaatRestaurant(restaurantId)) throw new RestaurantManagerException("GeefTafel - Restaurant bestaat niet");
            return restaurantRepo.GeefTafel(tafelId, restaurantId);
        }

        public bool BestaatTafel(Tafel t)
        {
            if (t == null) throw new RestaurantManagerException("BestaatTafel - Tafel mag niet null zijn");
            if (!restaurantRepo.BestaatRestaurant(t.RestaurantID)) throw new RestaurantManagerException("BestaatTafel - Restaurant bestaat niet");
            return restaurantRepo.BestaatTafel(t);
        }

        public List<Restaurant> GeefRestaurants(int? postcode, string? keuken)
        {
            if(!postcode.HasValue && string.IsNullOrWhiteSpace(keuken)) throw new RestaurantManagerException("GeefRestaurant - Geen parameters opgegeven");
            if (postcode.HasValue && !string.IsNullOrWhiteSpace(keuken)) return restaurantRepo.GeefRestaurant(postcode.Value, keuken);
            if (postcode.HasValue) return restaurantRepo.GeefRestaurantPostcode(postcode.Value);
            return restaurantRepo.GeefRestaurant(keuken);
        }

        public List<Restaurant> GeefRestaurantsOpDatum(DateTime datum, int aantalPlaatsen)
        {
            if (aantalPlaatsen <= 0) throw new RestaurantManagerException("GeefRestaurantsOpDatum - Aantal plaatsen moet groter zijn dan 0");
            return restaurantRepo.GeefRestaurantsOpDatum(datum, aantalPlaatsen);
        }

        public Tafel GeefBeschikbareTafel(int restaurantId, DateTime datum, int aantalPlaatsen)
        {
            if (restaurantId == null) throw new RestaurantManagerException("GeefBeschikbareTafel - Restaurant mag niet null zijn");
            if (aantalPlaatsen <= 0) throw new RestaurantManagerException("GeefBeschikbareTafel - Aantal plaatsen moet groter zijn dan 0");
            if (!restaurantRepo.BestaatRestaurant(restaurantId)) throw new RestaurantManagerException("GeefBeschikbareTafel - Restaurant bestaat niet");
            return restaurantRepo.GeefBeschikbareTafel(restaurantId, datum, aantalPlaatsen);
        }
    }
}
