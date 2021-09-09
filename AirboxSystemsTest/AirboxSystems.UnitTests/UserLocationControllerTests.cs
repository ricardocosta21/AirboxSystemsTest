using System;
using Moq;
using Xunit;
using AirboxSystems.API.Services.UserLocation;
using AirboxSystems.API.Domain.Models;
using AirboxSystems.API.Controllers;
using FluentAssertions;

namespace AirboxSystems.UnitTests
{
    public class UserLocationControllerTests
    {
        private readonly Mock<IUserLocationService> serviceStub = new();

        [Fact]
        public void UpdateUserLocation_WithValidItem_ReturnsTrue()
        {
            //Arrange
            UserPosition userPosition = new UserPosition()
            {
                Id = "1",
                UserId = 1,
                Longitude = 45.4543,
                Latitude = 44.6544
            };

            serviceStub.Setup(c => c.AddUserLocation(userPosition)).Returns(true);

            //Act
            UserLocationController controller = new UserLocationController(serviceStub.Object);
            var result = controller.UpdateUserLocation(userPosition);

            //Assert
            result.Should().BeTrue();
        }


        [Fact]
        public void UpdateUserLocation_WithInvalidItem_ReturnsFalse()
        {
            //Arrange
            UserPosition userPosition = new UserPosition()
            {
                Id = "1",
                UserId = 1,
                Longitude = 45.4543,
                Latitude = 44.6544
            };

            serviceStub.Setup(c => c.AddUserLocation(userPosition)).Returns(false);

            //Act
            UserLocationController controller = new UserLocationController(serviceStub.Object);
            var result = controller.UpdateUserLocation(userPosition);

            //Assert
            result.Should().BeFalse();
        }


        [Fact]
        public async void GetCurrentUserLocationAsync_WithInvalidItem_ReturnsNull()
        {
            //Arrange
            UserPosition userPosition = new UserPosition()
            {
                Id = "1",
                UserId = 1,
                Longitude = 45.4543,
                Latitude = 44.6544
            };

            serviceStub.Setup(c => c.GetCurrentLocationForUserAsync(userPosition.UserId))
                .ReturnsAsync(userPosition);

            //Act
            UserLocationController controller = new UserLocationController(serviceStub.Object);
            var result = await controller.GetCurrentUserLocationAsync(0);

            //Assert
            result.Value.Should().BeNull();
        }


        [Fact]
        public async void GetCurrentUserLocationAsync_WithValidItem_ReturnsNotNull()
        {
            //Arrange
            UserPosition userPosition = new UserPosition()
            {
                Id = "1",
                UserId = 1,
                Longitude = 45.4543,
                Latitude = 44.6544
            };

            serviceStub.Setup(c => c.GetCurrentLocationForUserAsync(userPosition.UserId))
                .ReturnsAsync(userPosition);

            //Act
            UserLocationController controller = new UserLocationController(serviceStub.Object);
            var result = await controller.GetCurrentUserLocationAsync(userPosition.UserId);

            //Assert
            result.Should().NotBeNull();
        }


    }
}