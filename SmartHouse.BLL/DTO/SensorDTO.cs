using System;
using System.Collections.Generic;
using System.Text;

namespace SmartHouse.BLL.DTO
{
    public class SensorDTO
    {
        public int Id { get; set; }
        public string HouseId { get; set; }
        public string RoomId { get; set; }
    }
}
