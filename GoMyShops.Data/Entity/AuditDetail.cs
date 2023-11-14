using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
//using System.Data.Entity.Spatial;

namespace GoMyShops.Data.Entity
{
    //[Table("AuditDetail")]
    public partial class AuditDetail
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int Id { get; set; }

        [Required]
        public int AuditID { get; set; }

        [Required]
        public int KeyFieldID { get; set; }

        [Required]
        public int AuditActionTypeENUM { get; set; }

        [Required]
        [StringLength(50)]
        public string KeyFieldValue { get; set; }

        [Required]
        public DateTime DateTimeStamp { get; set; }

        [Required]
        [StringLength(100)]
        public string AreaAccessed { get; set; }

        [Required]
        [StringLength(100)]
        public string DataModel { get; set; }

        [Required]       
        public string CompositeKeys { get; set; }

       
        public string ValueChanges { get; set; }
      
        public string ValueBefore { get; set; }
        
        public string ValueAfter { get; set; }

    }//end class
}//end namespace
