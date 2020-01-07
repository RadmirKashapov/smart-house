using AutoMapper;
using SmartHouse.BLL.BusinessModels;
using SmartHouse.BLL.DTO;
using SmartHouse.BLL.Infrastructure;
using SmartHouse.BLL.Interfaces;
using SmartHouse.DAL.Entities;
using SmartHouse.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SmartHouse.BLL.Services
{
    public class SmartService : ISmartService
    {
        IUnitOfWork Database { get; set; }

        public SmartService(IUnitOfWork uow)
        {
            Database = uow;
        }
        public void AddItem(HouseDTO houseDTO)
        {
            House house = new House
            {
                Name = houseDTO.Name
            };

            Database.Houses.Create(house);
            Database.Save();
        }

        public void AddItem(RoomDTO roomDTO, int houseId = 0)
        {
            Room room = new Room
            {
                Name = roomDTO.Name,
                HouseId = houseId
            };

            Database.Rooms.Create(room);
            Database.Save();
        }

        public IEnumerable<HouseDTO> ShowHouses()
        {
            // применяем автомаппер для проекции одной коллекции на другую
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<House, HouseDTO>()).CreateMapper();
            return mapper.Map<IEnumerable<House>, List<HouseDTO>>(Database.Houses.GetAll());
        }

        public IEnumerable<RoomDTO> ShowRoomsInHouse(int houseId)
        {
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<Room, RoomDTO>()).CreateMapper();
            var selectedRooms = from t in Database.Rooms.GetAll()
                                where t.HouseId == houseId
                                select t;

            return mapper.Map<IEnumerable<Room>, List<RoomDTO>>(selectedRooms);
        }

        public void EnterValueOfTemperature(int? data, int houseId = 0, int roomId = 0)
        {
            if (data == null) 
                throw new ValidationException("No data", "");

            var sensor = from t in Database.Sensors.GetAll()
                         where t.HouseId == houseId && t.RoomId == roomId
                         select t;

            Database.Records.Create(new Record { Date = DateTime.Now, Data = data.Value, SensorId = sensor.First().Id });
            Database.Save();
        }

        public double CalculateAverage(int? houseId, int duration, int? roomId = 0)
        {
          //  0 - Day
          //  1- month
          //  2- year
            if (houseId == null)
                throw new ValidationException("Error: Incorrect house id", "");

            if (roomId == null)
                throw new ValidationException("Error: Incorrect room id", "");

            var average = new Average(Database, houseId.Value, roomId.Value, duration);

            return average.CalculateAverage(); 
        }

        public void Dispose()
        {
            Database.Dispose();
        }
    }
}
