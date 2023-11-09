using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using GoMyShops.Commons;

namespace GoMyShops.Models.ViewModels
{
    public class AuditViewModels 
    {
        //private int CompanyCode = Convert.ToInt32(SessionBuilder.Get("CompanyCode"));

       public AuditViewModels()
       {
      
       }
        [Display(Name = "User ID")]
        public string srcUserID { get; set; }

        [Display(Name = "Date From")]
        public string srcDateFrom { get; set; }

        [Display(Name = "Date To")]
        public string srcDateTo { get; set; }

        [Display(Name = "Entity")]
        public string srcEntity { get; set; }

        [Display(Name = "Key Value")]
        public string srcKeyValue { get; set; }

        [Display(Name = "Action")]
        public string srcEmptyLogin { get; set; }

        public IEnumerable<SelectListItem> EmptyLoginDDL
        {
            get;
            set;
        }

        public IEnumerable<SelectListItem> EntityDDL
       {
           get;
           set;
       }
      

    }//end class

    public class AuditListViewModels : ActionsListViewModels
    {
        public int AUditDetailID { get; set; }
        public int KeyFieldID { get; set; }
        public string AreaAccessed { get; set; }
        public string DataModel { get; set; }
        public string CompositeKeys { get; set; }
        public int AuditID { get; set; }
        public int AuditActionTypeENUM { get; set; }
        public string AccessType { get; set; }
        public string UserName { get; set; }
        public string IPAddress { get; set; }
        public string KeyFieldValue { get; set; }
        public string BrowserID { get; set; }
        public string Changes { get; set; }
        public string BrowserType { get; set; }
        public string sStartTime { get; set; }
        public string sEndTime { get; set; }
        public string sAccessTime { get; set; }
        public int Type { get; set; }
        public string sType { get; set; }
        public System.DateTime StartTime { get; set; }
        public System.DateTime EndTime { get; set; }
        public Nullable<System.DateTime> AccessTime { get; set; }
    }


    public class AuditChange
    {
        public string Title { get; set; }
        public string FilterParam1 { get; set; }
        public string FilterParam2 { get; set; }
        public string FilterParam3 { get; set; }
        public string DateTimeStamp { get; set; }
        public CommonSetting.AuditActionType AuditActionType { get; set; }
        public string AuditActionTypeName { get; set; }
        public string AreaAccessed { get; set; }
        public List<AuditDelta> Changes { get; set; }
        public List<AuditValue> CompositeKeys { get; set; }
        public AuditChange()
        {
            Changes = new List<AuditDelta>();
        }
    }//end class

    public class AuditValue
    {
        public string FieldName { get; set; }
        public string Value { get; set; }
    }//end class

    public class AuditDelta
    {
        public string FieldName { get; set; }
        public string ValueBefore { get; set; }
        public string ValueAfter { get; set; }
    }//end class

    public class AuditDeltaForEditListing
    {
        public string KeyCode { get; set; }
        public string KeyCodeValue { get; set; }
        public string FieldKey1 { get; set; }
        public string FieldKey2 { get; set; }
        public string FieldKey3 { get; set; }
        public string FieldKeyValue1 { get; set; }
        public string FieldKeyValue2 { get; set; }
        public string FieldKeyValue3 { get; set; }
        public string FieldName { get; set; }
        public string ValueBefore { get; set; }
        public string ValueAfter { get; set; }
    }//end class

}//end namespace