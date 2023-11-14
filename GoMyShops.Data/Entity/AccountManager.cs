using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
////using System.Data.Entity.Spatial;

namespace GoMyShops.Data.Entity
{
    //[Table("AccountManager")]
    public partial class AccountManager
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(30)]
       // [Index("IX_AccountManager", 1, IsUnique = true)]
        public string AccountManagerCode { get; set; }

        [Required]
        [StringLength(30)]      
        public string AccountManagerUserCode { get; set; }

        [Required(AllowEmptyStrings = true)]
        [StringLength(100)]
        public string AccountManagerName { get; set; }

        [Required]
        [StringLength(30)]
        public string CreatedBy { get; set; }

        public DateTime CreatedTime { get; set; }

        [StringLength(30)]
        public string ModifiedBy { get; set; }

        public DateTime? ModifiedTime { get; set; }

        [Required]
        [StringLength(2)]
        public string Status { get; set; }

    }//end class
}//end namespace
