using AutoMapper;
using Ninject;
using Ninject.Modules;
using SmartHouse.BLL.DTO;
using SmartHouse.BLL.Infrastructure;
using SmartHouse.BLL.Interfaces;
using SmartHouse.PL.Models;
using SmartHouse.PL.Util;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace SmartHouse.PL.Controllers
{
    public class SmartController 
    {
        ISmartService smartService;
        List<HouseViewModel> houses;

        public SmartController()
        {
            var connectionString = System.Configuration.ConfigurationManager.
                ConnectionStrings["ModuleContext"].ConnectionString;
            NinjectModule smartModule = new SmartModule();
            NinjectModule serviceModule = new ServiceModule(connectionString);
            var kernel = new StandardKernel(smartModule, serviceModule);
            var processor = kernel.Get<ISmartService>();

            smartService = processor;
        }

        public void Start()
        {
            displayMenu();
        }

        public void displayMenu()
        {
            Boolean flag = true;
            
            ArrayList Options = new ArrayList
            {
                "Add house",
                "Add room",
                "Enter value of temperature",
                "Calculate the average temperature",
                "Look and edit the data"
            };

            while (flag == true)
            {
                int number = ChooseOption("Exit", Options);
                string house;
                string room;

                switch (number)
                {
                    case 0:
                        flag = false;
                        break;
                    case 1:
                        Console.WriteLine("Enter name of house");
                        house = Console.ReadLine();
                        var houseObject = new HouseDTO
                        {
                            Name = house
                        };
                        try { 
                        smartService.AddItem(houseObject);
                        }
                        catch (ValidationException ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                        break;
                    case 2:
                        houses = GetHouseList();
                        number = ChooseOption("Back", houses);
                        if (number == 0) return;
                        int houseId = houses[number - 1].Id;

                        Console.WriteLine("Enter name of room");
                        room = Console.ReadLine();

                        var roomObject = new RoomDTO
                        {
                            Name = room
                        };

                        try
                        {
                            smartService.AddItem(roomObject, houseId);
                        }
                        catch (ValidationException ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                        break;
                    case 3:
                        AdditionalMenu(smartService, "EnterValueOfTemperature");
                        break;
                    case 4:
                        AdditionalMenu(smartService, "CalculateTheAverageTemperature");
                        break;
                    case 5:
                        ArrayList options = new ArrayList
                        {
                            "Houses",
                            "Rooms",
                            "Sensors",
                            "Records"
                        };
                        number = ChooseOption("Back", options);
                        displayItems(number);
                        break;
                }
            }
        }

        protected void Dispose(bool disposing)
        {
            smartService.Dispose();
        }

        private int ChooseOption(string StrParam, ArrayList Options)
        {
            Console.WriteLine("Choose option:");

            int count = 0;

            Console.WriteLine($"{count++}. {StrParam}");

            foreach (Object option in Options)
            {
                Console.WriteLine($"{count++}. {option}");
            }

            string str;
            int number;

            str = Console.ReadLine();
            while (!int.TryParse(Convert.ToString(str), out number) && number < 0 && number > Options.Count && IsDigit(str))
            {
                str = Console.ReadLine();
            }

            return number;
        }

        private int ChooseOption(string StrParam, List<HouseViewModel> Options)
        {
            Console.WriteLine("Choose option:");

            int count = 0;

            Console.WriteLine($"{count++}. {StrParam}");

            foreach (HouseViewModel option in Options)
            {
                Console.WriteLine($"{count++}. {option.Name}");
            }

            string str;
            int number;

            str = Console.ReadLine();
            while (int.TryParse(Convert.ToString(str), out number) && number < 0 && number > count && IsDigit(str))
            {
                str = Console.ReadLine();
            }

            return number;
        }

        private int ChooseOption(string StrParam, List<RoomViewModel> Options)
        {
            Console.WriteLine("Choose option:");

            int count = 0;

            Console.WriteLine($"{count++}. {StrParam}");

            foreach (RoomViewModel option in Options)
            {
                Console.WriteLine($"{count++}. {option.Name}");
            }

            string str;
            int number;

            str = Console.ReadLine();
            while (int.TryParse(Convert.ToString(str), out number) && number < 0 && number > count && IsDigit(str))
            {
                str = Console.ReadLine();
            }

            return number;
        }

        private void AdditionalMenu(ISmartService smartService, string str)
        {
            ArrayList Places = new ArrayList { "In house", "In room" };

            ArrayList Durations = new ArrayList { "Day", "Month", "Year" };

            int option;

            switch (str)
            {
                case "EnterValueOfTemperature":
                    option = ChooseOption("Back", Places);
                    if (option == 0) return;
                    EnterValueMenu(smartService, (string)Places[option - 1]);
                    break;

                case "CalculateTheAverageTemperature":
                    option = ChooseOption("Back", Places);
                    if (option == 0) return;
                    CalculateAverageMenu(smartService, (string)Places[option - 1]);
                    break;
            }

            void EnterValueMenu(ISmartService smartService, string myStr)
            {
                int number;
                string data = "a";
                int house;
                int room;

                switch (myStr)
                {
                    case "In house":
                        houses = GetHouseList();
                        number = ChooseOption("Back", houses);
                        if (number == 0) return;
                        house = houses[number - 1].Id;

                        Console.WriteLine("Enter value of temperature in house");
                        while (!IsDigit(data))
                        {
                            data = Console.ReadLine();
                        }
                        try
                        {
                            smartService.EnterValueOfTemperature(Convert.ToInt32(data), house);
                        }
                        catch (ValidationException ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                        break;

                    case "In room":
                        houses = GetHouseList();
                        number = ChooseOption("Back", houses);
                        if (number == 0) return;
                        house = houses[number - 1].Id;

                        var RoomsDtos = smartService.ShowRoomsInHouse(house);
                        var mapperRoom = new MapperConfiguration(cfg => cfg.CreateMap<RoomDTO, RoomViewModel>()).CreateMapper();
                        var rooms = mapperRoom.Map<IEnumerable<RoomDTO>, List<RoomViewModel>>(RoomsDtos);

                        int numberRoom = ChooseOption("Back", rooms);
                        if (numberRoom == 0) return;
                        room = rooms[number - 1].Id;
                        Console.WriteLine($"Enter value of temperature in room {room} house {house}");
                        while (!IsDigit(data))
                        {
                            data = Console.ReadLine();
                        }
                        try
                        {
                            smartService.EnterValueOfTemperature(house, room, Convert.ToInt32(data));
                        }
                        catch (ValidationException ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                        break;

                }
            }

        void CalculateAverageMenu(ISmartService smartService, string myStr)
            {
                int number;
                int numberDuration;
                int numberRoom;
                int house;
                int room;
                int duration;

                switch (myStr)
                {
                    case "In house":
                        houses = GetHouseList();
                        number = ChooseOption("Back", houses);
                        if (number == 0) return;
                        house = houses[number - 1].Id;

                        numberDuration = ChooseOption("Back", Durations);
                        if (numberDuration == 0) return;
                        duration = numberDuration - 1;
                        try
                        {
                            Console.WriteLine($"Average for {duration} in {house} is {smartService.CalculateAverage(house, duration, 0)}");
                        }
                        catch(ValidationException ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                        break;

                    case "In room":
                        houses = GetHouseList();
                        number = ChooseOption("Back", houses);
                        if (number == 0) return;
                        house = houses[number - 1].Id;
                        
                        var RoomsDtos = smartService.ShowRoomsInHouse(house);
                        
                        var mapperRoom = new MapperConfiguration(cfg => cfg.CreateMap<RoomDTO, RoomViewModel>()).CreateMapper();
                        var rooms = mapperRoom.Map<IEnumerable<RoomDTO>, List<RoomViewModel>>(RoomsDtos);

                        numberRoom = ChooseOption("Back", rooms);
                        if (numberRoom == 0) return;
                        room = rooms[number - 1].Id;

                        numberDuration = ChooseOption("Back", Durations);
                        if (numberDuration == 0) return;
                        duration = numberDuration - 1;

                        try { 
                        Console.WriteLine($"Average for {duration} in {house} in {room} is {smartService.CalculateAverage(house, duration, room)}");
                        }
                        catch (ValidationException ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                        break;

                }
            }
        }
        private bool IsDigit(string str)
        {
            if (str == "") return false;
            foreach (char c in str)
            {
                if (c > '0' && c < '9')
                {
                    return true;
                }
                else return false;
            }
            return false;
        }

        private void displayItems(int number)
        {
            string s;
            IEnumerable coll;
            int index;
            int id;

            ArrayList Options = new ArrayList
            {
                "Edit",
                "Delete"
            };

            switch (number)
            {
                case 1: 
                    coll = GetHouseList();
                    s = string.Format("{0, 5} | {1, 10}", "Id", "Name");
                    Console.WriteLine(s);
                    for(int i = 0; i<s.Length; i++)
                    {
                        Console.Write("-=-");
                    }
                    Console.WriteLine("");
                    foreach (HouseViewModel obj in coll)
                    {
                        s = string.Format("{0, 5} | {1, 10}", obj.Id, obj.Name);
                        Console.WriteLine(s);
                    }
                    index = ChooseOption("Back", Options);
                    if (index == 0) return;
                    if (index == 1)
                    {
                        Console.WriteLine("Enter id");
                        try
                        {
                            id = Convert.ToInt32(Console.ReadLine());
                        }
                        catch (ValidationException ex)
                        {
                            Console.WriteLine(ex.Message);
                            return;
                        }
                        foreach (HouseViewModel obj in coll)
                            if (obj.Id == id)
                            {
                                smartService.Update(id, "House");
                                break;
                            }
                            else
                            {
                                Console.WriteLine("Id not found");
                                return;
                            }
                    }
                    if (index == 2)
                    {
                        Console.WriteLine("Enter id");
                        try
                        {
                            id = Convert.ToInt32(Console.ReadLine());
                        }
                        catch (ValidationException ex)
                        {
                            Console.WriteLine(ex.Message);
                            return;
                        }
                        foreach (HouseViewModel obj in coll)
                            if (obj.Id == id)
                            {
                                smartService.Delete(id, "House");
                                break;
                            }
                            else
                            {
                                Console.WriteLine("Id not found");
                                return;
                            }
                    }
                    break;
                case 2:
                    var RoomsDtos = smartService.ShowRooms();
                    var mapperRoom = new MapperConfiguration(cfg => cfg.CreateMap<RoomDTO, RoomViewModel>()).CreateMapper();
                    coll = mapperRoom.Map<IEnumerable<RoomDTO>, List<RoomViewModel>>(RoomsDtos);
                    
                    s = string.Format("{0, 5} | {1, 30}  | {2, 10}", "Id", "Name", "House Id");
                    Console.WriteLine(s);
                    for (int i = 0; i < s.Length - 30; i++)
                    {
                        Console.Write("-=-");
                    }
                    Console.WriteLine("");
                    foreach (RoomViewModel obj in coll)
                    {
                        s = string.Format("{0, 5} | {1, 30}  | {2, 10}", obj.Id, obj.Name, obj.HouseId);
                        Console.WriteLine(s);
                    }
                    index = ChooseOption("Back", Options);
                    if (index == 0) return;
                    if (index == 1)
                    {
                        Console.WriteLine("Enter id");
                        try
                        {
                            id = Convert.ToInt32(Console.ReadLine());
                        }
                        catch (ValidationException ex)
                        {
                            Console.WriteLine(ex.Message);
                            return;
                        }
                        foreach (RoomViewModel obj in coll)
                            if (obj.Id == id)
                            {
                                smartService.Update(id, "Room");
                                break;
                            }
                            else
                            {
                                Console.WriteLine("Id not found");
                                return;
                            }
                    }
                    if (index == 2)
                    {
                        Console.WriteLine("Enter id");
                        try
                        {
                            id = Convert.ToInt32(Console.ReadLine());
                        }
                        catch (ValidationException ex)
                        {
                            Console.WriteLine(ex.Message);
                            return;
                        }
                        foreach (RoomViewModel obj in coll)
                            if (obj.Id == id)
                            {
                                smartService.Delete(id, "Room");
                                break;
                            }
                            else
                            {
                                Console.WriteLine("Id not found");
                                return;
                            }
                    }
                    break;
                case 3:
                    var SensorsDtos = smartService.ShowSensors();
                    var mapperSensor = new MapperConfiguration(cfg => cfg.CreateMap<SensorDTO, SensorViewModel>()).CreateMapper();
                    coll = mapperSensor.Map<IEnumerable<SensorDTO>, List<SensorViewModel>>(SensorsDtos);

                    s = string.Format("{0, 5} | {1, 10}  | {2, 10}", "Id", "House Id", "Room Id");
                    Console.WriteLine(s);
                    for (int i = 0; i < s.Length; i++)
                    {
                        Console.Write("-=-");
                    }
                    Console.WriteLine("");
                    foreach (SensorViewModel obj in coll)
                    {
                        s = string.Format("{0, 5} | {1, 10}  | {2, 10}", obj.Id, obj.HouseId, obj.RoomId);
                        Console.WriteLine(s);
                    }
                    index = ChooseOption("Back", Options);
                    if (index == 0) return;
                    if (index == 1)
                    {
                        Console.WriteLine("Editing is not available");
                    }
                    if (index == 2)
                    {
                        Console.WriteLine("Enter id");
                        try
                        {
                            id = Convert.ToInt32(Console.ReadLine());
                        }
                        catch (ValidationException ex)
                        {
                            Console.WriteLine(ex.Message);
                            return;
                        }
                        foreach (SensorViewModel obj in coll)
                            if (obj.Id == id)
                            {
                                smartService.Delete(id, "Sensor");
                                break;
                            }
                            else
                            {
                                Console.WriteLine("Id not found");
                                return;
                            }
                    }
                    break;
                case 4:
                    var RecordsDtos = smartService.ShowRecords();
                    var mapperRecord = new MapperConfiguration(cfg => cfg.CreateMap<RecordDTO, RecordViewModel>()).CreateMapper();
                    coll = mapperRecord.Map<IEnumerable<RecordDTO>, List<RecordViewModel>>(RecordsDtos);

                    s = string.Format("{0, 5} | {1, 20}  | {2, 10}  | {3, 10}", "Id", "Date", "Data", "Sensor Id");
                    Console.WriteLine(s);
                    for (int i = 0; i < s.Length - 20; i++)
                    {
                        Console.Write("-=-");
                    }
                    Console.WriteLine("");
                    foreach (RecordViewModel obj in coll)
                    {
                        s = string.Format("{0, 5} | {1, 20}  | {2, 10}  | {3, 10}", obj.Id, obj.Date, obj.Data, obj.SensorId);
                        Console.WriteLine(s);
                    }
                    index = ChooseOption("Back", Options);
                    if (index == 0) return;
                    if (index == 1)
                    {
                        Console.WriteLine("Enter id");
                        try
                        {
                            id = Convert.ToInt32(Console.ReadLine());
                        }
                        catch(ValidationException ex)
                        {
                            Console.WriteLine(ex.Message);
                            return;
                        }
                        foreach(RecordViewModel obj in coll)
                            if(obj.Id == id)
                            {
                                smartService.Update(id, "Record");
                                break;
                            } else
                            {
                                Console.WriteLine("Id not found");
                                return;
                            }
                    }
                    if (index == 2)
                    {
                        Console.WriteLine("Enter id");
                        try
                        {
                            id = Convert.ToInt32(Console.ReadLine());
                        }
                        catch (ValidationException ex)
                        {
                            Console.WriteLine(ex.Message);
                            return;
                        }
                        foreach (RecordViewModel obj in coll)
                            if (obj.Id == id)
                            {
                                smartService.Delete(id, "Record");
                                break;
                            }
                            else
                            {
                                Console.WriteLine("Id not found");
                                return;
                            }
                    }
                    break;
            }
        }

        private List<HouseViewModel> GetHouseList()
        {
            IEnumerable<HouseDTO> houseDtos = smartService.ShowHouses();
            var mapperHouse = new MapperConfiguration(cfg => cfg.CreateMap<HouseDTO, HouseViewModel>()).CreateMapper();
            houses = mapperHouse.Map<IEnumerable<HouseDTO>, List<HouseViewModel>>(houseDtos);
            return houses;
        }

    }
}
