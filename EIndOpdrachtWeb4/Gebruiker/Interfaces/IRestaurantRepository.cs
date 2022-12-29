using RestaurantBL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantBL.Interfaces
{
    public interface IRestaurantRepository
    {
        void VoegRestaurantToe(Restaurant restaurant);
        void UpdateRestaurant(Restaurant restaurant);
        void VerwijderRestaurant(int restaurantId);
        List<Restaurant> GeefRestaurants();
        void VoegTafelToe(Restaurant res, Tafel tafel);
        void UpdateTafel(Restaurant res, Tafel tafel);
        void VerwijderTafel(Restaurant res, int tafelId);
        List<Restaurant> GetRestaurants(Locatie location, string keuken);
        List<Tafel> GeefTafelsVanRestaurant(Restaurant restaurant);
        List<Tafel> GeefBeschikbareTafelsRestaurant(Restaurant restaurant);
        List<Tafel> GeefBeschikbareTafels(DateTime datum, Locatie locatie, string keuken);
        bool BestaatRestaurant(Restaurant restaurant);
        bool IsDezelfde(Restaurant restaurant);
        bool BestaatTafel(Tafel tafel);
        bool IsDezelfde(Tafel tafel);
        bool BestaatRestaurant(int id);
        Restaurant GeefRestaurant(int id);
    }
}
