using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SimpleInjector;
using SimpleInjector.Lifestyles;
using GoMyShops.BAL;
using GoMyShops.Commons;
using GoMyShops.Data;
using Microsoft.AspNetCore.Identity.UI.Services;
using GoMyShops.Models;
using GoMyShops.BAL.WebAPI;

namespace GoMyShops.DependencySetup
{
    public static class SimplerInjectorSetup
    {
        public static void IOCSetup(Container container)
        {
            var lifestyle = new AsyncScopedLifestyle();
          
            container.Register<IModulesBAL, ModulesBAL>();
            container.Register<IAdminMenuItem, AdminMenuItem>();
            container.Register<ISignUpBAL, SignUpBAL>();
            container.Register<ILoginBAL, LoginBAL>();
            container.Register<IUsersBAL, UsersBAL>();
            container.Register<IUserGroupBAL, UserGroupBAL>();
            container.Register<IAppCtrlUserProfileBAL, AppCtrlUserProfileBAL>();
            container.Register<IServicesBAL, ServicesBAL>();          
            container.Register<IEmailBAL, EmailBAL>();         
            container.Register<IEmailSender,Commons. EmailService>();
            container.Register<ISmsBAL, SmsBAL>();
            container.Register<IDistributorBAL, DistributorBAL>();
            container.Register<IBranchBAL, BranchBAL>();
            container.Register<ICompanyBAL, CompanyBAL>();
            container.Register<IAnnouncementBAL, AnnouncementBAL>();
            container.Register<IIndexBAL, IndexBAL>();
            container.Register<ITokenServiceBAL, TokenServiceBAL>();

            container.Register<IDataSettingBAL, DataSettingBAL>();
            container.Register<ICentralizeNameBAL, CentralizeNameBAL>();
            container.Register<ISystemSettingsBAL, SystemSettingsBAL>();
            container.Register<IUnitOfWork, UnitOfWork>(Lifestyle.Scoped);
            container.Register<IUnitOfWorkFactory, UnitOfWorkFactory>();
            container.Register(typeof(IRepository<>), typeof(Repository<>));
        }
    }//end Class
}//end Namespace