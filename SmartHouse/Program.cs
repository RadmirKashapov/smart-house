using Ninject;
using Ninject.Modules;
using SmartHouse.BLL.Infrastructure;
using SmartHouse.PL.Controllers;
using SmartHouse.PL.Util;
using System;

namespace SmartHouse
{
    class Program
    {
        static void Main(string[] args)
        {
            NinjectModule orderModule = new SmartModule();
            NinjectModule serviceModule = new ServiceModule("DefaultConnection");
            var kernel = new StandardKernel(orderModule, serviceModule);

            SmartController controller= new SmartController();
            controller.Start();
            Console.ReadKey();
        }
    }
}
