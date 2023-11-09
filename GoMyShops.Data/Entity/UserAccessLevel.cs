using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
//using System.Data.Entity.Spatial;

namespace GoMyShops.Data.Entity
{
    //[Table("UserAccessLevel")]
    public partial class UserAccessLevel
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int Id { get; set; }

        [StringLength(20)]
        public string CompanyCode { get; set; }

        [StringLength(20)]
        public string DistributorCode { get; set; }

        //[Index("IX_ucBranchUser", 1, IsUnique = true)]
        //[Required]
        [StringLength(20)]
        public string BranchCode { get; set; }

        //[Index("IX_ucBranchUser", 2, IsUnique = true)]
        [Required]
        [StringLength(256)]
        public string Username { get; set; }

        [Required]
        [StringLength(20)]
        public string CreatedBy { get; set; }

        public DateTime CreatedTime { get; set; }

        [StringLength(20)]
        public string ModifiedBy { get; set; }

        public DateTime? ModifiedTime { get; set; }

    }//end class
}//end namespace