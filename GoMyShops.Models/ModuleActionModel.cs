using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoMyShops.Models
{
    public class ModuleActionModel
    {
        public string ModuleActionType { get; set; } = string.Empty;
        public int ModuleID { get; set; }
        public int ParentID { get; set; }
        public int SortSequence { get; set; }
        public bool TopMenu { get; set; }       
        public bool isChecked { get; set; }
        public string ModuleName { get; set; } = string.Empty;
        public bool MenuFlag { get; set; }
        public bool DetailFlag { get; set; }
        public bool CreateFlag { get; set; }
        public bool EditFlag { get; set; }
        public bool DeleteFlag { get; set; }
        public bool ApproveFlag { get; set; }
        public List<ModuleActionModel> Children { get; set; }
    }//end class
}//end namespace