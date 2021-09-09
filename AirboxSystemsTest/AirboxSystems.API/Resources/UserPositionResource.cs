using System;

namespace AirboxSystems.API.Resources
{
    public class UserPositionResource
    {
        public string Id { get; set; }
        public int UserId { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public DateTime TimeStamp { get; set; }
    }
}
