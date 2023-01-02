using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Identity.Client;
using Moq;
using RestaurantBL.Interfaces;
using RestaurantBL.Managers;
using RestaurantBL.Model;
using RestaurantRESTgebruiker.Controllers;
using RestaurantRESTgebruiker.Model.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTestREST
{
    public class UnitTestGebruiker
    {
        private RestaurantGebruikerController gc;
        private RestaurantManager rm;
        private ReservatieManager resm;
        private GebruikerManager gm;
        private Mock<IRestaurantRepository> restoRepo;
        private Mock<IReservatieRepository> reserRepo;
        private Mock<IGebruikerRepository> gebruikerRepo;
        private ILoggerFactory logger = new LoggerFactory();

        public UnitTestGebruiker()
        {
            restoRepo = new Mock<IRestaurantRepository>();
            reserRepo = new Mock<IReservatieRepository>();
            gebruikerRepo = new Mock<IGebruikerRepository>();
            rm = new RestaurantManager(restoRepo.Object, reserRepo.Object);
            resm = new ReservatieManager(reserRepo.Object);
            gm = new GebruikerManager(gebruikerRepo.Object);
            gc = new RestaurantGebruikerController(rm, gm, resm, logger);
        }

        [Fact]
        public void GeefGebruiker_Valid()
        {
            gebruikerRepo.Setup(g => g.GeefGebruiker(It.IsAny<int>())).Returns((int i) => new Gebruiker());
            var result = gc.GeefGebruiker(5);
            Assert.IsType<OkObjectResult>(result.Result);
        }

        [Fact]
        public void GeefGebruiker_Invalid()
        {
            gebruikerRepo.Setup(g => g.GeefGebruiker(It.IsAny<int>())).Returns((int i) => null);
            var result = gc.GeefGebruiker(5);
            Assert.IsType<BadRequestObjectResult>(result.Result);
        }

        [Fact]
        public void VoegGebruikerToe_Valid()
        {
            GebruikerRESTinputDTO gebruiker = new GebruikerRESTinputDTO
            {
                Email = "test@test.com",
                Gemeente = "testgemeente",
                Huisnummer = "123",
                Naam = "tester",
                Postcode = 9800,
                Straat = "testlaan",
                Telefoonnummer = "0475849302"
            };
            gebruikerRepo.Setup(g => g.BestaatGebruiker(It.IsAny<int>())).Returns((int i) => false);
            gebruikerRepo.Setup(g => g.VoegGebruikerToe(It.IsAny<Gebruiker>())).Returns((Gebruiker g) => g);
            var result = gc.VoegGebruikerToe(gebruiker);
            Assert.IsType<CreatedAtActionResult>(result);
        }

        [Fact]
        public void VoegGebruikerToe_Invalid()
        {
            GebruikerRESTinputDTO gebruiker = new GebruikerRESTinputDTO
            {
                Email = "test@test.com",
                Gemeente = "testgemeente",
                Huisnummer = "123",
                Naam = "tester",
                Postcode = 9800,
                Straat = "testlaan",
                Telefoonnummer = "0475849302"
            };
            gebruikerRepo.Setup(g => g.BestaatGebruiker(It.IsAny<int>())).Returns((int i) => true);
            var result = gc.VoegGebruikerToe(gebruiker);
            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public void UpdateGebruiker_Valid()
        {
            GebruikerRESTinputDTO gebruiker = new GebruikerRESTinputDTO
            {
                Email = "test@test.com",
                Gemeente = "testgemeente",
                Huisnummer = "123",
                Naam = "tester",
                Postcode = 9800,
                Straat = "testlaan",
                Telefoonnummer = "0475849302"
            };
            gebruikerRepo.Setup(g => g.BestaatGebruiker(It.IsAny<int>())).Returns((int i) => true);
            gebruikerRepo.Setup(g => g.UpdateGebruiker(It.IsAny<Gebruiker>())).Returns((Gebruiker g) => g);
            gebruikerRepo.Setup(g => g.IsDezelfde(It.IsAny<Gebruiker>())).Returns(false);
            var result = gc.UpdateGebruiker(1, gebruiker);
            Assert.IsType<CreatedAtActionResult>(result);
        }

        [Fact]
        public void UpdateGebruiker_Invalid()
        {
            GebruikerRESTinputDTO gebruiker = new GebruikerRESTinputDTO
            {
                Email = "test@test.com",
                Gemeente = "testgemeente",
                Huisnummer = "123",
                Naam = "tester",
                Postcode = 9800,
                Straat = "testlaan",
                Telefoonnummer = "0475849302"
            };
            gebruikerRepo.Setup(g => g.BestaatGebruiker(It.IsAny<int>())).Returns((int i) => false);
            var result = gc.UpdateGebruiker(1, gebruiker);
            Assert.IsType<NotFoundObjectResult>(result);
        }

        [Fact]
        public void VerwijderGebruiker_Valid()
        {
            gebruikerRepo.Setup(g => g.BestaatGebruiker(It.IsAny<int>())).Returns((int i) => true);
            var result = gc.VerwijderGebruiker(5);
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public void VerwijderGebruiker_Invalid()
        {
            gebruikerRepo.Setup(g => g.BestaatGebruiker(It.IsAny<int>())).Returns((int i) => false);
            var result = gc.VerwijderGebruiker(5);
            Assert.IsType<NotFoundObjectResult>(result);
        }

        [Fact]
        public void GeefRestaurant_Valid()
        {
            restoRepo.Setup(r => r.GeefRestaurantsOpDatum(It.IsAny<DateTime>(), It.IsAny<int>())).Returns((DateTime dt, int i) => new List<Restaurant>());
            var result = gc.GeefRestaurant(DateTime.Now.Date, 4);
            Assert.IsType<OkObjectResult>(result.Result);
        }

        [Fact]
        public void GeefRestaurant_Invalid()
        {
            restoRepo.Setup(r => r.GeefRestaurantsOpDatum(It.IsAny<DateTime>(), It.IsAny<int>())).Returns((DateTime dt, int i) => new List<Restaurant>());
            var result = gc.GeefRestaurant(DateTime.Now.Date, 0);
            Assert.IsType<BadRequestObjectResult>(result.Result);
        }

        [Fact]
        public void GeefRestaurantFilter_Valid()
        {
            restoRepo.Setup(r => r.GeefRestaurant(It.IsAny<int>(), It.IsAny<string>())).Returns((int i, string s) => new List<Restaurant>());
            restoRepo.Setup(r => r.GeefRestaurantPostcode(It.IsAny<int>())).Returns((int i) => new List<Restaurant>());
            restoRepo.Setup(r => r.GeefRestaurant(It.IsAny<string>())).Returns((string s) => new List<Restaurant>());
            var result = gc.GeefRestaurant(9900, "Vlaams");
            Assert.IsType<OkObjectResult>(result.Result);
        }

        [Fact]
        public void GeefRestaurantFilter_Invalid()
        {
            var result = gc.GeefRestaurant(null, null);
            Assert.IsType<BadRequestObjectResult>(result.Result);
        }

        [Fact]
        public void VoegReservatieToe_Valid()
        {
            ReservatieRESTinputDTO res = new ReservatieRESTinputDTO
            {
                AantalPlaatsen = 4,
                Datum = DateTime.Now.AddHours(4),
                GebruikerID = 1,
                RestaurantID = 5
            };
            restoRepo.Setup(r => r.BestaatRestaurant(It.IsAny<int>())).Returns(true);
            restoRepo.Setup(r => r.GeefRestaurant(It.IsAny<int>())).Returns((int i) => new Restaurant());
            gebruikerRepo.Setup(g => g.GeefGebruiker(It.IsAny<int>())).Returns((int i) => new Gebruiker());
            restoRepo.Setup(r => r.GeefBeschikbareTafel(It.IsAny<int>(), It.IsAny<DateTime>(), It.IsAny<int>())).Returns((int i, DateTime dt, int p) => new Tafel());
            reserRepo.Setup(res => res.GeefReservatie(It.IsAny<int>())).Returns((int i) => new Reservatie());
            reserRepo.Setup(res => res.VoegReservatieToe(It.IsAny<Reservatie>())).Returns((Reservatie r) => r);
            var result = gc.VoegReserveringToe(res);
            Assert.IsType<CreatedAtActionResult>(result);
        }

        [Fact]
        public void VoegReservatieToe_Invalid()
        {
            ReservatieRESTinputDTO res = new ReservatieRESTinputDTO
            {
                AantalPlaatsen = 4,
                Datum = DateTime.Now.AddHours(4),
                GebruikerID = 1,
                RestaurantID = 5
            };
            restoRepo.Setup(r => r.BestaatRestaurant(It.IsAny<int>())).Returns(true);
            restoRepo.Setup(r => r.GeefRestaurant(It.IsAny<int>())).Returns((int i) => new Restaurant());
            gebruikerRepo.Setup(g => g.GeefGebruiker(It.IsAny<int>())).Returns((int i) => new Gebruiker());
            restoRepo.Setup(r => r.GeefBeschikbareTafel(It.IsAny<int>(), It.IsAny<DateTime>(), It.IsAny<int>())).Returns((int i, DateTime dt, int p) => new Tafel());
            reserRepo.Setup(res => res.GeefReservatie(It.IsAny<int>())).Returns((int i) => new Reservatie());
            reserRepo.Setup(res => res.BestaatReservatie(It.IsAny<int>())).Returns(false);
            var result = gc.VoegReserveringToe(res);
            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public void GeefReservatie_Valid()
        {
            reserRepo.Setup(res => res.GeefReservatie(It.IsAny<int>())).Returns((int i) => new Reservatie { ContactPersoon = new Gebruiker(), RestaurantInfo = new Restaurant()});
            reserRepo.Setup(res => res.BestaatReservatie(It.IsAny<int>())).Returns(true);
            var result = gc.GeefReservatie(5);
            Assert.IsType<OkObjectResult>(result.Result);
        }

        [Fact]
        public void GeefReservatie_Invalid()
        {
            reserRepo.Setup(res => res.GeefReservatie(It.IsAny<int>())).Returns((int i) => new Reservatie { ContactPersoon = new Gebruiker(), RestaurantInfo = new Restaurant() });
            reserRepo.Setup(res => res.BestaatReservatie(It.IsAny<int>())).Returns(false);
            var result = gc.GeefReservatie(5);
            Assert.IsType<BadRequestObjectResult>(result.Result);
        }

        [Fact]
        public void UpdateReservatieToe_Valid()
        {
            ReservatieRESTinputDTO res = new ReservatieRESTinputDTO
            {
                AantalPlaatsen = 4,
                Datum = DateTime.Now.AddHours(4),
                GebruikerID = 1,
                RestaurantID = 5
            };
            restoRepo.Setup(r => r.BestaatRestaurant(It.IsAny<int>())).Returns(true);
            reserRepo.Setup(res => res.BestaatReservatie(It.IsAny<int>())).Returns(true);
            restoRepo.Setup(r => r.GeefRestaurant(It.IsAny<int>())).Returns((int i) => new Restaurant());
            gebruikerRepo.Setup(g => g.GeefGebruiker(It.IsAny<int>())).Returns((int i) => new Gebruiker());
            restoRepo.Setup(r => r.GeefBeschikbareTafel(It.IsAny<int>(), It.IsAny<DateTime>(), It.IsAny<int>())).Returns((int i, DateTime dt, int p) => new Tafel());
            reserRepo.Setup(res => res.GeefReservatie(It.IsAny<int>())).Returns((int i) => new Reservatie());
            reserRepo.Setup(res => res.UpdateReservatie(It.IsAny<Reservatie>())).Returns((Reservatie r) => r);
            var result = gc.UpdateReservatie(5 ,res);
            Assert.IsType<CreatedAtActionResult>(result);
        }

        [Fact]
        public void VerwijderReservatie_Valid()
        {
            reserRepo.Setup(res => res.BestaatReservatie(It.IsAny<int>())).Returns(true);
            reserRepo.Setup(res => res.GeefReservatie(It.IsAny<int>())).Returns(new Reservatie { ReservatieDetail = DateTime.Now.AddDays(2)});
            var result = gc.VerwijderReservatie(5);
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public void VerwijderReservatie_Invalid()
        {
            reserRepo.Setup(res => res.BestaatReservatie(It.IsAny<int>())).Returns(false);
            var result = gc.VerwijderReservatie(5);
            Assert.IsType<NotFoundObjectResult>(result);
        }

        [Fact]
        public void GeefReservatiesVoorgebruikerOpDatum_Valid()
        {
            gebruikerRepo.Setup(g => g.BestaatGebruiker(It.IsAny<int>())).Returns(true);
            gebruikerRepo.Setup(g => g.GeefGebruiker(It.IsAny<int>())).Returns(new Gebruiker(1, "Aaron", "test@test.com", "0478392019", new Locatie(9800, "test", "test", "10")));
            reserRepo.Setup(res => res.GeefReservatiesVanGebruikerOpDatum(It.IsAny<int>(), It.IsAny<DateTime>())).Returns(new List<Reservatie>());
            reserRepo.Setup(res => res.GeefReservatiesVoorgebruikerOpDatum(It.IsAny<int>(), It.IsAny<DateTime>(), It.IsAny<DateTime>())).Returns(new List<Reservatie>());
            reserRepo.Setup(res => res.GeefReservatiesVanGebruiker(It.IsAny<Gebruiker>())).Returns(new List<Reservatie>());
            var result = gc.GeefReservatiesVoorgebruikerOpDatum(1, DateTime.Now.AddDays(1), DateTime.Now.AddDays(3));
            Assert.IsType<OkObjectResult>(result.Result);
        }

        [Fact]
        public void GeefReservatiesVoorgebruikerOpDatum_Invalid()
        {
            gebruikerRepo.Setup(g => g.BestaatGebruiker(It.IsAny<int>())).Returns(false);
            var result = gc.GeefReservatiesVoorgebruikerOpDatum(1, DateTime.Now.AddDays(1), DateTime.Now.AddDays(3));
            Assert.IsType<NotFoundObjectResult>(result.Result);
        }
    }
}
