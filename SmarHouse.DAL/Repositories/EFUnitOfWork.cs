using SmartHouse.DAL.EF;
using SmartHouse.DAL.Interfaces;
using SmartHouse.DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

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
            if(houseRepository == null)
                houseRepository = new HouseRepository(db);
            return houseRepository;
        }
    }
}
