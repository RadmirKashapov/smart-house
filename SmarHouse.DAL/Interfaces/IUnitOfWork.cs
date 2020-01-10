using SmartHouse.DAL.Entities;
using System;

namespace SmartHouse.DAL.Interfaces
{
    /// <summary>
    /// Интерфейс паттерна Unit Of Work, необходимый для упрощения подключения к БД
    /// </summary>
    public interface IUnitOfWork : IDisposable
    {
        IRepository<House> Houses { get; }
        IRepository<Room> Rooms { get; }
        IRepository<Sensor> Sensors { get; }
        IRepository<Record> Records { get; }
        void Save();
    }
}
