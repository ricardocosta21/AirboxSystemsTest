using System;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Entities;

namespace AirboxSystems.API.Domain.Models
{
    [BsonIgnoreExtraElements]
    public class UserPosition
    {
        public int UserId { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public Coordinates2D Location { get; set; }
        //[BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime TimeStamp { get; set; }
        public double DistanceMeters { get; set; }
    }
}
