using Ninject.Modules;
using SmartHouse.BLL.Interfaces;
using SmartHouse.BLL.Services;

namespace SmartHouse.PL.Util
{
    class SmartModule : NinjectModule
    {
        public override void Load()
        {
            Bind<ISmartService>().To<SmartService>();
        }
    }
}
