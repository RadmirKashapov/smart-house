using System;
using System.Collections.Generic;
using System.Text;
using SmarHouse.DAL.Entities;

namespace SmarHouse.DAL.Interfaces
{
    interface IUnitOfWork : IDisposable
    {
        IRepository<House> Homes { get; }
        IRepository<Room> Rooms { get; }
        IRepository<Sensor> Sensors { get; }
        void Save();
    }
}
