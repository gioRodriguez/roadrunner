﻿using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.SignalR;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Roadrunner.BusinessInterfaces;
using Roadrunner.Types;
using Roadrunner.Web.Hubs;
using Roadrunner.Web.Models;

namespace Roadrunner.Test.Web
{
    [TestClass]
    public class DriversApiControllerTest
    {
        private Mock<IDriversProcessor> _mockDriversProcessor;

        [TestInitialize]
        public void Setup()
        {
            _mockDriversProcessor = new Mock<IDriversProcessor>();
        }

        [TestMethod]
        public void DriverReadyAtPositionTest()
        {
            // Arrange            
            var driversApiCtrl = new DriverHub(_mockDriversProcessor.Object);

            // Act
            driversApiCtrl.DriverReadyAtPosition(new ReqPositionModel { X = 1, Y = 2 }).Wait();

            // Assert
            _mockDriversProcessor.Verify(drivers => drivers.DriverReadyAtPositionAsync(It.IsAny<Position>()));
        }
    }
}
