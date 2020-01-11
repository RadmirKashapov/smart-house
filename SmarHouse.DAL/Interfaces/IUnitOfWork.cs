using SmartHouse.DAL.Entities;
using System;

namespace SmartHouse.DAL.Interfaces
{
    /// <summary>
    /// Интерфейс паттерна Unit Of Work, необходимый для упрощения подключения к БД
    /// </summary>
    public interface IUnitOfWork : IDisposable
    {
        IHouseRepository<House> Houses { get; }
        IRoomRepository<Room> Rooms { get; }
        ISensorRepository<Sensor> Sensors { get; }
        IRecordRepository<Record> Records { get; }
        void Save();
    }
}
