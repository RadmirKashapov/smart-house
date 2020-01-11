using SmartHouse.DAL.EF;
using SmartHouse.DAL.Entities;
using SmartHouse.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SmartRecord.DAL.Repositories
{
    public class RecordRepository : IRecordRepository<Record>
    {
        private ModelContext db;
        public int Count { get; set; } = 0;

        public RecordRepository(ModelContext context)
        {
            this.db = context;
        }

        public IEnumerable<Record> GetAll()
        {
            return db.Records;
        }

        public Record Get(int id)
        {
            return db.Records.Find(id);
        }

        public void Create(Record home)
        {
            db.Records.Add(home);
            Count++;
        }

        public void Update(Record home)
        {
            db.Entry(home).State = System.Data.Entity.EntityState.Modified;
        }

        public IEnumerable<Record> Find(Func<Record, Boolean> predicate)
        {
            return db.Records.Where(predicate).ToList();
        }

        public void Delete(int id)
        {
            Record home = db.Records.Find(id);
            if (home != null)
            {
                db.Records.Remove(home);
                Count--;
            }
        }

        public int GetCount()
        {
            return Count;
        }

        public IEnumerable<int> GetSelectedRecordsPerDay(int sensorId)
        {
            var x  = from t in GetAll()
                     where t.SensorId == sensorId && t.Date.Day >= DateTime.Today.Day - 1
                     select t.Data;

            return x;
        }

        public IEnumerable<int> GetSelectedRecordsPerMonth(int sensorId)
        {
            var x = from t in GetAll()
                    where t.SensorId == sensorId && t.Date.Month >= DateTime.Today.Month - 1
                    select t.Data;

            return x;
        }

        public IEnumerable<int> GetSelectedRecordsPerYear(int sensorId)
        {
            var x = from t in GetAll()
                    where t.SensorId == sensorId && t.Date.Year >= DateTime.Today.Year - 1
                    select t.Data;

            return x;
        }
    }
}

