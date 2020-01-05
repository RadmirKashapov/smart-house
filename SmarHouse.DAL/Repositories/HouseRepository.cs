using SmartHouse.DAL.EF;
using SmartHouse.DAL.Entities;
using SmartHouse.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SmartHouse.DAL.Repositories
{
    public class HouseRepository : IRepository<House>
    {
        private ModelContext db;
        public int Count { get; set; } = 0;

        public HouseRepository(ModelContext context)
        {
            this.db = context;
        }

        public IEnumerable<House> GetAll()
        {
            return db.Houses;
        }

        public House Get(int id)
        {
            return db.Houses.Find(id);
        }

        public void Create(House home)
        {
            db.Houses.Add(home);
            Count++;
        }

        public void Update(House home)
        {
            db.Entry(home).State = System.Data.Entity.EntityState.Modified;
        }

        public IEnumerable<House> Find(Func<House, Boolean> predicate)
        {
            return db.Houses.Where(predicate).ToList();
        }

        public void Delete(int id)
        {
            House home = db.Houses.Find(id);
            if (home != null)
            {
                db.Houses.Remove(home);
                Count--;
            }
        }

        public int GetCount() 
        {
            return Count;
        }
    }
}
