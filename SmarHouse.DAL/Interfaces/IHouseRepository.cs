using SmartHouse.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace SmartHouse.DAL.Interfaces
{
    public interface IHouseRepository<T> : IRepository<House> where T : class
    {
    }
}
