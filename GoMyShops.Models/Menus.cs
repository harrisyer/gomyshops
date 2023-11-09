using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoMyShops.Models
{
    public interface IAdminMenuItem
    {
        bool TopMenu { get; set; }
        int MenuItemId { get; set; }
        bool isChecked { get; set; }
        //used for check parent
        int ParentID { get; set; }
        int SortSequence { get; set; }
        string MenuItemResourceKey { get; set; }
        string ModuleType { get; set; }
        string DisplayText { get; set; }
        string ControllerName { get; set; }
        string ActionName { get; set; }
        string BreadCrumb { get; set; }
        string Default { get; set; }
        string ApplicationType { get; set; }
        string IconName { get; set; }
        List<IAdminMenuItem> Children { get; set; }
    }

    public class AdminMenuItem : IAdminMenuItem
    {
        public bool TopMenu { get; set; }
        public int MenuItemId { get; set; }
        public bool isChecked { get; set; }
        //used for check parent
        public int ParentID { get; set; }
        public int SortSequence { get; set; }
        public string MenuItemResourceKey { get; set; }
        public string ModuleType { get; set; }
        public string DisplayText { get; set; }
        public string ControllerName { get; set; }
        public string ActionName { get; set; }
        public string ApplicationType { get; set; }
        public string IconName { get; set; }
        public string BreadCrumb { get; set; }
        public string Default { get; set; }
        private List<IAdminMenuItem> children;
        public List<IAdminMenuItem> Children
        {
            get
            {
                if (children == null)
                {
                    children = new List<IAdminMenuItem>();
                }
                return children;
            }

            set
            {
            }
        }
    }//end class
}//end namespace
