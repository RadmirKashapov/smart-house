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
            IEnumerable<Sensor> selectedSensors;
            IEnumerable<int> selectedRecords;
            DateTime dateTime = DateTime.Now;

            selectedSensors = from t in Database.Sensors.GetAll()
                              where t.HouseId == houseId && t.RoomId == roomId
                              select t;

            if (selectedSensors.Count() == 0)
            {
                return double.NaN;
            }

            switch (duration)
            {
                case 0:
                    selectedRecords = from t in Database.Records.GetAll()
                                      where t.SensorId == selectedSensors.First().Id && t.Date.Day == DateTime.Now.Day - 1
                                      select t.Data;

                    if (selectedRecords.Count() == 0)
                    {
                        return double.NaN;
                    }

                    return (double)selectedRecords.Average();
                case 1:
                    selectedRecords = from t in Database.Records.GetAll()
                                      where t.SensorId == selectedSensors.First().Id && t.Date.Month == DateTime.Now.Month - 1
                                      select t.Data;
                    if (selectedRecords.Count() == 0)
                    {
                        return double.NaN;
                    }

                    return (double)selectedRecords.Average();
                default:
                    selectedRecords = from t in Database.Records.GetAll()
                                      where t.SensorId == selectedSensors.First().Id && t.Date.Year == DateTime.Now.Year - 1
                                      select t.Data;

                    if (selectedRecords.Count() == 0)
                    {
                        return double.NaN;
                    }

                    return (double)selectedRecords.Average();
            }
        }
         
    }
}
