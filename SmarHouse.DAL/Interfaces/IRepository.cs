using System;
using System.Collections.Generic;
using System.Text;

namespace SmartHouse.DAL.Interfaces
{
    /// <summary>
    /// Интерфейс репозиториев IRepository. Определяет контракт для классов репозиториев
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IRepository<T> where T: class
    {
        IEnumerable<T> GetAll();
        T Get(int id);
        IEnumerable<T> Find(Func<T, Boolean> predicate);
        void Create(T item);
        void Update(T item);
        void Delete(int id);
        public int GetCount();
    }
}
