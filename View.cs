using System;
using System.Collections.Generic;
using System.Text;

namespace Smart_House
{
    class View
    {
        internal void displayMenu()
        {
            Boolean flag = true;

            string[] Options = new string[]
            {
                "Add house",
                "Add room",
                "Enter value of temperature",
                "Calculate the average temperature"
            };

            while (flag == true)
            {
                Int32 number = ChooseOption("Exit", Options);

                ViewModel user = new ViewModel();

                switch (number)
                {
                    case 0:
                        flag = false;
                        break;
                    case 1:
                        user.AddHouse();
                        break;
                    case 2:
                        user.AddRoom();
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
            void EnterValueMenu(ViewModel user, string str)
            {
                int counter = 0;
                switch (str)
                {
                    case "House":

                        Console.WriteLine("Choose option:");

                        foreach (House house in user.ShowHouses())
                        {
                            Console.WriteLine("" + counter++ + house);
                        }

                        Console.WriteLine("0. Back");

                        str = Console.ReadLine();
                        Int32 number;

                        while (!int.TryParse(Convert.ToString(str), out number) && number < 0 && number > user.ShowHouses().Length)
                        {
                            str = Console.ReadLine();
                        }

                        if (number == 0) return;

                }
            }

            void CalculateAverageMenu(ViewModel user, string str)
            {

            }

            switch (str)
            {
                case "EnterValueOfTemperature":
                    break;
                case "CalculateTheAverageTemperature":
                    break;
            }
        }

        private int ChooseOption(String StrParam, string[] Options)
        {
            Console.WriteLine("Choose option:");

            int count = 0;

            Console.WriteLine($"{count++}. {StrParam}");

            foreach (string option in Options)
            {
                Console.WriteLine($"{count++}. {option}");
            }

            string str = "";
            Int32 number;

            while (!int.TryParse(Convert.ToString(str), out number) && number < 0 && number > Options.Length)
            {
                str = Console.ReadLine();
            }

            return number;
        }
    }
}
