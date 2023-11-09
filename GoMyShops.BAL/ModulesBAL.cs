using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using System.Threading.Tasks;
using GoMyShops.Data;
using GoMyShops.Data.Entity;
using GoMyShops.Models;
using GoMyShops.Commons;
using GoMyShops.Models.ViewModels;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;

namespace GoMyShops.BAL
{

    public interface IModulesBAL
    {
        Task<IList<IAdminMenuItem>> SelectModules(bool isInitMenu);
    }

    public class ModulesBAL: IModulesBAL
    {
        private readonly ILogger<ModulesBAL> _logger;
        private static string BreadCrumb = string.Empty;

        IRepository<ModuleSU> _iModule;
        IUnitOfWork _uow;
        IUsersBAL _userBAL;
        ISignUpBAL _signUpBAL;
        //IUnitOfWorkFactory _uowFactory;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public ModulesBAL(IHttpContextAccessor httpContextAccessor, ILogger<ModulesBAL> logger, IUnitOfWorkFactory uowFactory, IUsersBAL userBAL, ISignUpBAL signUpBAL)
        {
            //this._uowFactory = uowFactory;
            _userBAL = userBAL;
            this._uow = uowFactory.Create();
            _iModule = _uow.Repository<ModuleSU>();
            _signUpBAL = signUpBAL;
            _httpContextAccessor = httpContextAccessor;
            _logger = logger;
        }


        public async Task< IList<IAdminMenuItem>> SelectModules(bool isInitMenu)
        {
            try
            {
                IQueryable<IAdminMenuItem> modules = _iModule.GetAsQueryable(filter: x => x.ModuleStatus == 1).AsNoTracking()
                                                            .Where(x => x.Type == "N" || x.Type == "L")
                                                             .OrderBy(x=>x.ModuleSequence)
                                                             //.OrderBy(x => x.ModuleID)
                                                             .OrderBy(x => x.ModuleCode)
                                                             .Select(x=> new AdminMenuItem
                                                                     {
                                                                         ActionName =x.TargetAction,
                                                                         ControllerName = x.TargetController,
                                                                         DisplayText = x.ModuleName,
                                                                         MenuItemId = x.ModuleID,
                                                                         MenuItemResourceKey = x.ResourceKey,
                                                                         ParentID = (int)x.ParentModuleID,
                                                                         SortSequence = (int)x.ModuleSequence,
                                                                         ModuleType =x.Type,
                                                                         Default=x.Default,
                                                                         ApplicationType=x.ApplicationType,
                                                                         IconName=x.IconName,  
                                                             });

                if (isInitMenu)
                {
                    return await FillInMenuItemslist(modules.Where(x=>x.Default=="Y").ToList());
                }//end if

                



                return await FillInMenuItemslist(modules.ToList());
            }
            catch (Exception ex)
            {
                _logger.LogError("Error", ex);
            }
            finally { }
            return new List<IAdminMenuItem>();

        }

        private  async Task<List<IAdminMenuItem>> FillInMenuItemslist(List<IAdminMenuItem> p)
        {
            List<IAdminMenuItem> MenuList = new List<IAdminMenuItem>();

            var topMenuList = (from a in p
                               where a.ParentID == 0
                               select a).ToList();

            foreach (IAdminMenuItem a in topMenuList)
            {
                a.TopMenu = true;
                a.isChecked = true;
            }
            MenuList.AddRange(topMenuList.OrderBy(s => s.SortSequence));           

            var uncheckList = from a in p
                              where a.isChecked == false
                              select a;

            //add for skip display menu by user group
            if (!_httpContextAccessor.HttpContext.User.Identity.Name.IsNullOrEmptyString())
            {
                SignUpVerifyViewModels svvm = new SignUpVerifyViewModels();
                svvm.SignUpName = _httpContextAccessor.HttpContext.User.Identity.Name;
                _signUpBAL.IsSignUpUser(svvm);
                string groupCode = "";
                if (svvm.IsSignUpUser)
                {
                    groupCode = CommonSetting.GroupCode.OnBoarding;
                }
                else
                {
                    groupCode = _userBAL.GetUserGroup(_httpContextAccessor.HttpContext.User.Identity.Name);
                }

                //var uga = _uow.Repository<UserGroupAccess>().GetAsQueryable(x => x.GroupCode == groupCode).ToList();

                //var a = (from m in p
                //         join ug in _uow.Repository<UserGroupAccess>().GetAsQueryable(x => x.GroupCode == groupCode) on new { ModuleID = m.MenuItemId } equals new { ug.ModuleID }
                //         //where m.ParentID != 0
                //         where ug.MenuFlag == true
                //         select m).ToList();

                //add for top lvl access right
                if (_httpContextAccessor.HttpContext.User.Identity.Name.ToLower() != "root") 
                { 
                    var topListNotDefault = MenuList.Where(x => x.Default == "Y");

                    MenuList = (from c in MenuList
                                  // where (c.ModuleType!="L")
                                  where (from o in _uow.Repository<UserGroupAccess>().GetAsQueryable(x => x.GroupCode == groupCode)
                                         select o.ModuleID)
                                           .Contains(c.MenuItemId) 
                                  select c).ToList();
                    MenuList.AddRange(topListNotDefault);


                    MenuList = (from c in MenuList
                                  where !(from o in _uow.Repository<UserGroupAccess>().GetAsQueryable(x => x.GroupCode == groupCode && x.MenuFlag == false)
                                          select o.ModuleID)
                                           .Contains(c.MenuItemId)
                                  select c).ToList();

                    MenuList = MenuList.OrderBy(s => s.SortSequence).ToList();
                }//end if
                //end add for top lvl access right
                
                var uncheckList1=from c in uncheckList
                                 where (c.ModuleType=="L")
                                 select c;

                uncheckList = from c in uncheckList
                             // where (c.ModuleType!="L")
                              where (from o in _uow.Repository<UserGroupAccess>().GetAsQueryable(x => x.GroupCode == groupCode )
                                      select o.ModuleID)
                                       .Contains(c.MenuItemId)
                              select c;
                uncheckList = uncheckList.Union(uncheckList1);


                uncheckList = from c in uncheckList
                              where !(from o in _uow.Repository<UserGroupAccess>().GetAsQueryable(x => x.GroupCode == groupCode && x.MenuFlag == false)
                                      select o.ModuleID)
                                       .Contains(c.MenuItemId)
                              select c;
                uncheckList = uncheckList.OrderBy(x => x.SortSequence);
              
              
                FillInChildsMenuItemslist(MenuList, uncheckList.ToList());

                return MenuList;

            }//end if

            //

            FillInChildsMenuItemslist(MenuList, uncheckList.ToList());

            return MenuList;

        }

