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
        IEnumerable<RoomDTO> ShowRoomsInHouse(HouseDTO house);
        void EnterValueOfTemperature(int? data, int houseId = 0, int roomId = 0);
        double CalculateAverage(int? houseId, int? roomId, int duration);
    }
}
