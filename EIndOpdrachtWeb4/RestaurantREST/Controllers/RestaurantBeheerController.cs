using Microsoft.AspNetCore.Mvc;
using RestaurantBL.Managers;
using RestaurantBL.Model;
using RestaurantDL.Model;
using RestaurantRESTbeheerder.Exceptions;
using RestaurantRESTbeheerder.Mappers;
using RestaurantRESTbeheerder.Model.Input;
using RestaurantRESTbeheerder.Model.Output;

namespace RestaurantRESTbeheerder.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RestaurantBeheerController: ControllerBase
    {
        private RestaurantManager restaurantManager;
        private readonly ILogger logger;
        public RestaurantBeheerController(RestaurantManager restaurantManager, ILoggerFactory logger)
        {
            this.restaurantManager = restaurantManager;
            this.logger = logger.AddFile("LogBeheerder.txt").CreateLogger("BeheerderLogger");
        }

        [HttpGet("GeefRestaurant/{id}")]
        public ActionResult<RestaurantRESToutputDTO> GeefRestaurant(int id)
        {
            try
            {
                if (!restaurantManager.BestaatRestaurant(id)) return NotFound();
                Restaurant restaurant = restaurantManager.GeefRestaurant(id);
                RestaurantRESToutputDTO restaurantDTO = MapRestaurantFromDomain.MapFromDomain(restaurant);
                return Ok(restaurantDTO);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost("VoegRestaurantToe")]
        public ActionResult<RestaurantRESToutputDTO> VoegRestaurantToe([FromBody] RestaurantRESTinputDTO restaurant)
        {
            try
            {
                Restaurant r = restaurantManager.VoegRestaurantToe(MapRestaurantToDomain.MapToDomain(restaurant));
                return CreatedAtAction(nameof(GeefRestaurant), new { id = r.ID }, MapRestaurantFromDomain.MapFromDomain(r));
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        [HttpPut("UpdateRestaurant/{id}")]
        public IActionResult UpdateRestaurant(int id, [FromBody] RestaurantRESTinputDTO restaurant)
        {
            try
            {
                if (!restaurantManager.BestaatRestaurant(id)) return BadRequest("Restaurant bestaat niet");
                Restaurant r = MapRestaurantToDomain.MapToDomain(restaurant);
                r.ZetId(id);
                r = restaurantManager.UpdateRestaurant(r);
                return CreatedAtAction(nameof(GeefRestaurant), new { id = r.ID }, MapRestaurantFromDomain.MapFromDomain(r));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("DeleteRestaurant/{id}")]
        public IActionResult DeleteRestaurant(int id)
        {
            try
            {
                if (!restaurantManager.BestaatRestaurant(id)) return NotFound("Restaurant bestaat niet");
                restaurantManager.VerwijderRestaurant(id);
                return NoContent();
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        [HttpGet("GeefReservatiesOpDatum/{id}/{datum}")]
        public ActionResult<ReservatieRESToutputDTO> GeefReservatiesOpDatum(int id, DateTime datum)
        {
            try
            {
                if (!restaurantManager.BestaatRestaurant(id)) return NotFound("Restaurant bestaat niet");
                List<Reservatie> res = restaurantManager.GeefReservatiesOpDAtum(id, datum);
                List<ReservatieRESToutputDTO> resList = res.Select(r => MapReservatieFromDomain.MapFromDomain(r)).ToList();
                return Ok(resList);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost("VoegTafelToe/{restaurantId}")]
        public ActionResult<TafelRESToutputDTO> VoegTafelToe(int restaurantId, [FromBody]TafelRESTinputDTO tafel)
        {
            try
            {
                if (!restaurantManager.BestaatRestaurant(restaurantId)) return NotFound("Restaurant bestaat niet");
                Tafel t = restaurantManager.VoegTafelToe(MapTafelToDomain.MapToDomain(tafel, restaurantId));
                return CreatedAtAction(nameof(GeefTafel), new { id = t.ID, restaurantId = t.RestaurantID}, MapTafelFromDomain.MapFromDomain(t));
            }
            catch (Exception e)
            {

                return BadRequest(e.Message);
            }
        }

        [HttpGet("GeefTafel/{id}/{restaurantId}")]
        public ActionResult<TafelRESToutputDTO> GeefTafel(int id, int restaurantId)
        {
            try
            {
                if (!restaurantManager.BestaatRestaurant(restaurantId)) return NotFound("Restaurant bestaat niet");
                Tafel t = restaurantManager.GeefTafel(id, restaurantId);
                return Ok(MapTafelFromDomain.MapFromDomain(t));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPut("UpdateTafel/{id}/{restaurantId}")]
        public IActionResult UpdateTafel(int id, int restaurantId,[FromBody] TafelRESTinputDTO tafel)
        {
            try
            {
                if (!restaurantManager.BestaatRestaurant(restaurantId)) return NotFound("Restaurant bestaat niet");
                Tafel t = MapTafelToDomain.MapToDomain(tafel, restaurantId);
                t.ZetId(id);
                if (!restaurantManager.BestaatTafel(t)) return NotFound("Tafel bestaat niet");
                t = restaurantManager.UpdateTafel(restaurantId, t);
                return CreatedAtAction(nameof(GeefTafel), new { id = t.ID, restaurantId = t.RestaurantID }, MapTafelFromDomain.MapFromDomain(t));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("DeleteTafel/{id}/{restaurantId}")]
        public IActionResult VerwijderTafel(int id, int restaurantId)
        {
            try
            {
                if (!restaurantManager.BestaatRestaurant(restaurantId)) return NotFound("Restaurant bestaat niet");
                Tafel t = restaurantManager.GeefTafel(id, restaurantId);
                restaurantManager.VerwijderTafel(restaurantId, t);
                return NoContent();
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }
    }
}
