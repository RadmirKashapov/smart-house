using AutoMapper;
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

        public IEnumerable<RoomDTO> ShowRoomsInHouse(HouseDTO house)
        {
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<Room, RoomDTO>()).CreateMapper();
            var selectedRooms = from t in Database.Rooms.GetAll()
                                where t.HouseId == house.Id
                                select t;

            return mapper.Map<IEnumerable<Room>, List<RoomDTO>>(selectedRooms);
        }

        public void EnterValueOfTemperature(int? data, int houseId = 0, int roomId = 0)
        {
            if (data == null) 
                throw new ValidationException("No data", "");

            var count = 0;

            foreach(Sensor sensor in Database.Sensors.GetAll()) 
            {
                count++;
            }

            Database.Sensors.Create(new Sensor { Id = ++count, HouseId = houseId, RoomId = roomId, Date = DateTime.Now, Data = data.Value });
            Database.Save();
        }

        public void CalculateAverage(int? houseId, int? roomId, int duration)
        {
            
        }
    }
}
