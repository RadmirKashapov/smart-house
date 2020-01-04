using System;
using System.Collections.Generic;
using System.Text;

namespace SmartHouse.BLL.DTO
{
    public class SensorDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int HouseId { get; set; }
        public int RoomId { get; set; }
        public DateTime? Date { get; set; }
        public int Data { get; set; }
    }
}
