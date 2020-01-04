﻿using SmarHouse.DAL.EF;
using SmarHouse.DAL.Entities;
using SmarHouse.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SmarRoom.DAL.Repositories
{
    class RoomRepository : IRepository<Room>
    {
        private ModelContext db;

        public RoomRepository(ModelContext context)
        {
            this.db = context;
        }

        public IEnumerable<Room> GetAll()
        {
            return db.Rooms;
        }

        public Room Get(int id)
        {
            return db.Rooms.Find(id);
        }

        public void Create(Room room)
        {
            db.Rooms.Add(room);
        }

        public void Update(Room room)
        {
            db.Entry(room).State = System.Data.Entity.EntityState.Modified;
        }

        public IEnumerable<Room> Find(Func<Room, Boolean> predicate)
        {
            return db.Rooms.Where(predicate).ToList(); //linq
        }

        public void Delete(int id)
        {
            Room room = db.Rooms.Find(id);
            if (room != null)
                db.Rooms.Remove(room);
        }
    }
}
