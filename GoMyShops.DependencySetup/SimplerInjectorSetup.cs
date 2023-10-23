using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SimpleInjector;
using SimpleInjector.Lifestyles;
using GoMyShops.BAL;
using GoMyShops.Data;
namespace GoMyShops.DependencySetup
{
    public static class SimplerInjectorSetup
    {
        public static void IOCSetup(Container container)
        {
            var lifestyle = new AsyncScopedLifestyle();
            container.Register<IServicesBAL, ServicesBAL>();
            container.Register<IDataSettingBAL, DataSettingBAL>();
            container.Register<ICentralizeNameBAL, CentralizeNameBAL>();
            container.Register<ISystemSettingsBAL, SystemSettingsBAL>();
            container.Register<IUnitOfWorkFactory, UnitOfWorkFactory>();
            container.Register(typeof(IRepository<>), typeof(Repository<>));
        }
    }//end Class
}//end Namespace