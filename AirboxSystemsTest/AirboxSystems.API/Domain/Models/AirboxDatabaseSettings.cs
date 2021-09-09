namespace AirboxSystems.API.Domain.Models
{
    public class AirboxDatabaseSettings : IAirboxDatabaseSettings
    {
        public string AirboxUserPositionCollection { get; set; }
        public string AirboxLocationHistoryCollection { get; set; }
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }

    public interface IAirboxDatabaseSettings
    {
        string AirboxUserPositionCollection { get; set; }
        string AirboxLocationHistoryCollection { get; set; }
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
    }
}