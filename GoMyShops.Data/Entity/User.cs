using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
//using System.Data.Entity.Spatial;

namespace GoMyShops.Data.Entity
{
    //[Table("User")]
    public partial class User
    {
        [StringLength(2)]
        public string CompanyCode { get; set; }

        [StringLength(20)]
        public string DistributorCode { get; set; }

        [StringLength(20)]
        public string BranchCode { get; set; }

        [Required(AllowEmptyStrings = true)]
        [StringLength(30)]
        //[Index("IX_User_CustomerCode", 1, IsUnique = false)]
        public string CustomerCode { get; set; }

        //[Required]
        //[StringLength(20)]
        ////[Index("IX_LocationCode", 1, IsUnique = true)]
        //public string LocationCode { get; set; }

        [Key]
        [StringLength(20)]
        public string Username { get; set; }

        //[StringLength(20)]
        //public string Password { get; set; }

        [Required]
        [StringLength(50)]
        public string FirstName { get; set; }

        [Required(AllowEmptyStrings =true)]
        [StringLength(50)]
        public string LastName { get; set; }

        [StringLength(255)]
        public string Address1 { get; set; }

        [StringLength(255)]
        public string Address2 { get; set; }

        //[StringLength(255)]
        //public string Address3 { get; set; }

        [StringLength(5)]
        public string Country { get; set; }
        
        [StringLength(50)]
        [Required(AllowEmptyStrings = true)]
        public string State { get; set; }

        [StringLength(100)]
        [Required(AllowEmptyStrings = true)]
        public string City { get; set; }

        [StringLength(15)]
        public string PostCode { get; set; }

        [StringLength(20)]
        public string PhoneNo { get; set; }

        [StringLength(20)]
        public string MobileNo { get; set; }

        [StringLength(50)]
        public string Email { get; set; }

        [Required]
        [StringLength(1)]
        public string Type { get; set; }

        //[Required(AllowEmptyStrings = true)]
        //[StringLength(20)]
        //public string Password { get; set; }

        [Required]
        [StringLength(2)]
        public string Status { get; set; }

        [Required]
        [StringLength(20)]
        public string CreatedBy { get; set; }

        public DateTime CreatedTime { get; set; }

        [StringLength(20)]
        public string ModifiedBy { get; set; }

        public DateTime? ModifiedTime { get; set; }

        [StringLength(20)]
        public string GroupCode { get; set; }

        //For Login Images
        [Required(AllowEmptyStrings = true)]
        [StringLength(30)]
        public string ImageCode { get; set; }

        [Required(AllowEmptyStrings = true)]
        [StringLength(50)]
        public string Phrase { get; set; }

    }//end class
}//end namespace