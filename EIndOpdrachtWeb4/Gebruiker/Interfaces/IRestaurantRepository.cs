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
        Restaurant VoegRestaurantToe(Restaurant restaurant);
        Restaurant UpdateRestaurant(Restaurant restaurant);
        void VerwijderRestaurant(int restaurantId);
        List<Restaurant> GeefRestaurants();
        Tafel VoegTafelToe(Tafel tafel);
        Tafel UpdateTafel(int restaurantId, Tafel tafel);
        void VerwijderTafel(int restaurantId, int tafelId);
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
        List<Reservatie> GeefReservatiesOpDatum(int id, DateTime datum);
        Tafel GeefTafel(int tafelId, int restaurantId);
        List<Restaurant> GeefRestaurant(int value, string keuken);
        List<Restaurant> GeefRestaurant(string keuken);
        List<Restaurant> GeefRestaurantPostcode(int postcode);
        List<Restaurant> GeefRestaurantsOpDatum(DateTime datum, int aantalPlaatsen);
    }
}
