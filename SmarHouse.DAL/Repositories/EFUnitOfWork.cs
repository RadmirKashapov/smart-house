using SmartHouse.DAL.EF;
using SmartHouse.DAL.Entities;
using SmartHouse.DAL.Interfaces;
using System;

namespace SmartHouse.DAL.Repositories
{
    public class EFUnitOfWork : IUnitOfWork
    {
        private ModelContext db;
        private HouseRepository houseRepository;
        private RoomRepository roomRepository;
        private SensorRepository sensorRepository;
        public EFUnitOfWork(string connectionString)
        {
            db = new ModelContext(connectionString);
        }

        public IRepository<House> Houses
        {
            get
            {
                if (houseRepository == null)
                    houseRepository = new HouseRepository(db);
                return houseRepository;
            }
        }

        public IRepository<Room> Rooms
        {
            get
            {
                if (roomRepository == null)
                    roomRepository = new RoomRepository(db);
                return roomRepository;
            }
        }

        public IRepository<Sensor> Sensors
        {
            get
            {
                if (sensorRepository == null)
                    sensorRepository = new SensorRepository(db);
                return sensorRepository;
            }
        }

        public void Save()
        {
            db.SaveChanges();
        }

        private bool disposed = false;

        public virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    db.Dispose();
                }
                this.disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
