using Microsoft.AspNetCore.Mvc;
using RestaurantBL.Managers;
using RestaurantBL.Model;
using RestaurantDL.Migrations;
using RestaurantDL.Model;
using RestaurantRESTbeheerder.Mappers;
using RestaurantRESTgebruiker.Mappers;
using RestaurantRESTgebruiker.Model.Input;
using RestaurantRESTgebruiker.Model.Output;
using System;

namespace RestaurantRESTgebruiker.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RestaurantGebruikerController : ControllerBase
    {
        private RestaurantManager restaurantManager;
        private GebruikerManager gebruikerManager;
        private ReservatieManager reserveringManager;
        private readonly ILogger logger;
        public RestaurantGebruikerController(RestaurantManager restaurantManager, GebruikerManager gebruikerManager, ReservatieManager reserveringManager, ILoggerFactory logger)
        {
            this.restaurantManager = restaurantManager;
            this.gebruikerManager = gebruikerManager;
            this.reserveringManager = reserveringManager;
            this.logger = logger.AddFile("LogGebruiker.txt").CreateLogger("GebruikerLogger");
        }
        [HttpGet("GeefGebruiker/{id}")]
        public ActionResult<GebruikerRESToutputDTO> GeefGebruiker(int id)
        {
            try
            {
                Gebruiker gebruiker = gebruikerManager.GeefGebruiker(id);
                GebruikerRESToutputDTO gebruikerDTO = MapGebruikerFromDomain.MapFromDomain(gebruiker);
                return Ok(gebruikerDTO);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        [HttpPost("VoegGebruikerToe")]
        public IActionResult VoegGebruikerToe([FromBody] GebruikerRESTinputDTO gebruiker)
        {
            try
            {
                Gebruiker g = MapGebruikerToDomain.MapToDomain(gebruiker);
                g = gebruikerManager.VoegGebruikerToe(g);
                return CreatedAtAction(nameof(GeefGebruiker), new { id = g.Id }, MapGebruikerFromDomain.MapFromDomain(g));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("UpdateGebruiker/{id}")]
        public IActionResult UpdateGebruiker(int id, [FromBody] GebruikerRESTinputDTO gebruiker)
        {
            try
            {
                if (!gebruikerManager.BestaatGebruiker(id)) return NotFound("Gebruiker bestaat niet");
                Gebruiker g = MapGebruikerToDomain.MapToDomain(gebruiker);
                g.ZetId(id);
                g = gebruikerManager.UpdateGebruiker(g);
                return CreatedAtAction(nameof(GeefGebruiker), new { id = g.Id }, MapGebruikerFromDomain.MapFromDomain(g));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("VerwijderGebruiker/{id}")]
        public IActionResult VerwijderGebruiker(int id)
        {
            try
            {
                if (!gebruikerManager.BestaatGebruiker(id)) return NotFound("Gebruiker bestaat niet");
                gebruikerManager.VerwijderGebruiker(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("GeefRestaurant/filter")]
        public ActionResult<RestaurantRESToutputDTO> GeefRestaurant([FromQuery] int? postcode, [FromQuery] string? keuken)
        {
            try
            {
                List<Restaurant> res = restaurantManager.GeefRestaurants(postcode, keuken);
                List<RestaurantRESToutputDTO> restaurantDTO = res.Select(r => MapRestaurantFromDomain.MapFromDomain(r)).ToList();
                return Ok(restaurantDTO);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("GeefRestaurant")]
        public ActionResult<RestaurantRESToutputDTO> GeefRestaurant([FromQuery] DateTime datum, [FromQuery] int aantalPlaatsen)
        {
            try
            {
                List<Restaurant> res = restaurantManager.GeefRestaurantsOpDatum(datum, aantalPlaatsen);
                List<RestaurantRESToutputDTO> restaurantDTO = res.Select(r => MapRestaurantFromDomain.MapFromDomain(r)).ToList();
                return Ok(restaurantDTO);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost("VoegReserveringToe")]
        public IActionResult VoegReserveringToe([FromBody] ReservatieRESTinputDTO reservatie)
        {
            try
            {
                double atMinuteInBlock = reservatie.Datum.TimeOfDay.TotalMinutes % 30;
                if (atMinuteInBlock < 15) reservatie.Datum = reservatie.Datum.AddMinutes(-atMinuteInBlock);
                else
                {
                    double minutesToAdd = 30 - atMinuteInBlock;
                    reservatie.Datum = reservatie.Datum.AddMinutes(minutesToAdd);
                }
                Restaurant resto = restaurantManager.GeefRestaurant(reservatie.RestaurantID);
                Gebruiker g = gebruikerManager.GeefGebruiker(reservatie.GebruikerID);
                Tafel t = restaurantManager.GeefBeschikbareTafel(reservatie.RestaurantID, reservatie.Datum, reservatie.AantalPlaatsen);
                Reservatie r = MapReservatieToDomain.MapToDomain(reservatie, t, g, resto);
                r = reserveringManager.VoegReservatieToe(r);
                return CreatedAtAction(nameof(GeefReservatie), new { id = r.ReservatieID }, MapReservatieFromDomain.MapFromDomain(r));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("GeefReservatie/{id}")]
        public ActionResult<ReservatieRESToutputDTO> GeefReservatie(int id)
        {
            try
            {
                Reservatie r = reserveringManager.GeefReservatie(id);
                ReservatieRESToutputDTO reservatieDTO = MapReservatieFromDomain.MapFromDomain(r);
                return Ok(reservatieDTO);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPut("UpdateReservatie/{id}")]
        public IActionResult UpdateReservatie(int id, [FromBody] ReservatieRESTinputDTO reservatie)
        {
            try
            {
                if (!reserveringManager.BestaatReservatie(id)) return NotFound("Reservatie bestaat niet");
                double atMinuteInBlock = reservatie.Datum.TimeOfDay.TotalMinutes % 30;
                if (atMinuteInBlock < 15) reservatie.Datum = reservatie.Datum.AddMinutes(-atMinuteInBlock);
                else
                {
                    double minutesToAdd = 30 - atMinuteInBlock;
                    reservatie.Datum = reservatie.Datum.AddMinutes(minutesToAdd);
                }
                Restaurant resto = restaurantManager.GeefRestaurant(reservatie.RestaurantID);
                Gebruiker g = gebruikerManager.GeefGebruiker(reservatie.GebruikerID);
                Tafel t = restaurantManager.GeefBeschikbareTafel(reservatie.RestaurantID, reservatie.Datum, reservatie.AantalPlaatsen);
                Reservatie r = MapReservatieToDomain.MapToDomain(reservatie, t, g, resto);
                r.ZetId(id);
                r = reserveringManager.UpdateReservatie(r);
                return CreatedAtAction(nameof(GeefReservatie), new { id = r.ReservatieID }, MapReservatieFromDomain.MapFromDomain(r));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("VerwijderReservatie/{id}")]
        public IActionResult VerwijderReservatie(int id)
        {
            try
            {
                if (!reserveringManager.BestaatReservatie(id)) return NotFound("Reservatie bestaat niet");
                Reservatie res = reserveringManager.GeefReservatie(id);
                reserveringManager.VerwijderReservatie(res);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("/reservering/gebruiker")]
        public ActionResult<ReservatieRESToutputDTO> GeefReservatiesVoorgebruikerOpDatum([FromQuery] int gebruikerID, [FromQuery] DateTime? datumB, [FromQuery] DateTime? datumE)
        {
            try
            {
                if (!gebruikerManager.BestaatGebruiker(gebruikerID)) return NotFound("Gebruiker bestaat niet");
                Gebruiker g = gebruikerManager.GeefGebruiker(gebruikerID);
                List<Reservatie> res = reserveringManager.GeefReservatiesVoorgebruikerOpDatum(g, datumB, datumE);
                List<ReservatieRESToutputDTO> reservatieDTO = res.Select(r => MapReservatieFromDomain.MapFromDomain(r)).ToList();
                return Ok(reservatieDTO);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
