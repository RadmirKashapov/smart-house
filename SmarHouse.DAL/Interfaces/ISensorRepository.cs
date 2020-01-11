using SmartHouse.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace SmartHouse.DAL.Interfaces
{
    public interface ISensorRepository<T> : IRepository<T> where T : class
    {
        public IEnumerable<Sensor> GetSelectedSensors(int houseId, int roomId);
    }
}
