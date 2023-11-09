using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
//using System.Data.Entity.Spatial;

namespace GoMyShops.Data.Entity
{
    //[Table("AuditMaster")]
    public partial class AuditMaster
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int AuditID { get; set; }

        [Required]
        [StringLength(50)]
        public string UserName { get; set; }

        [Required]
        [StringLength(50)]
        public string IPAddress { get; set; }

        [Required]
        [StringLength(30)]
        public string BrowserID { get; set; }

        [Required]
        [StringLength(30)]
        public string BrowserType { get; set; }

        [Required]
        public DateTime StartTime { get; set; }

        [Required]
        public DateTime EndTime { get; set; }

        public int Type { get; set; }

    }//end class
}//end namespace
