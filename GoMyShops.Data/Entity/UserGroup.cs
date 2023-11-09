using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
//using System.Data.Entity.Spatial;

namespace GoMyShops.Data.Entity
{
    //[Table("UserGroup")]
    public partial class UserGroup
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int Id { get; set; }

        [StringLength(2)]
       // [Index("UK_UserGroup_GroupCode",1, IsUnique = true)]
        public string CompanyCode { get; set; }

        //[Index("UK_UserGroup_GroupCode",2, IsUnique = true)]
        [StringLength(20)]
        public string GroupCode { get; set; }

        [Required(AllowEmptyStrings = true)]
        [StringLength(30)]
        //[Index("UK_UserGroup_GroupCode", 3, IsUnique = true)]
        public string CustomerCode { get; set; }

        [Required]
        [StringLength(20)]
        public string GroupName { get; set; }

        [StringLength(255)]
        public string Description { get; set; }

        [Required]
        [StringLength(1)]
        public string GroupType { get; set; }

        [Required]
        [StringLength(20)]
        public string CreatedBy { get; set; }

        public DateTime CreatedTime { get; set; }

        [StringLength(20)]
        public string ModifiedBy { get; set; }

        public DateTime? ModifiedTime { get; set; }

        [Required]
        [StringLength(1)]
        public string Status { get; set; }

        public int SecurityId { get; set; }

       

    }//end class
}//end namespace