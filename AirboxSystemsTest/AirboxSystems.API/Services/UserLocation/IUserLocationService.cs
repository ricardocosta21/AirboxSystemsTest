using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using AirboxSystems.API.Domain.Models;
using System.Collections.Generic;

namespace AirboxSystems.API.Services.UserLocation
{
    public interface IUserLocationService
    {
        //1.	Receive a location update for a user
        public bool AddUserLocation(UserPosition userPosition);

        //2.	Return the current location for a specified user
        public Task<UserPosition> GetCurrentLocationForUserAsync(int userId);

        //3.	Return the location history for a specified user
        public Task<IEnumerable<UserPosition>> GetLocationHistoryAsync(int userId);

        //4.	Return the current location for all users
        public Task<IEnumerable<UserPosition>> GetLocationForAllUsersAsync();

        //5.   Return the current location for all users within a specified area
        public Task<IEnumerable<UserPosition>> GetLocationForAllUsersWithinAnAreaAsync();
    }
}
