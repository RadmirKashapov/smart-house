using System;
using System.Collections.Generic;
using System.Text;

namespace SmartHouse.PL.Models
{
    public class SensorViewModel
    {
        public int Id { get; set; }
        public string HouseId { get; set; }
        public string RoomId { get; set; }
    }
}
