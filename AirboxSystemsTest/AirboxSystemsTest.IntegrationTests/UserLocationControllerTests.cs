using System;
using Moq;
using Xunit;
using AirboxSystems.API.Services.UserLocation;
using AirboxSystems.API.Domain.Models;
using AirboxSystems.API.Controllers;
using System.Threading.Tasks;
using System.Net;
using Microsoft.AspNetCore.Mvc;
using AirboxSystems.API.Resources;
using FluentAssertions;
using System.Net.Http;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using AirboxSystems.API;

namespace AirboxSystemsTest.IntegrationTests
{
    public class UserLocationControllerTests
    {
        private readonly HttpClient _client;

        public UserLocationControllerTests()
        {
            var server = new TestServer(new WebHostBuilder()
                .UseEnvironment("Development")
                .UseStartup<Startup>());

            _client = server.CreateClient();
        }

        [Fact]
        public async Task When_Invalid_GetCurrentUserLocationAsync_Expect_404()
        {
            var userId = 1;
            
            var response = await _client.GetAsync($"/api/userlocation/user/{userId}");

            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task When_Valid_GetCurrentUserLocationAsync_Expect_200()
        {
            var userId = 123;
            UserPosition expectedUserPosition = new()
            {
                UserId = userId,
            };

            var response = await _client.GetAsync($"/api/userlocation/user/{userId}");

            response.StatusCode.Should().Be(HttpStatusCode.Accepted);
        }
    }
}
