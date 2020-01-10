using System;
using System.Collections.Generic;
using System.Text;

namespace SmartHouse.BLL.DTO
{
    /// <summary>
    /// Data Transfer Object - специальная модель для передачи данных.
    /// Содержит только те данные, которые мы собираемся передать на уровень представления или, наоборот, получить с этого уровня
    /// </summary>
    public class HouseDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
