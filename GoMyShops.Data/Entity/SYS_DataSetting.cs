using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
////using System.Data.Entity.Spatial;

namespace GoMyShops.Data.Entity
{  
    public partial class SYS_DataSetting
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int Id { get; set; } 

        [StringLength(50)]
        public string SettingName { get; set; }

        [StringLength(500)]
        public string SettingValue { get; set; }     

        public Boolean IsReadOnly { get; set; }

    }//end class
}//end namespace