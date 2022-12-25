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
        public RestaurantManager(IRestaurantRepository restaurantRepo)
        {
            this.restaurantRepo = restaurantRepo;
        }

        public void VoegRestaurantToe(Restaurant restaurant)
        {
            try
            {
                if (restaurant == null) throw new RestaurantManagerException("VoegRestaurantToe - Restaurant mag niet null zijn");
                if (restaurantRepo.BestaatRestaurant(restaurant)) throw new RestaurantManagerException("VoegRestaurantToe - Restaurant bestaat al");
                restaurantRepo.VoegRestaurantToe(restaurant);
            }
            catch (Exception ex)
            {
                throw new RestaurantManagerException("VoegRestaurantToe - Er is een fout opgetreden", ex);
            }
        }

        public void UpdateRestaurant(Restaurant restaurant)
        {
            try
            {
                if (restaurant == null) throw new RestaurantManagerException("UpdateRestaurant - Restaurant mag niet null zijn");
                if (!restaurantRepo.BestaatRestaurant(restaurant)) throw new RestaurantManagerException("UpdateRestaurant - Restaurant bestaat niet");
                if (restaurantRepo.IsDezelfde(restaurant)) throw new RestaurantManagerException("UpdateRestaurant - Restaurant is dezelfde");
                restaurantRepo.UpdateRestaurant(restaurant);
            }
            catch (Exception ex)
            {
                throw new RestaurantManagerException("UpdateRestaurant - Er is een fout opgetreden", ex);
            }
        }

        public void VerwijderRestaurant(Restaurant restaurant)
        {
            try
            {
                if (restaurant == null) throw new RestaurantManagerException("VerwijderRestaurant - Restaurant mag niet null zijn");
                if (!restaurantRepo.BestaatRestaurant(restaurant)) throw new RestaurantManagerException("VerwijderRestaurant - Restaurant bestaat niet");
                restaurantRepo.VerwijderRestaurant(restaurant);
            }
            catch (Exception ex)
            {
                throw new RestaurantManagerException("VerwijderRestaurant - Er is een fout opgetreden", ex);
            }
        }

        public void VoegTafelToe(Restaurant res, Tafel tafel)
        {

            try
            {
                if (res == null) throw new RestaurantManagerException("VoegTafelToe - Restaurant mag niet null zijn");
                if (!restaurantRepo.BestaatRestaurant(res)) throw new RestaurantManagerException("VoegTafelToe - Restaurant bestaat niet");
                restaurantRepo.VoegTafelToe(res, tafel);
            }
            catch (Exception ex)
            {
                throw new RestaurantManagerException("VoegTafelToe - Er is een fout opgetreden", ex);
            }
        }

        public void UpdateTafel(Restaurant res, Tafel tafel)
        {
            try
            {
                if (res == null) throw new RestaurantManagerException("UpdateTafel - Restaurant mag niet null zijn");
                if (!restaurantRepo.BestaatRestaurant(res)) throw new RestaurantManagerException("UpdateTafel - Restaurant bestaat niet");
                if (!restaurantRepo.BestaatTafel(tafel)) throw new RestaurantManagerException("UpdateTafel - Tafel bestaat niet");
                if (restaurantRepo.IsDezelfde(tafel)) throw new RestaurantManagerException("UpdateTafel - Tafel is dezelfde");
                restaurantRepo.UpdateTafel(res, tafel);
            }
            catch (Exception ex)
            {
                throw new RestaurantManagerException("UpdateTafel - Er is een fout opgetreden", ex);
            }
        }

        public void VerwijderTafel(Restaurant res, Tafel tafel)
        {
            try
            {
                if (res == null) throw new RestaurantManagerException("VerwijderTafel - Restaurant mag niet null zijn");
                if (!restaurantRepo.BestaatRestaurant(res)) throw new RestaurantManagerException("VerwijderTafel - Restaurant bestaat niet");
                if (!restaurantRepo.BestaatTafel(tafel)) throw new RestaurantManagerException("VerwijderTafel - Tafel bestaat niet");
                restaurantRepo.VerwijderTafel(res, tafel.ID);
            }
            catch (Exception ex)
            {
                throw new RestaurantManagerException("VerwijderTafel - Er is een fout opgetreden", ex);
            }
        }
    }
}
