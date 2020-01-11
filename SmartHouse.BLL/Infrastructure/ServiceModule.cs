using SmartHouse.DAL.Interfaces;
using SmartHouse.DAL.Repositories;
using Ninject.Modules;

namespace SmartHouse.BLL.Infrastructure
{
    //ServiceModule представляет специальный модуль Ninject, 
    //который служит для организации сопоставления зависимостей
    //В частности, он устанавливает использование EFUnitOfWork в качестве объекта IUnitOfWork.
    public class ServiceModule : NinjectModule
    {
        private string connectionString;
        public ServiceModule(string connection)
        {
            connectionString = connection;
        }
        public override void Load()
        {
            Bind<IUnitOfWork>().To<EFUnitOfWork>().WithConstructorArgument(connectionString);
        }
    }
}
