using SmartHouse.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace SmartHouse.DAL.Interfaces
{
    public interface IRecordRepository<T> : IRepository<T> where T : class
    {
        public IEnumerable<int> GetSelectedRecordsPerDay(int sensorId);
        public IEnumerable<int> GetSelectedRecordsPerMonth(int sensorId);
        public IEnumerable<int> GetSelectedRecordsPerYear(int sensorId);
    }
}
