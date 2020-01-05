using SmartHouse.DAL.Entities;
using SmartHouse.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SmartHouse.BLL.BusinessModels
{
    public class Average
    {
        public int houseId { get; set; }
        public int roomId { get; set; }
        public int duration { get; set; }

        private IUnitOfWork Database;

        public Average(IUnitOfWork Database, int houseId, int roomId, int duration)
        {
            this.houseId = houseId;
            this.roomId = roomId;
            this.duration = duration;
            this.Database = Database;
        }

        public double CalculateAverage()
        {
            IEnumerable<int> selectedSensors;

            switch (duration)
            {
                case 0:
                    selectedSensors = from t in Database.Sensors.GetAll()
                                      where t.HouseId == houseId && t.RoomId == roomId && t.Date.Day == t.Date.Day - 1
                                      select t.Data;
                    return (double)selectedSensors.Average();
                case 1:
                    selectedSensors = from t in Database.Sensors.GetAll()
                                      where t.HouseId == houseId && t.RoomId == roomId && t.Date.Day == t.Date.Month - 1
                                      select t.Data;
                    return (double)selectedSensors.Average();
                default:
                    selectedSensors = from t in Database.Sensors.GetAll()
                                      where t.HouseId == houseId && t.RoomId == roomId && t.Date.Day == t.Date.Year - 1
                                      select t.Data;
                    return (double)selectedSensors.Average();
            }
        }
         
    }
}
