using System;
using System.Threading.Tasks;
using CrossSolar.Controllers;
using CrossSolar.Models;
using CrossSolar.Repository;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace CrossSolar.Tests.Controller
{
    public class AnalyticsControllerTests
    {
        private readonly Mock<IAnalyticsRepository> _analyticsRepositoryMock = new Mock<IAnalyticsRepository>();
        private readonly AnalyticsController _analyticsController;
        private static Mock<AnalyticsController> _mockAnalyticsController;
        private readonly Mock<IPanelRepository> _panelRepositoryMock = new Mock<IPanelRepository>();
        public AnalyticsControllerTests()
        {
            _analyticsController =
                new AnalyticsController(_analyticsRepositoryMock.Object, _panelRepositoryMock.Object);
            _mockAnalyticsController = new Mock<AnalyticsController>();
        }


        [Fact]
        public async Task Post_ShouldInsertOneHourElectricity()
        {
            var oneHourElectricityModel = new OneHourElectricityModel
            {
                Id = 0,
                DateTime = DateTime.Now,
                KiloWatt = 123456
            };

            // Arrange

            // Act
            var result = await _analyticsController.Post(1, oneHourElectricityModel);

            // Assert
            Assert.NotNull(result);

            var createdResult = result as CreatedResult;
            Assert.NotNull(createdResult);
            Assert.Equal(201, createdResult.StatusCode);
        }

        [Fact]
        public async Task Get_ShouldReturn_NotFound()
        {
            // Arrange

            // Act
            var result = await _analyticsController.Get(1);

            // Assert
            Assert.NotNull(result);

            var notFoundResult = result as NotFoundResult;
            Assert.NotNull(notFoundResult);
            Assert.Equal(404, notFoundResult.StatusCode);
        }

        [Fact]
        public async Task Get_ShouldReturn()
        {
            var oneHourElectricityModel = new OneHourElectricityModel
            {
                DateTime = DateTime.Now,
                Id = 23,
                KiloWatt = 123456
            };

            // Assign:
            //_mockAnalyticsController.Setup(x => x.Post(2, oneHourElectricityModel));
            var postResult = await _analyticsController.Post(2, oneHourElectricityModel);

            // Act
            var result = await _analyticsController.Get(1);
           // _mockAnalyticsController.Verify(x=>x.Get(2));

            // Assert
            var notFoundResult = result as NotFoundResult;
            Assert.NotNull(notFoundResult);
            Assert.Equal(404, notFoundResult.StatusCode);
        }
        [Fact]
        public async Task DayResult_ShouldReturn()
        {
            // Assign:

            // Act
            var dayResults = await _analyticsController.DayResults(2);

            // Assert
            Assert.NotNull(dayResults);

            var okResult = dayResults as OkResult;
            Assert.NotNull(okResult);
            Assert.Equal(200, okResult.StatusCode);
        }
    }
}