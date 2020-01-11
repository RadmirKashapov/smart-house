using SmartHouse.DAL.EF;
using SmartHouse.DAL.Entities;
using SmartHouse.DAL.Interfaces;
using SmartRecord.DAL.Repositories;
using System;

namespace SmartHouse.DAL.Repositories
{
    /// <summary>
    /// Реализация интерфейса IUnitOfWork. Через данный класс происходит взаимодействие с БД
    /// </summary>
    public class EFUnitOfWork : IUnitOfWork
    {
        private ModelContext db;
        private HouseRepository houseRepository;
        private RoomRepository roomRepository;
        private SensorRepository sensorRepository;
        private RecordRepository recordRepository;

        /// <summary>
        /// Строка подключения к БД в конструкторе класса EFUnitOfWork передается в
        /// конструктор контекста данных
        /// </summary>
        /// <param name="connectionString"></param>
        public EFUnitOfWork(string connectionString)
        {
            db = new ModelContext(connectionString);
        }

        public IHouseRepository<House> Houses
        {
            get
            {
                if (houseRepository == null)
                    houseRepository = new HouseRepository(db);
                return houseRepository;
            }
        }

        public IRoomRepository<Room> Rooms
        {
            get
            {
                if (roomRepository == null)
                    roomRepository = new RoomRepository(db);
                return roomRepository;
            }
        }

        public ISensorRepository<Sensor> Sensors
        {
            get
            {
                if (sensorRepository == null)
                    sensorRepository = new SensorRepository(db);
                return sensorRepository;
            }
        }

        public IRecordRepository<Record> Records
        {
            get
            {
                if (recordRepository == null)
                    recordRepository = new RecordRepository(db);
                return recordRepository;
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
