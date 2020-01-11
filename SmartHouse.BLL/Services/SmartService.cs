using AutoMapper;
using SmartHouse.BLL.BusinessModels;
using SmartHouse.BLL.DTO;
using SmartHouse.BLL.Infrastructure;
using SmartHouse.BLL.Interfaces;
using SmartHouse.DAL.Entities;
using SmartHouse.DAL.Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SmartHouse.BLL.Services
{
    /// <summary>
    /// Класс сервиса, реализующий ISmartService
    /// </summary>
    public class SmartService : ISmartService
    {
        IUnitOfWork Database { get; set; } // Необходим для взаимодействия с БД

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

            Room room = new Room
            {
                Name = $"Main room of house '{houseDTO.Name}'",
                HouseId = house.Id
            };

            Sensor sensor = new Sensor
            {
                HouseId = house.Id,
                RoomId = Database.Rooms.GetCount() + 1
            };

            Database.Houses.Create(house);
            Database.Rooms.Create(room);
            Database.Sensors.Create(sensor);
            Database.Save();
        }

        public void AddItem(RoomDTO roomDTO, int houseId = 0)
        {
            Room room = new Room
            {
                Name = roomDTO.Name,
                HouseId = houseId
            };

            Sensor sensor = new Sensor
            {
                HouseId = houseId,
                RoomId = Database.Rooms.GetCount() + 1
            };

            Database.Rooms.Create(room);
            Database.Sensors.Create(sensor);
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
            var selectedRooms = Database.Rooms.GetSelectedRooms(houseId);

            return mapper.Map<IEnumerable<Room>, List<RoomDTO>>(selectedRooms);
        }

        public void EnterValueOfTemperature(int? data, int houseId = 0, int roomId = 0)
        {
            if (data == null)
                throw new ValidationException("No data", "");

            var sensor = Database.Sensors.GetSelectedSensors(houseId, roomId);

            if (sensor.Count() == 0)
            {
                Sensor sensorNew = new Sensor
                {
                    HouseId = houseId,
                    RoomId = roomId
                };
                Database.Sensors.Create(sensorNew);
                Database.Records.Create(new Record { Data = data.Value, SensorId = Database.Sensors.GetCount() + 1 });
                Database.Save();
                return;
            }
            Database.Records.Create(new Record { Data = data.Value, SensorId = sensor.First().Id });
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

        public IEnumerable<RoomDTO> ShowRooms()
        {
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<Room, RoomDTO>()).CreateMapper();
            return mapper.Map<IEnumerable<Room>, List<RoomDTO>>(Database.Rooms.GetAll());
        }
        public IEnumerable<SensorDTO> ShowSensors()
        {
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<Sensor, SensorDTO>()).CreateMapper();
            return mapper.Map<IEnumerable<Sensor>, List<SensorDTO>>(Database.Sensors.GetAll());
        }

        public IEnumerable<RecordDTO> ShowRecords()
        {
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<Record, RecordDTO>()).CreateMapper();
            return mapper.Map<IEnumerable<Record>, List<RecordDTO>>(Database.Records.GetAll());
        }

        public void Update(int id, string str, string name = "Undefined", int? data = null, DateTime? dateTime = null)
        {
            switch (str)
            {
                case "House":
                    House house = Database.Houses.Get(id);
                    if (name != "Undefined")
                    {
                        house.Name = name;
                    }
                    Database.Houses.Update(house);
                    Database.Save();
                    break;
                case "Room":
                    Room room = Database.Rooms.Get(id);
                    if (name != "Undefined")
                    {
                        room.Name = name;
                    }
                    Database.Rooms.Update(room);
                    Database.Save();
                    break;
                case "Sensor":
                    Sensor sensor = Database.Sensors.Get(id);
                    Database.Sensors.Update(sensor);
                    Database.Save();
                    break;
                case "Record":
                    Record record = Database.Records.Get(id);
                    if (dateTime != null)
                    {
                        record.Date = dateTime.Value;
                    }
                    if (data != null)
                    {
                        record.Data = data.Value;
                    }
                    Database.Records.Update(record);
                    Database.Save();
                    break;
            }
        }

        public void Delete(int id, string str)
        {
            switch (str)
            {
                case "House":
                    Database.Houses.Delete(id);
                    Database.Save();
                    break;
                case "Room":
                    Database.Rooms.Delete(id);
                    Database.Save();
                    break;
                case "Sensor":
                    Database.Sensors.Delete(id);
                    Database.Save();
                    break;
                case "Record":
                    Database.Records.Delete(id);
                    Database.Save();
                    break;
            }
        }
    }
}
