using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
////using System.Data.Entity.Spatial;

namespace GoMyShops.Data.Entity
{
    ////[Table("AppCtrlSU")]
    public partial class AppCtrlSU
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int Id { get; set; }

        public int AppCtrlID { get; set; }

        [StringLength(50)]
        public string AppCtrName { get; set; }

        [StringLength(200)]
        public string AppCtrDesc { get; set; }

        [StringLength(1)]
        public string AppCtrType { get; set; }

        public int SortOrder { get; set; }

        [StringLength(1)]
        public string Status { get; set; }
    }//end class
}//end namespace