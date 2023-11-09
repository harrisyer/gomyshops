using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
//using System.Data.Entity.Spatial;

namespace GoMyShops.Data.Entity
{
    //[Table("Location")]
    public partial class Location
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(20)]
        //[Index("IX_Branch", 1, IsUnique = false)]
        public string BranchCode { get; set; }

        [Required]
        [StringLength(20)]
        //[Index("IX_LocationCode", 1, IsUnique = true)]
        public string LocationCode { get; set; }

        [Required]
        [StringLength(100)]
        public string LocationName { get; set; }

        public bool IsStation { get; set; }

        [StringLength(20)]
        public string CommissionCode { get; set; }

        [Required]
        [StringLength(1)]
        public string Status { get; set; }

        [Required]
        [StringLength(20)]
        public string CreatedBy { get; set; }

        public DateTime CreatedTime { get; set; }

        [StringLength(20)]
        public string ModifiedBy { get; set; }

        public DateTime? ModifiedTime { get; set; }
    }//end class
}//end namespace
