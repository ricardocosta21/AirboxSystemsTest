
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AirboxSystems.API.Domain.Models;
using Microsoft.EntityFrameworkCore;
using MongoDB.Bson;
using MongoDB.Driver;

namespace AirboxSystems.API.Persistence.Repositories
{
    public class UserLocationRepository : IUserLocationRepository
    {
        private readonly IMongoCollection<UserPosition> _userPosition;
        private readonly IMongoCollection<UserPosition> _locationHistory;

        public UserLocationRepository(IAirboxDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _userPosition = database.GetCollection<UserPosition>(settings.AirboxUserPositionCollection);
            _locationHistory = database.GetCollection<UserPosition>(settings.AirboxLocationHistoryCollection);
        }

        // 1. Receive a location update for a user
        public void AddUserLocation(UserPosition userPosition)
        {
            var filter = Builders<UserPosition>.Filter.Eq("UserId", userPosition.UserId);
            var update = Builders<UserPosition>.Update
                .Set("Latitude", userPosition.Latitude)
                .Set("Longitude", userPosition.Longitude)
                .Set("TimeStamp", userPosition.TimeStamp);

            var updateOptions = new UpdateOptions()
            {
                IsUpsert = true
            };

            _userPosition.UpdateOne(filter, update, updateOptions);

            _locationHistory.InsertOne(userPosition);
        }

        // 2. Return the current location for a specified user
        public async Task<UserPosition> GetCurrentLocationForUserAsync(int userId)
        {
            //return await _userPosition.Find(x => x.UserId == userId)
            //    .SortByDescending(x => x.TimeStamp).FirstOrDefaultAsync();

            return await _userPosition.Find($"{{ UserId: {userId} }}").SingleAsync();


            //return await _context.UserPosition.Where(x => x.UserId == userId)
            //    .OrderByDescending(x => x.TimeStamp).FirstOrDefaultAsync();
        }

        // 3. Return the location history for a specified user
        public async Task<IEnumerable<UserPosition>> GetLocationHistoryAsync(int userId)
        {
            return await _locationHistory.Find(x => x.UserId == userId)
                .SortByDescending(x => x.TimeStamp).ToListAsync();

            //Mongodb
            //var filter = Builders<User>.Filter.Eq(x => x.UserId, userId)
            //var documents = await collection.Find(new BsonDocument()).ToListAsync();


            //return await _context.UserPosition.Where(x => x.UserId == userId).
            //    OrderByDescending(x => x.TimeStamp).ToListAsync();
        }

        // 4. Return the current location for all users
        public async Task<IEnumerable<UserPosition>> GetLocationForAllUsersAsync()
        {
            //List<UserPosition> userLocations = new List<UserPosition>();
            //var filter = new BsonDocument();
            //var userIdCursor = await _userPosition.DistinctAsync<int>("UserId", filter);

            //await userIdCursor.ForEachAsync(userId =>
            //{
            //    var entity = _userPosition.Find(x => x.UserId == userId).FirstOrDefault();
            //    userLocations.Add(entity);
            //});
            //return userLocations;

            return await _userPosition.Find(f => true).ToListAsync();
        }


        ////5.   Return the current location for all users within a specified area
        //public async Task<IEnumerable<LocationHistory>> GetLocationForAllUsersWithinAnAreaAsync(string area)
        //{
        //    //return null;
        //}

    }
}