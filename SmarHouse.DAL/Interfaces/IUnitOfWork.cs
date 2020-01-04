using SmartHouse.DAL.Entities;
using System;

namespace SmartHouse.DAL.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<House> Houses { get; }
        IRepository<Room> Rooms { get; }
        IRepository<Sensor> Sensors { get; }
        void Save();
    }
}
