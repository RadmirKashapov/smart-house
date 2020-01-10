using System;
using SmartHouse.DAL.Entities;
using System.Data.Entity;

namespace SmartHouse.DAL.EF
{
    public class ModelContext : DbContext
    {
        public DbSet<House> Houses { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<Sensor> Sensors { get; set; }
        public DbSet<Record> Records { get; set; }

        static ModelContext()
        {
            Database.SetInitializer<ModelContext>(new SensorDbInitializer());
        }
        public ModelContext(string connectionString)
            : base(connectionString)
        {
        }
    }

    //public class SensorDbInitializer : DropCreateDatabaseIfModelChanges<ModelContext>
    public class SensorDbInitializer : DropCreateDatabaseAlways<ModelContext>
    {
        protected override void Seed(ModelContext db)
        {
            db.Houses.Add(new House { Name = "House 1" });
            db.Rooms.Add(new Room {  Name = "Room 1" });
            db.Sensors.Add(new Sensor { HouseId = 1 , RoomId = 0});
            db.Records.Add(new Record { Data = 13, Date = DateTime.Now.AddDays(-1), SensorId = 1 });
            db.Records.Add(new Record { Data = 19, Date = DateTime.Now.AddDays(-1).AddHours(-1), SensorId = 1 });
            db.Records.Add(new Record { Data = 13, Date = DateTime.Now.AddDays(-365), SensorId = 1 });
            db.Records.Add(new Record { Data = 19, Date = DateTime.Now.AddDays(-365).AddHours(-1), SensorId = 1 });
            db.Records.Add(new Record { Data = 13, Date = DateTime.Now.AddDays(-30), SensorId = 1 });
            db.Records.Add(new Record { Data = 19, Date = DateTime.Now.AddDays(-30).AddHours(-1), SensorId = 1 });
            db.SaveChanges();
        }
    }
}
