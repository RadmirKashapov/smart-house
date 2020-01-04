﻿using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using System.Configuration;
using SmarHouse.DAL.Entities;
using System.Data.Entity;

namespace SmarHouse.DAL.EF
{
    public class ModelContext : DbContext
    {
        public DbSet<House> Houses { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<Sensor> Sensors { get; set; }

        static ModelContext()
        {
            Database.SetInitializer<ModelContext>(new SensorDbInitializer());
        }
        public ModelContext(string connectionString)
            : base(connectionString)
        {
        }
    }

    public class SensorDbInitializer : DropCreateDatabaseIfModelChanges<ModelContext>
    {
        protected override void Seed(ModelContext db)
        {
            db.Houses.Add(new House { Name = "House 1" });
            db.Rooms.Add(new Room {  Name = "Room 1" });
            db.Sensors.Add(new Sensor { Name = "Sensor 1", Date = DateTime.Now });
            db.SaveChanges();
        }
    }
}
