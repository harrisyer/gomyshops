using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
////using System.Data.Entity.Spatial;

namespace GoMyShops.Data.Entity
{  
    public partial class SYS_Setting
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int Id { get; set; }

        [StringLength(20)]
        public string SettingsType { get; set; } = string.Empty;

        [StringLength(50)]
        public string SettingName { get; set; } = string.Empty;

        [StringLength(500)]
        public string SettingValue { get; set; } = string.Empty;

    }//end class
}//end namespace