        private void FillInChildsMenuItemslist(List<IAdminMenuItem> checkList,List<UserGroupAccess> ugaList, List<IAdminMenuItem> uncheckList)
        {
            if (uncheckList.Exists(x => x.isChecked == false) == false)
            {
                return;
            }

            foreach (IAdminMenuItem a in checkList)
            {
                //var gotChildCount = ugaList.Where(x => x.MenuFlag == false
                //                                      && x.ModuleID==a.MenuItemId 
                //                                     ).Count();
                //if (gotChildCount >= 1)
                //{
                //    uncheckList = uncheckList.Where(x => x.MenuItemId != a.MenuItemId).ToList();
                //   // continue;
                //}

                //if (uncheckList.Any(cus => cus.ParentID  == a.MenuItemId ) == true)
                //{
                List<IAdminMenuItem> matchList = new List<IAdminMenuItem>();
                matchList = (from b in uncheckList
                             where b.ParentID == a.MenuItemId && b.isChecked != true
                             select b).ToList();
                matchList.ToList().ForEach(c => c.isChecked = true);
                a.Children.AddRange(matchList.ToList());

                if (a.Children.Count > 0)
                {
                    FillInChildsMenuItemslist(a.Children, ugaList, uncheckList);
                }

                //}
            }//end foreach
        }

        private void FillInChildsMenuItemslist(List<IAdminMenuItem> checkList, List<IAdminMenuItem> uncheckList)
        {
            if (uncheckList.Exists(x => x.isChecked == false) == false)
            {
                return;
            }

            foreach (IAdminMenuItem a in checkList)
            {
                //if (uncheckList.Any(cus => cus.ParentID  == a.MenuItemId ) == true)
                //{
                List<IAdminMenuItem> matchList = new List<IAdminMenuItem>();
                matchList = (from b in uncheckList
                             where b.ParentID == a.MenuItemId && b.isChecked != true
                             select b).ToList();
                matchList.ToList().ForEach(c => c.isChecked = true);
                a.Children.AddRange(matchList.ToList());

                if (a.Children.Count > 0)
                {
                    FillInChildsMenuItemslist(a.Children, uncheckList);
                }

                //}
            }//end foreach
        }



        private static void FillResourceKey(IList<IAdminMenuItem> ChirdAMIList)
        {
            foreach (IAdminMenuItem a in ChirdAMIList)
            {
                //TODO Harris Core-temp-off
                //a.DisplayText = MenuString(GoMyShops.Resources.Identity.ResourceManager.GetString(a.MenuItemResourceKey), a.DisplayText);
                if (a.ParentID==0 )
                {
                    BreadCrumb = string.Empty;  
                }//end if
                a.BreadCrumb = BreadCrumb + a.DisplayText;
                if (a.Children.Count > 0)
                {
                    //a.BreadCrumb = a.BreadCrumb + "->";
                    BreadCrumb = a.BreadCrumb + " > ";
                    FillResourceKey(a.Children);
                }//end if
            }
        }

       

        //public static System.Collections.Generic.IList<Models.ModuleList> SelectModules()
        //{
        //    Mapper.CreateMap<DAL.iModule, Models.ModuleList>();

        //    IList<DAL.iModule> Modules = DataBAL.Modules.SelectModules();
        //    IList<Models.ModuleList> ModuleListVMs = Mapper.Map<IList<DAL.iModule>, IList<Models.ModuleList>>(Modules);
        //    return ModuleListVMs;
        //}

        public static string MenuString(string item, string OringinalText)
        {
            if (item == null)
                return OringinalText;
            if (item == string.Empty)
                return OringinalText;
            return item;
        }//end MenuString

    }//end class
}//end namespace