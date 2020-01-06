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

            IEnumerable<HouseDTO> houseDtos = smartService.ShowHouses();
            var mapperHouse = new MapperConfiguration(cfg => cfg.CreateMap<HouseDTO, HouseViewModel>()).CreateMapper();
            houses = mapperHouse.Map<IEnumerable<HouseDTO>, List<HouseViewModel>>(houseDtos);
           
            ArrayList Options = new ArrayList
            {
                "Add house",
                "Add room",
                "Enter value of temperature",
                "Calculate the average temperature"
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
                        smartService.AddItem(houseObject);
                        break;
                    case 2:
                        number = ChooseOption("Back", houses);
                        if (number == 0) return;
                        int houseId = houses[number - 1].Id;

                        Console.WriteLine("Enter name of room");
                        room = Console.ReadLine();

                        var roomObject = new RoomDTO
                        {
                            Name = room
                        };

                        smartService.AddItem(roomObject, houseId);
                        break;
                    case 3:
                        AdditionalMenu(smartService, "EnterValueOfTemperature");
                        break;
                    case 4:
                        AdditionalMenu(smartService, "CalculateTheAverageTemperature");
                        break;
                }
            }
        }
        private int ChooseOption(string StrParam, ArrayList Options)
        {
            Console.WriteLine("Choose option:");

            int count = 0;

            Console.WriteLine($"{count++}. {StrParam}");

            foreach (string option in Options)
            {
                Console.WriteLine($"{count++}. {option}");
            }

            string str;
            int number;

            str = Console.ReadLine();
            while (!int.TryParse(Convert.ToString(str), out number) && number < 0 && number > Options.Count)
            {
                str = Console.ReadLine();
            }

            return number;
        }

        private int ChooseOption(string StrParam, IEnumerable Options)
        {
            Console.WriteLine("Choose option:");

            int count = 0;

            Console.WriteLine($"{count++}. {StrParam}");

            foreach (string option in Options)
            {
                Console.WriteLine($"{count++}. {option}");
            }

            string str;
            int number;

            str = Console.ReadLine();
            while (int.TryParse(Convert.ToString(str), out number) && number < 0 && number > count)
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

                        number = ChooseOption("Back", houses);
                        if (number == 0) return;
                        house = houses[number - 1].Id;

                        Console.WriteLine("Enter value of temperature in house");
                        while (!IsDigit(data))
                        {
                            data = Console.ReadLine();
                        }
                        smartService.EnterValueOfTemperature(Convert.ToInt32(data), house);
                        break;

                    case "In room":

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
                        smartService.EnterValueOfTemperature(house, room, Convert.ToInt32(data));
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

                        number = ChooseOption("Back", houses);
                        if (number == 0) return;
                        house = houses[number - 1].Id;

                        numberDuration = ChooseOption("Back", Durations);
                        if (numberDuration == 0) return;
                        duration = numberDuration - 1;

                        Console.WriteLine($"Average for {duration} in {house} is {smartService.CalculateAverage(house, duration, 0)}");
                        break;

                    case "In room":

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

                        Console.WriteLine($"Average for {duration} in {house} in {room} is {smartService.CalculateAverage(house, duration, room)}");
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

        protected void Dispose(bool disposing)
        {
            smartService.Dispose();
        }
    }
}
