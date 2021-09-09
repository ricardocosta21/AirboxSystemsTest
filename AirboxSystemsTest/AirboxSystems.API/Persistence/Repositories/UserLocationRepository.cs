
using System.Collections.Generic;
using System.Threading.Tasks;
using AirboxSystems.API.Domain.Models;
using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;
using MongoDB.Driver.GeoJsonObjectModel;
using MongoDB.Entities;

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
                .Set("TimeStamp", userPosition.TimeStamp)
                .Set("Location", userPosition.Location)
                .Set("DistanceMeters", userPosition.DistanceMeters);

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
            return await _userPosition.Find($"{{ UserId: {userId} }}").SingleAsync();
        }

        // 3. Return the location history for a specified user
        public async Task<IEnumerable<UserPosition>> GetLocationHistoryAsync(int userId)
        {
            return await _locationHistory.Find(x => x.UserId == userId)
                .SortByDescending(x => x.TimeStamp).ToListAsync();
        }

        // 4. Return the current location for all users
        public async Task<IEnumerable<UserPosition>> GetLocationForAllUsersAsync()
        {
            return await _userPosition.Find(f => true).ToListAsync();
        }


        //5.   Return the current location for all users within a specified area
        public async Task<IEnumerable<UserPosition>> GetLocationForAllUsersWithinAnAreaAsync()
        {

            //var point = GeoJson.Point(GeoJson.Geographic(-47.6666, 43.5555));
            //var locationQuery = new FilterDefinitionBuilder<UserPosition>().Near(tag => tag.Location.Coordinates, point, 200000);
            //return await _userPosition.Find(locationQuery).ToListAsync();


            //var gp = new GeoJsonPoint<GeoJson2DGeographicCoordinates>(new GeoJson2DGeographicCoordinates(-47.6666, 43.5555));
            //var query = Builders<UserPosition>.Filter.Near("Location", gp, 200000);
            //return await _userPosition.Find(query).ToListAsync();


            var longitude = -47.6666; //x
            var latitude = 43.5555; //y
            var point = new GeoJsonPoint<GeoJson2DGeographicCoordinates>(new GeoJson2DGeographicCoordinates(longitude, latitude));
            var filter = Builders<UserPosition>.Filter.NearSphere(doc => doc.Location.Coordinates, point, 200000);
            return await _userPosition.Find(filter).ToListAsync();


            //var filter = Builders<Cidade>.Filter.Near(x => x.Localizacao.Coordinates, pnt, distanceInMeters);

            //// This is the actual query execution
            //List<Cidade> cities = collection.Find(filter).ToList().Result;


        }

    }
}