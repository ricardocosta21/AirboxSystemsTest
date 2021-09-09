using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AirboxSystems.API.Services.UserLocation;
using Microsoft.AspNetCore.Mvc;
using AirboxSystems.API.Domain.Models;
using MongoDB.Entities;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AirboxSystems.API.Controllers
{
    [Route("/api/[controller]")]
    public class UserLocationController : Controller
    {
        private readonly IUserLocationService _userLocationService;

        public UserLocationController(IUserLocationService userLocationService)
        {
            _userLocationService = userLocationService;
        }

        //1.	Receive a location update for a user
        [HttpPost]
        public bool UpdateUserLocation([FromBody] UserPosition userPosition)
        {
            userPosition.TimeStamp = DateTime.UtcNow;

            userPosition.Location = new Coordinates2D(userPosition.Latitude, userPosition.Longitude);

            return _userLocationService.AddUserLocation(userPosition);
        }

        //2.	Return the current location for a specified user
        [HttpGet("{userId}")]
        [Route("user")]

        // TODO : async IAsyncEnumerable
        public async Task<ActionResult<UserPosition>> GetCurrentUserLocationAsync(int userId)
        {
            var userLocation = await _userLocationService.GetCurrentLocationForUserAsync(userId);
            if (userLocation == null)
                return NotFound("No data");
            return Ok(userLocation);
        }

        //3.	Return the location history for a specified user
        [HttpGet("{userId}")]
        [Route("history")]
        public async Task<ActionResult<IEnumerable<UserPosition>>> GetLocationHistory(int userId)
        {
            var locationHistory = await _userLocationService.GetLocationHistoryAsync(userId);
            if (locationHistory == null)
                return NotFound("No data");
            return Ok(locationHistory);
        }

        //4.	Return the current location for all users
        [HttpGet]
        [Route("users")]
        public async Task<ActionResult<IEnumerable<UserPosition>>> GetLocationForAllUsers()
        {
            var userPosition = await _userLocationService.GetLocationForAllUsersAsync();
            if (userPosition == null)
                return NotFound("No data");
            return Ok(userPosition);
        }

        //5.   Return the current location for all users within a specified area
        [HttpGet]
        [Route("area")]
        public async Task<ActionResult<IEnumerable<UserPosition>>> GetLocationForAllUsersWithinArea()
        {
            var userPosition = await _userLocationService.GetLocationForAllUsersWithinAnAreaAsync();
            if (userPosition == null)
                return NotFound("No data");
            return Ok(userPosition);
        }

    }
}
