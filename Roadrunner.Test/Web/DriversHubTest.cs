using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Roadrunner.BusinessInterfaces;
using Roadrunner.Types;
using Roadrunner.Utils.Identity;
using Roadrunner.Web.Hubs;
using Roadrunner.Web.Models;

namespace Roadrunner.Test.Web
{
    [TestClass]
    public class DriversApiControllerTest
    {
        private Mock<IDriversProcessor> _mockDriversProcessor;
        private Mock<IHubContext<PassengersHub>> _mockHubPassengers;
        private Mock<IRoadrunnerIdentity> _mockRoadrunnerIdentity;

        [TestInitialize]
        public void Setup()
        {
            _mockDriversProcessor = new Mock<IDriversProcessor>();
            _mockHubPassengers = new Mock<IHubContext<PassengersHub>>();
            _mockRoadrunnerIdentity = new Mock<IRoadrunnerIdentity>();
        }

        [TestMethod]
        public void DriverReadyAtPositionTest()
        {
            // Arrange            
            var driverHub = new DriverHub(
                _mockDriversProcessor.Object,
                _mockHubPassengers.Object,
                _mockRoadrunnerIdentity.Object
            );

            // Act
            driverHub.DriverReadyAtPosition(new ReqPositionModel { X = 1, Y = 2 }).Wait();

            // Assert
            _mockDriversProcessor.Verify(drivers => drivers.DriverReadyAtPositionAsync(It.IsAny<Position>()));
        }
    }
}
