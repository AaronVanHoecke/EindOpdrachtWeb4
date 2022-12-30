using Microsoft.AspNetCore.Mvc;
using RestaurantBL.Managers;
using RestaurantBL.Model;
using RestaurantRESTgebruiker.Mappers;
using RestaurantRESTgebruiker.Model.Input;
using RestaurantRESTgebruiker.Model.Output;

namespace RestaurantRESTgebruiker.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RestaurantGebruikerController : ControllerBase
    {
        private RestaurantManager restaurantManager;
        private GebruikerManager gebruikerManager;
        private ReservatieManager reserveringManager;
        public RestaurantGebruikerController(RestaurantManager restaurantManager, GebruikerManager gebruikerManager, ReservatieManager reserveringManager)
        {
            this.restaurantManager = restaurantManager;
            this.gebruikerManager = gebruikerManager;
            this.reserveringManager = reserveringManager;
        }
        [HttpGet("{id}")]
        public ActionResult<GebruikerRESToutputDTO> GeefGebruiker(int id)
        {
            try
            {
                GebruikerRESToutputDTO gebruikerDTO = MapGebruikerFromDomain.MapFromDomain(id, gebruikerManager);
                return Ok(gebruikerDTO);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        [HttpPost]
        public IActionResult VoegGebruikerToe([FromBody] GebruikerRESTinputDTO gebruiker)
        {
            try
            {
                Gebruiker g = MapGebruikerToDomain.MapToDomain(gebruiker);
                g = gebruikerManager.VoegGebruikerToe(g);
                return CreatedAtAction(nameof(GeefGebruiker), new { id = g.Id }, MapGebruikerFromDomain.MapFromDomain(g.Id, gebruikerManager));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public IActionResult UpdateGebruiker(int id, [FromBody] GebruikerRESTinputDTO gebruiker)
        {
            try
            {
                if (!gebruikerManager.BestaatGebruiker(id)) return BadRequest("Gebruiker bestaat niet");
                Gebruiker g = MapGebruikerToDomain.MapToDomain(gebruiker);
                g.ZetId(id);
                g = gebruikerManager.UpdateGebruiker(g);
                return CreatedAtAction(nameof(GeefGebruiker), new { id = g.Id }, MapGebruikerFromDomain.MapFromDomain(g.Id, gebruikerManager));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public IActionResult VerwijderGebruiker(int id)
        {
            try
            {
                if (!gebruikerManager.BestaatGebruiker(id)) return BadRequest("Gebruiker bestaat niet");
                gebruikerManager.VerwijderGebruiker(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("/filter")]
        public ActionResult<RestaurantRESToutputDTO> GeefRestaurant([FromQuery]int? postcode, [FromQuery]string? keuken)
        {
            try
            {
                List<Restaurant> res = restaurantManager.GeefRestaurants(postcode, keuken);
                List<RestaurantRESToutputDTO> restaurantDTO = res.Select(r => MapRestaurantFromDomain.MapFromDomain(r.ID, restaurantManager)).ToList();
                return Ok(restaurantDTO);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("/restaurant")]
        public ActionResult<RestaurantRESToutputDTO> GeefRestaurant([FromQuery] DateTime datum , [FromQuery] int aantalPlaatsen)
        {
            try
            {
                List<Restaurant> res = restaurantManager.GeefRestaurantsOpDatum(datum, aantalPlaatsen);
                List<RestaurantRESToutputDTO> restaurantDTO = res.Select(r => MapRestaurantFromDomain.MapFromDomain(r.ID, restaurantManager)).ToList();
                return Ok(restaurantDTO);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
