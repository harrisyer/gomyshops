using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
////using System.Data.Entity.Spatial;

namespace GoMyShops.Data.Entity
{
    ////[Table("Announcement")]
    public partial class Announcement
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int Id { get; set; }

        [Required(AllowEmptyStrings = true)]
        [StringLength(500)]
        public string Title { get; set; }
                
        [Required(AllowEmptyStrings = true)]
        //[StringLength(2000)]
        public string Comment { get; set; }

        [Required]
        [StringLength(1)]
        public string Type { get; set; }

        [Required(AllowEmptyStrings = true)]
        [StringLength(1)]
        public string DisplayFrequency { get; set; }        

        public bool IsAdmin { get; set; }

        public bool IsMerchant { get; set; }

        public bool IsPartner { get; set; }

        //[Index("IX_Announcement_Time", 1, IsUnique = false)]
        [Required]
        public DateTime StartTime { get; set; }

        //[Index("IX_Announcement_Time", 2, IsUnique = false)]
        [Required]
        public DateTime EndTime { get; set; }

        public int Priority { get; set; }

        [Required]
        [StringLength(20)]
        public string CreatedBy { get; set; }

        [Required]
        public DateTime CreatedTime { get; set; }

        [StringLength(20)]
        public string ModifiedBy { get; set; }

        public DateTime? ModifiedTime { get; set; }

        [Required]
        [StringLength(2)]
        public string Status { get; set; }

    }//end class
}//end namespace