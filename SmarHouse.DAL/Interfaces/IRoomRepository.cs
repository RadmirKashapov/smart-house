using SmartHouse.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace SmartHouse.DAL.Interfaces
{
    public interface IRoomRepository<T> : IRepository<T> where T : class
    {
        public IEnumerable<Room> GetSelectedRooms(int houseId);
    }
}
