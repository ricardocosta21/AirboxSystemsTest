using System;
using Microsoft.EntityFrameworkCore;
using AirboxSystems.API.Domain.Models;

namespace AirboxSystems.API.Persistence.Contexts
{
    public class AppDbContext : DbContext
    {
        public DbSet<UserPosition> UserPosition { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);


//            var users = new[]
//{
//    new User { Name = "Danial", Email = "danial@example.com" },
//    new User { Name = "Bahar", Email = "bahar@example.com" },
//    new User { Name = "Shadi", Email = "shadi@exmaple.com" }
//};
//            collection.InsertMany(users);





            //modelBuilder.Entity<UserPosition>().ToTable("UserPosition");
            //modelBuilder.Entity<UserPosition>().Property(p => p.Id).ValueGeneratedOnAdd();
            //modelBuilder.Entity<UserPosition>().HasAlternateKey(p => p.Id);
            //modelBuilder.Entity<UserPosition>().Property(p => p.UserId).IsRequired();
            //modelBuilder.Entity<UserPosition>().Property(p => p.TimeStamp).IsRequired();

            //modelBuilder.Entity<UserPosition>().HasData
            //(
            //    new UserPosition { Id = "1", UserId = 1, Latitude = -73.856077, Longitude = 40.848447, TimeStamp = DateTime.Now },
            //    new UserPosition { Id = "2", UserId = 1, Latitude = -73.856077, Longitude = 40.848447, TimeStamp = DateTime.Now },
            //    new UserPosition { Id = "3", UserId = 1, Latitude = -73.856077, Longitude = 40.848447, TimeStamp = DateTime.Now },
            //    new UserPosition { Id = "4", UserId = 2, Latitude = -73.856077, Longitude = 40.848447, TimeStamp = DateTime.Now },
            //    new UserPosition { Id = "5", UserId = 2, Latitude = -73.856077, Longitude = 40.848447, TimeStamp = DateTime.Now },
            //    new UserPosition { Id = "6", UserId = 2, Latitude = -73.856077, Longitude = 40.848447, TimeStamp = DateTime.Now },
            //    new UserPosition { Id = 7, UserId = 3, Latitude = -73.856077, Longitude = 40.848447, TimeStamp = DateTime.Now },
            //    new UserPosition { Id = 8, UserId = 3, Latitude = -73.856077, Longitude = 40.848447, TimeStamp = DateTime.Now },
            //    new UserPosition { Id = 9, UserId = 3, Latitude = -73.856077, Longitude = 40.848447, TimeStamp = DateTime.Now }
            //);

            // modelBuilder.Entity<LocationHistory>().ToTable("LocationHistory");
            // modelBuilder.Entity<LocationHistory>().HasKey(p => p.UserId);
            // modelBuilder.Entity<LocationHistory>().Property(p => p.UserId).IsRequired().ValueGeneratedOnAdd();
            // modelBuilder.Entity<LocationHistory>().Property(p => p.Location).IsRequired();

            // modelBuilder.Entity<LocationHistory>().HasData
            //(
            //    new UserPosition { UserId = 222, Location = new Coordinates(232, 222), TimeStamp = DateTime.Now }, // Id set manually due to in-memory provider
            //    new UserPosition { UserId = 102, Location = new Coordinates(232, 3443), TimeStamp = DateTime.Now }
            //);
        }
    }
}