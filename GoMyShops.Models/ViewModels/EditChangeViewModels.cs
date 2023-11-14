using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using GoMyShops.Commons;

namespace GoMyShops.Models.ViewModels
{
    public class EditChangeViewModels
    {
        public string Title { get; set; }
        public string FilterParam1 { get; set; }
        public string FilterParam2 { get; set; }
        public string FilterParam3 { get; set; }
        public string DateTimeStamp { get; set; }
        //public CommonSetting.AuditActionType AuditActionType { get; set; }
        //public string AuditActionTypeName { get; set; }
        //public string AreaAccessed { get; set; }
        public List<AuditDelta> Changes { get; set; }
        public List<AuditValue> CompositeKeys { get; set; }
        public EditChangeViewModels()
        {
            Changes = new List<AuditDelta>();
        }
    }//end class
}//end namespace
