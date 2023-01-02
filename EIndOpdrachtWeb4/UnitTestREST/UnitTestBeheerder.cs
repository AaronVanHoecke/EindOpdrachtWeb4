using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using RestaurantBL.Interfaces;
using RestaurantBL.Managers;
using RestaurantBL.Model;
using RestaurantRESTbeheerder.Controllers;
using RestaurantRESTbeheerder.Model.Input;
using RestaurantRESTbeheerder.Model.Output;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTestREST
{
    public class UnitTestBeheerder
    {
        private RestaurantBeheerController bc;
        private RestaurantManager rm;
        private Mock<IRestaurantRepository> restoRepo;
        private Mock<IReservatieRepository> reserRepo;
        private ILoggerFactory logger = new LoggerFactory();
        public UnitTestBeheerder()
        {
            restoRepo = new Mock<IRestaurantRepository>();
            reserRepo = new Mock<IReservatieRepository>();
            rm = new RestaurantManager(restoRepo.Object, reserRepo.Object);
            bc = new RestaurantBeheerController(rm, logger);
        }

        [Fact]
        public void GeefRestaurantOpId_Valid()
        {
            restoRepo.Setup(r => r.BestaatRestaurant(5)).Returns(true);
            Restaurant r = new Restaurant("Pitta Stonks", new Locatie(9900, "Eeklo", "weudelaan", "10"), "Turks", "0487686465", "pittastonks@info.be");
            RestaurantRESToutputDTO resto = new RestaurantRESToutputDTO(5, "Pitta Stonks", 9900, "Eeklo", "weudelaan", "10", "Turks", "0487686465", "pittastonks@info.be");
            restoRepo.Setup(r => r.GeefRestaurant(5)).Returns(r);
            var result = bc.GeefRestaurant(5);
            Assert.IsType<OkObjectResult>(result.Result);
            Assert.IsType<RestaurantRESToutputDTO>(((OkObjectResult)result.Result).Value);
        }

        [Fact]
        public void GeefRestaurantOpId_Invalid()
        {
            restoRepo.Setup(r => r.BestaatRestaurant(101)).Returns(false);
            var result = bc.GeefRestaurant(101);
            Assert.IsType<NotFoundResult>(result.Result);
        }

        [Fact]
        public void VoegRestaurantToe_Valid()
        {
            RestaurantRESTinputDTO restaurant = new RestaurantRESTinputDTO
            {
                Naam = "Pitta Stonks",
                Email = "pittastonks@info.be",
                Gemeente = "Eeklo",
                Straat = "weudelaan",
                Huisnummer = "10",
                Keuken = "Turks",
                Postcode = 9900,
                Telefoonnummer = "0487686465"
            };
            restoRepo.Setup(r => r.VoegRestaurantToe(It.IsAny<Restaurant>())).Returns((Restaurant r) => r);
            var result = bc.VoegRestaurantToe(restaurant);
            Assert.IsType<CreatedAtActionResult>(result.Result);
        }

        [Fact]
        public void VoegRestaurantToe_Invalid()
        {
            RestaurantRESTinputDTO restaurant = new RestaurantRESTinputDTO
            {
                Naam = "Pitta Stonks",
                Email = "pittastonks@info.be",
                Gemeente = "Eeklo",
                Straat = "weudelaan",
                Huisnummer = "10",
                Keuken = "Turks",
                Postcode = 9900,
                Telefoonnummer = "0487686465"
            };
            restoRepo.Setup(r => r.VoegRestaurantToe(It.IsAny<Restaurant>())).Returns((Restaurant r) => null);
            var result = bc.VoegRestaurantToe(restaurant);
            Assert.IsType<BadRequestObjectResult>(result.Result);
        }

        [Fact]
        public void UpdateRestaurant_Valid()
        {
            RestaurantRESTinputDTO restaurant = new RestaurantRESTinputDTO
            {
                Naam = "Pitta Stonks",
                Email = "pittastonks@info.be",
                Gemeente = "Eeklo",
                Straat = "weudelaan",
                Huisnummer = "10",
                Keuken = "Turks",
                Postcode = 9900,
                Telefoonnummer = "0487686465"
            };
            restoRepo.Setup(r => r.BestaatRestaurant(5)).Returns(true);
            restoRepo.Setup(r => r.UpdateRestaurant(It.IsAny<Restaurant>())).Returns((Restaurant r) => r);
            var result = bc.UpdateRestaurant(5, restaurant);
            Assert.IsType<CreatedAtActionResult>(result);
        }

        [Fact]
        public void UpdateRestaurant_Invalid()
        {
            RestaurantRESTinputDTO restaurant = new RestaurantRESTinputDTO
            {
                Naam = "Pitta Stonks",
                Email = "pittastonks@info.be",
                Gemeente = "Eeklo",
                Straat = "weudelaan",
                Huisnummer = "10",
                Keuken = "Turks",
                Postcode = 9900,
                Telefoonnummer = "0487686465"
            };
            restoRepo.Setup(r => r.BestaatRestaurant(5)).Returns(true);
            var result = bc.UpdateRestaurant(5, restaurant);
            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public void VerwijderRestaurant_Valid()
        {
            restoRepo.Setup(r => r.BestaatRestaurant(5)).Returns(true);
            reserRepo.Setup(r => r.GeefReservatiesVanRestaurant(5)).Returns(new List<Reservatie>());
            var result = bc.DeleteRestaurant(5);
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public void VerwijderRestaurant_Invalid()
        {
            restoRepo.Setup(r => r.BestaatRestaurant(5)).Returns(false);
            var result = bc.DeleteRestaurant(5);
            Assert.IsType<NotFoundObjectResult>(result);
        }

        [Fact]
        public void GeefReservatiesOpDatum_Valid()
        {
            restoRepo.Setup(r => r.BestaatRestaurant(5)).Returns(true);
            restoRepo.Setup(r => r.GeefReservatiesOpDatum(5, new DateTime(2020, 12, 12))).Returns(new List<Reservatie>());
            var result = bc.GeefReservatiesOpDatum(5, new DateTime(2020, 12, 12));
            Assert.IsType<OkObjectResult>(result.Result);
            Assert.IsType<List<ReservatieRESToutputDTO>>(((OkObjectResult)result.Result).Value);
        }

        [Fact]
        public void GeefReservatiesOpDatum_Invalid()
        {
            restoRepo.Setup(r => r.BestaatRestaurant(5)).Returns(false);
            var result = bc.GeefReservatiesOpDatum(5, new DateTime(2020, 12, 12));
            Assert.IsType<NotFoundObjectResult>(result.Result);
        }

        [Fact]
        public void VoegTafelToe_Valid()
        {
            TafelRESTinputDTO tafel = new TafelRESTinputDTO
            {
                AantalPlaatsen = 4,
                TafelNummer = 1
            };
            restoRepo.Setup(r => r.BestaatRestaurant(5)).Returns(true);
            restoRepo.Setup(r => r.VoegTafelToe(It.IsAny<Tafel>())).Returns((Tafel t) => t);
            var result = bc.VoegTafelToe(5, tafel);
            Assert.IsType<CreatedAtActionResult>(result.Result);
        }

        [Fact]
        public void VoegTafelToe_Invalid()
        {
            TafelRESTinputDTO tafel = new TafelRESTinputDTO
            {
                AantalPlaatsen = 4,
                TafelNummer = 1
            };
            restoRepo.Setup(r => r.BestaatRestaurant(5)).Returns(false);
            var result = bc.VoegTafelToe(5, tafel);
            Assert.IsType<NotFoundObjectResult>(result.Result);
        }
        [Fact]
        public void UpdateTafel_Valid()
        {
            TafelRESTinputDTO tafel = new TafelRESTinputDTO
            {
                AantalPlaatsen = 4,
                TafelNummer = 1
            };
            Tafel t = new Tafel(4, 1, 5);
            restoRepo.Setup(r => r.BestaatRestaurant(5)).Returns(true);
            restoRepo.Setup(r => r.BestaatTafel(It.IsAny<Tafel>())).Returns(true);
            restoRepo.Setup(r => r.UpdateTafel(5, It.IsAny<Tafel>())).Returns((int id, Tafel t) => t);
            var result = bc.UpdateTafel(1, 5, tafel);
            Assert.IsType<CreatedAtActionResult>(result);
        }
        [Fact]
        public void UpdateTafel_Invalid()
        {
            TafelRESTinputDTO tafel = new TafelRESTinputDTO
            {
                AantalPlaatsen = 4,
                TafelNummer = 1
            };
            restoRepo.Setup(r => r.BestaatRestaurant(5)).Returns(true);
            var result = bc.UpdateTafel(1, 5, tafel);
            Assert.IsType<NotFoundObjectResult>(result);
        }
        
        [Fact]
        public void VerwijderTafel_Valid()
        {
            restoRepo.Setup(r => r.BestaatRestaurant(It.IsAny<int>())).Returns(true);
            restoRepo.Setup(r => r.GeefTafel(1, 5)).Returns((int id, int tafelnummer) => new Tafel(4, 1, 5));
            restoRepo.Setup(r => r.BestaatTafel(It.IsAny<Tafel>())).Returns(true);
            reserRepo.Setup(r => r.TafelHeeftReservaties(1)).Returns(false);
            var result = bc.VerwijderTafel(1, 5);
            Assert.IsType<NoContentResult>(result);
        }
        
        [Fact]   
        public void VerwijderTafel_Invalid()
        {
            restoRepo.Setup(r => r.BestaatRestaurant(It.IsAny<int>())).Returns(false);
            var result = bc.VerwijderTafel(1, 5);
            Assert.IsType<NotFoundObjectResult>(result);
        }
        
    }
}
