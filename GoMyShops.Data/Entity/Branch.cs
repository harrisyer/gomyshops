using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
//using System.Data.Entity.Spatial;

namespace GoMyShops.Data.Entity
{
    //[Table("Branch")]
    public partial class Branch
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(2)]
        //[Index("IX_ucBranch", 1, IsUnique = true)]
        public string CompanyCode { get; set; }

        [Required]
        [StringLength(20)]
       
        public string DistributorCode { get; set; }

        [Required]
        [StringLength(20)]       
        //[Index("IX_Branch", 1, IsUnique = false)]
        public string BranchCode { get; set; }

        [Required]
        [StringLength(200)]
        public string Name { get; set; }

        [Required]
        [StringLength(255)]
        public string Address1 { get; set; }

        [StringLength(255)]
        public string Address2 { get; set; }

        [StringLength(255)]
        public string Address3 { get; set; }

        [StringLength(20)]
        public string GSTRegNo { get; set; }

        [StringLength(5)]
        public string CountryCode { get; set; }

        [StringLength(5)]
        public string StateCode { get; set; }

        [StringLength(100)]
        public string City { get; set; }

        [StringLength(15)]
        public string PostCode { get; set; }

        [StringLength(20)]
        public string PhoneNo { get; set; }

        [StringLength(20)]
        public string FaxNo { get; set; }

        [StringLength(100)]
        public string Email { get; set; }

        [StringLength(1)]
        public string Type { get; set; }

        [Required]
        [StringLength(20)]
        public string CreatedBy { get; set; }

        public DateTime CreatedTime { get; set; }

        [StringLength(20)]
        public string ModifiedBy { get; set; }

        public DateTime? ModifiedTime { get; set; }

        [StringLength(1)]
        public string Status { get; set; }
    }//end class
}//end namespace
