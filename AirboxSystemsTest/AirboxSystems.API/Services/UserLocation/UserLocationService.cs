using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using AirboxSystems.API.Domain.Models;
using System.Collections.Generic;
using AirboxSystems.API.Persistence.Repositories;

namespace AirboxSystems.API.Services.UserLocation
{
    public class UserLocationService : IUserLocationService
    {

        private readonly IUserLocationRepository _userLocationRepository;

        public UserLocationService(IUserLocationRepository userLocationRepository)
        {
            _userLocationRepository = userLocationRepository;
        }


        //1.	Receive a location update for a user
        public bool AddUserLocation(UserPosition userPosition)
        {
            _userLocationRepository.AddUserLocation(userPosition);

            //TODO add logic to check if it went through
            return true;
        }

        //2.	Return the current location for a specified user
        public async Task<UserPosition> GetCurrentLocationForUserAsync(int userId)
        {
            return await _userLocationRepository.GetCurrentLocationForUserAsync(userId);
        }

        //3.	Return the location history for a specified user
        public async Task<IEnumerable<UserPosition>> GetLocationHistoryAsync(int userId)
        {
            return await _userLocationRepository.GetLocationHistoryAsync(userId);
        }

        //4.	Return the current location for all users
        public async Task<IEnumerable<UserPosition>> GetLocationForAllUsersAsync()
        {
            return await _userLocationRepository.GetLocationForAllUsersAsync();
        }




        //5.   Return the current location for all users within a specified area
        public async Task<IEnumerable<UserPosition>> GetLocationForAllUsersWithinAnAreaAsync()
        {
            return await _userLocationRepository.GetLocationForAllUsersWithinAnAreaAsync();
        }

    }
}
