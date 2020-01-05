using SmartHouse.DAL.EF;
using SmartHouse.DAL.Entities;
using SmartHouse.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SmartHouse.DAL.Repositories
{
    public class SensorRepository : IRepository<Sensor>
    {
        private ModelContext db;
        public int Count { get; set; } = 0;

        public SensorRepository(ModelContext context)
        {
            this.db = context;
        }

        public IEnumerable<Sensor> GetAll()
        {
            return db.Sensors;
        }

        public Sensor Get(int id)
        {
            return db.Sensors.Find(id);
        }

        public void Create(Sensor sensor)
        {
            db.Sensors.Add(sensor);
            Count++;
        }

        public void Update(Sensor sensor)
        {
            db.Entry(sensor).State = System.Data.Entity.EntityState.Modified;
        }

        public IEnumerable<Sensor> Find(Func<Sensor, Boolean> predicate)
        {
            return db.Sensors.Where(predicate).ToList();
        }

        public void Delete(int id)
        {
            Sensor sensor = db.Sensors.Find(id);
            if (sensor != null)
            {
                db.Sensors.Remove(sensor);
                Count--;
            }
        }

        public int GetCount()
        {
            return Count;
        }
    }
}
