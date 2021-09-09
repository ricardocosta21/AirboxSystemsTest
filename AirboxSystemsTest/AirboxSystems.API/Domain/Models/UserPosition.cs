using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace AirboxSystems.API.Domain.Models
{
    public class UserPosition
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public int UserId { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        //[BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime TimeStamp { get; set; }
    }
}
