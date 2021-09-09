using System.Collections.Generic;
using AirboxSystems.API.Domain.Models;

namespace AirboxSystems.API.Resources
{
    public class LocationHistoryResource
    {
        public int UserId { get; set; }
        public List<UserPosition> Location { get; set; }
    }
}
