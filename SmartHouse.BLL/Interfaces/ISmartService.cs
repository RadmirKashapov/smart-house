using SmartHouse.BLL.DTO;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace SmartHouse.BLL.Interfaces
{
    /// <summary>
    /// Интерфейс определяет базовые методы для взаимодействия с уровнями представления и данных
    /// </summary>
    public interface ISmartService
    {
        /// <summary>
        /// Добавляет объект House в БД. Дополнительно добавляются объекты Room, Sensor
        /// </summary>
        /// <param name="houseDTO"></param>

        void AddItem(HouseDTO houseDTO);

        /// <summary>
        /// Генерация объекта Room и вспомогательного объекта Sensor
        /// </summary>
        /// <param name="roomDTO"></param>
        /// <param name="houseId"></param>
        void AddItem(RoomDTO roomDTO, int houseId = 0);

        /// <summary>
        /// Показывает все имеющиеся дома в БД
        /// </summary>
        /// <returns>Возвращает список объектов HouseDTO на уровень представления</returns>
        IEnumerable<HouseDTO> ShowHouses();

        /// <summary>
        /// Показывает все имеющиеся комнаты в доме
        /// </summary>
        /// <param name="houseId"></param>
        /// <returns>Возвращает список объектов RoomDTO на уровень представления</returns>
        IEnumerable<RoomDTO> ShowRoomsInHouse(int houseId);

        /// <summary>
        /// Показывает все имеющиеся комнаты в БД
        /// </summary>
        /// <returns>Возвращает список объектов RoomDTO на уровень представления</returns>
        IEnumerable<RoomDTO> ShowRooms();

        /// <summary>
        /// Показывает все имеющиеся датчики в БД
        /// </summary>
        /// <returns>Возвращает список объектов SensorDTO на уровень представления</returns>
        IEnumerable<SensorDTO> ShowSensors();
        /// <summary>
        /// Показывает все имеющиеся данные с датчиков в БД
        /// </summary>
        /// <returns>Возвращает список объектов RecordDTO на уровень представления</returns>
        IEnumerable<RecordDTO> ShowRecords();
        /// <summary>
        /// Обновляет записи в БД
        /// </summary>
        /// <param name="id"></param>
        /// <param name="str"></param>
        /// <param name="name"></param>
        /// <param name="data"></param>
        /// <param name="dateTime"></param>
        void Update(int id, string str, string name = "Undefined", int? data = null, DateTime? dateTime = null);
        
        /// <summary>
        /// Удаляет записи из БД
        /// </summary>
        /// <param name="id"></param>
        /// <param name="str"></param>
        void Delete(int id, string str);

        /// <summary>
        /// Ввод значения датчика в доме/комнате
        /// </summary>
        /// <param name="data"></param>
        /// <param name="houseId"></param>
        /// <param name="roomId"></param>
        void EnterValueOfTemperature(int? data, int houseId = 0, int roomId = 0);

        /// <summary>
        /// Подсчет среднего значения температуры в доме/комнате за день/месяц/год
        /// </summary>
        /// <param name="houseId"></param>
        /// <param name="duration"></param>
        /// <param name="roomId"></param>
        /// <returns>Возвращает значение типа double и NaN в случае невзможности подсчета</returns>
        double CalculateAverage(int? houseId, int duration, int? roomId);
        void Dispose();

    }
}
