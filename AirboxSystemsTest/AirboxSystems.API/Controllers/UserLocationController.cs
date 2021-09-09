using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AirboxSystems.API.Services.UserLocation;
using Microsoft.AspNetCore.Mvc;
using AirboxSystems.API.Domain.Models;
using AirboxSystems.API.Resources;
using AutoMapper;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AirboxSystems.API.Controllers
{
    [Route("/api/[controller]")]
    public class UserLocationController : Controller
    {
        private readonly IUserLocationService _userLocationService;
        private readonly IMapper _mapper;


        public UserLocationController(IUserLocationService userLocationService, IMapper mapper)
        {
            _userLocationService = userLocationService;
            _mapper = mapper;
        }

        //1.	Receive a location update for a user
        [HttpPost]
        public bool UpdateUserLocation([FromBody] UserPosition userPosition)
        {
            userPosition.TimeStamp = DateTime.UtcNow;  

            return _userLocationService.AddUserLocation(userPosition);
        }

        //2.	Return the current location for a specified user
        [HttpGet("{userId}")]
        [Route("user")]
        public async Task<UserPositionResource> GetCurrentUserLocationAsync(int userId)
        {
            var userLocation = await _userLocationService.GetCurrentLocationForUserAsync(userId);
            var resources = _mapper.Map<UserPosition, UserPositionResource> (userLocation);
            return resources;
        }

        //3.	Return the location history for a specified user
        [HttpGet("{userId}")]
        [Route("history")]
        public async Task<IEnumerable<UserPositionResource>> GetLocationHistory(int userId)
        {
            var locationHistory = await _userLocationService.GetLocationHistoryAsync(userId);
            var resources = _mapper.Map<IEnumerable<UserPosition>, IEnumerable<UserPositionResource>>(locationHistory);
            return resources;
        }

        //4.	Return the current location for all users
        [HttpGet]
        [Route("users")]
        public async Task<IEnumerable<UserPositionResource>> GetLocationForAllUsers()
        {
            var userPosition = await _userLocationService.GetLocationForAllUsersAsync();
            var resources = _mapper.Map<IEnumerable<UserPosition>, IEnumerable<UserPositionResource>>(userPosition);
            return resources;
        }


        ////5.   Return the current location for all users within a specified area
        //[HttpGet]
        //public async Task<LocationHistoryResource> GetLocationForAllUsersWithinArea(string area)
        //{
        //    var locationHistory = await _userLocationService.GetLocationForAllUsersWithinAnAreaAsync(area);
        //    var resources = _mapper.Map<LocationHistory, LocationHistoryResource>(locationHistory);
        //    return resources;
        //}




        //[HttpPost]
        //[ProducesResponseType(typeof(bool), 201)]
        //[ProducesResponseType(typeof(ErrorResource), 400)]
        //public async Task<bool> PostAsync([FromBody] BasketProduct bProduct)
        //{
        //    return await _basketProductService.AddAsync(bProduct);
        //}



    }
}
