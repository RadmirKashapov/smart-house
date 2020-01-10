using SmartHouse.BLL.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace SmartHouse.BLL.Interfaces
{
    public interface ISmartService
    {
        void AddItem(HouseDTO houseDTO);
        void AddItem(RoomDTO roomDTO, int houseId = 0);
        IEnumerable<HouseDTO> ShowHouses();
        IEnumerable<RoomDTO> ShowRoomsInHouse(int houseId);
        IEnumerable<RoomDTO> ShowRooms();
        IEnumerable<SensorDTO> ShowSensors();
        IEnumerable<RecordDTO> ShowRecords();
        void EnterValueOfTemperature(int? data, int houseId = 0, int roomId = 0);
        double CalculateAverage(int? houseId, int duration, int? roomId);
        void Dispose();

    }
}
