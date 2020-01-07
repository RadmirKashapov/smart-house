using System;
using System.Collections.Generic;
using System.Text;

namespace SmartHouse.BLL.DTO
{
    public class RecordDTO
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public int Data { get; set; }
        public int SensorId { get; set; }
    }
}
