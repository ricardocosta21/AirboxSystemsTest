using System.Collections.Generic;
using System.Threading.Tasks;
using AirboxSystems.API.Domain.Models;

namespace AirboxSystems.API.Persistence.Repositories
{
    public interface IUserLocationRepository
    {
        // 1. 
        void AddUserLocation(UserPosition userPosition);
        // 2.
        Task<UserPosition> GetCurrentLocationForUserAsync(int userId);
        // 3.
        Task<IEnumerable<UserPosition>>  GetLocationHistoryAsync(int userId);
        // 4.
        Task<IEnumerable<UserPosition>> GetLocationForAllUsersAsync();
        //    // 5.
        //    Task<IEnumerable<LocationHistory>> GetLocationForAllUsersWithinAnAreaAsync(string area);
    }
}
