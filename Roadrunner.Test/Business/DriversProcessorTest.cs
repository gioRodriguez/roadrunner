using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Roadrunner.BusinessLayer;
using Roadrunner.DataInterfaces;
using Roadrunner.Types;
using Roadrunner.Utils.Identity;

namespace Roadrunner.Test.Business
{
    [TestClass]
    public class DriversProcessorTest
    {
        private Mock<IRoadrunnerIdentity> _mockIdentity;
        private Mock<IDriversRepository> _mockDriversRepository;
        private Mock<IHistoryRepository> _mockHistoryRepository;

        [TestInitialize]
        public void Setup()
        {
            _mockIdentity = new Mock<IRoadrunnerIdentity>();
            _mockDriversRepository = new Mock<IDriversRepository>();
            _mockHistoryRepository = new Mock<IHistoryRepository>();            

            _mockIdentity
                .Setup(identity => identity.IsAuthenticated())
                .Returns(true);
            _mockIdentity
                .Setup(identity => identity.GetUserId())
                .Returns("driverId");
        }

        [TestMethod]
        public void DriverReadyAtPositionAsyncTest()
        {
            // Arrange            
            var position = Position.Create(1, 1);                        
            var driversProcessor = new DriversProcessor(_mockIdentity.Object, _mockDriversRepository.Object, _mockHistoryRepository.Object);

            // Act
            driversProcessor.DriverReadyAtPositionAsync(position).Wait();

            // Assert
            _mockDriversRepository.Verify(driversRepo => driversRepo.DriverReadyAtPositionAsync("driverId", position));
            _mockHistoryRepository.Verify(driversRepo => driversRepo.DriverReadyAtPositionAsync("driverId", position));
        }
    }
}
