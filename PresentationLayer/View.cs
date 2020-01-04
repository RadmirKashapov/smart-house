using System;
using System.Collections;
using System.Text;

namespace PresentationLayer
{
    class View
    {
        internal void displayMenu()
        {
            Boolean flag = true;

            ArrayList Options = new ArrayList
            {
                "Add house",
                "Add room",
                "Enter value of temperature",
                "Calculate the average temperature"
            };

            ViewModel user = new ViewModel();
            ArrayList Houses = user.ShowHouses();

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
                        user.AddItem(house);
                        break;
                    case 2:

                        number = ChooseOption("Back", Houses);
                        if (number == 0) return;
                        house = (string)Houses[number - 1];

                        Console.WriteLine("Enter name of room");
                        room = Console.ReadLine();

                        user.AddItem(house, room);
                        break;
                    case 3:
                        AdditionalMenu(user, "EnterValueOfTemperature");
                        break;
                    case 4:
                        AdditionalMenu(user, "CalculateTheAverageTemperature");
                        break;
                }
            }
        }

        private void AdditionalMenu(ViewModel user, string str)
        {
            ArrayList Houses = user.ShowHouses();

            ArrayList Places = new ArrayList{ "In house", "In room" };

            ArrayList Durations = new ArrayList { "Day", "Month", "Year" };

            int option;

            switch (str)
            {
                case "EnterValueOfTemperature":
                    option = ChooseOption("Back", Places);
                    if (option == 0) return;
                    EnterValueMenu(user, (string)Places[option - 1]);  
                    break;

                case "CalculateTheAverageTemperature":
                    option = ChooseOption("Back", Places);
                    if (option == 0) return;
                    CalculateAverageMenu(user, (string)Places[option - 1]);
                    break;
            }

            void EnterValueMenu(ViewModel user, string myStr)
            {
                int number;
                string data="a";
                string house;
                string room;

                switch (myStr)
                {
                    case "In house":

                        number = ChooseOption("Back", Houses);
                        if (number == 0) return;
                        house = (string)Houses[number - 1];

                        Console.WriteLine("Enter value of temperature in house");
                        while (!IsDigit(data))
                        {
                            data = Console.ReadLine();
                        }
                        user.EnterValueOfTemperature(house, Convert.ToInt32(data));
                        break;

                    case "In room":

                        number = ChooseOption("Back", Houses);
                        if (number == 0) return;
                        house = (string)Houses[number - 1];

                        ArrayList RoomsInHouse = user.ShowRoomsInHouse(house);

                        int numberRoom = ChooseOption("Back", RoomsInHouse);
                        if (numberRoom == 0) return;
                        room = (string)RoomsInHouse[number - 1];
                        Console.WriteLine($"Enter value of temperature in {room} {house}");
                        while (!IsDigit(data))
                        {
                            data = Console.ReadLine();
                        }
                        user.EnterValueOfTemperature(house, room, Convert.ToInt32(data));
                        break;

                }
            }

            void CalculateAverageMenu(ViewModel user, string myStr)
            {
                int number;
                int numberDuration;
                int numberRoom;
                string house;
                string room;
                string duration;

                switch (myStr)
                {
                    case "In house":

                        number = ChooseOption("Back", Houses);
                        if (number == 0) return;
                        house = (string)Houses[number - 1];

                        numberDuration = ChooseOption("Back", Durations);
                        if (numberDuration == 0) return;
                        duration = (string)Durations[numberDuration - 1];

                        Console.WriteLine($"Average for {duration} in {house} is {user.CalculateAverage(house, duration)}");
                        break;

                    case "In room":

                        number = ChooseOption("Back", Houses);
                        if (number == 0) return;
                        house = (string)Houses[number - 1];

                        ArrayList RoomsInHouse = user.ShowRoomsInHouse(house);
                        numberRoom = ChooseOption("Back", RoomsInHouse);
                        if (numberRoom == 0) return;
                        room = (string)RoomsInHouse[number - 1];

                        numberDuration = ChooseOption("Back", Durations);
                        if (numberDuration == 0) return;
                        duration = (string)Durations[numberDuration - 1];

                        Console.WriteLine($"Average for {duration} in {house} in {room} is {user.CalculateAverage(house, room, duration)}");
                        break;

                }
            }
        }


        private bool IsDigit(string str)
        {
            if (str == "") return false;
            foreach(char c in str)
            {
                if (c > '0' && c < '9')
                {
                    return true;
                }
                else return false;
            }
            return false;
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

            string str = "a";
            int number;

            while (!int.TryParse(Convert.ToString(str), out number) && number < 0 && number > Options.Count)
            {
                str = Console.ReadLine();
            }

            return number;
        }
    }
}
