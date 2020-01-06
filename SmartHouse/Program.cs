using Ninject;
using Ninject.Modules;
using SmartHouse.BLL.Infrastructure;
using SmartHouse.BLL.Interfaces;
using SmartHouse.PL.Controllers;
using SmartHouse.PL.Util;
using System;

namespace SmartHouse
{
    class Program
    {
        static void Main(string[] args)
        {
            SmartController controller = new SmartController();
            controller.Start();
            Console.ReadKey();
        }
    }
}
