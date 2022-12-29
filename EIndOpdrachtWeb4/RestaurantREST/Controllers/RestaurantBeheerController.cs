using Microsoft.AspNetCore.Mvc;
using RestaurantBL.Managers;
using RestaurantBL.Model;
using RestaurantREST.Exceptions;
using RestaurantREST.Mappers;
using RestaurantREST.Model.Input;

namespace RestaurantREST.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RestaurantBeheerController: ControllerBase
    {
        private RestaurantManager restaurantManager;
        public RestaurantBeheerController(RestaurantManager restaurantManager)
        {
            this.restaurantManager = restaurantManager;
        }

        [HttpPost]

        public void VoegRestaurantToe([FromBody] RestaurantRESTinputDTO restaurant)
        {
            try
            {
                restaurantManager.VoegRestaurantToe(MapRestaurantToDomain.MapToDomain(restaurant));
            }
            catch (Exception ex)
            {

                BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public void UpdateRestaurant(int id, [FromBody] RestaurantRESTinputDTO restaurant)
        {
            try
            {
                if (!restaurantManager.BestaatRestaurant(id)) BadRequest("Restaurant bestaat niet");
                Restaurant r = MapRestaurantToDomain.MapToDomain(restaurant);
                r.ZetId(id);
                restaurantManager.UpdateRestaurant(r);
                
            }
            catch (Exception ex)
            {
                BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public void DeleteRestaurant(int id)
        {
            try
            {
                if (!restaurantManager.BestaatRestaurant(id)) BadRequest("Restaurant bestaat niet");
                restaurantManager.VerwijderRestaurant(id);
            }
            catch (Exception ex)
            {

                BadRequest(ex.Message);
            }
        }
    }
}
