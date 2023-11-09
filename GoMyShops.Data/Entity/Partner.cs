using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
//using System.Data.Entity.Spatial;

namespace GoMyShops.Data.Entity
{
    //[Table("Partner")]
    public partial class Partner
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(30)]
        //[Index("IX_Partner_PartnerCode", 1, IsUnique = true)]
        public string PartnerCode { get; set; }

        [Required]
        [StringLength(250)]
        //[Index("IX_Partner_PartnerName", 1, IsUnique = true)]
        public string PartnerName { get; set; }

        [Required]
        [StringLength(5)]       
        public string PartnerType { get; set; }

        [StringLength(30)]       
        public string AccountManagerCode { get; set; }
        //[Required(AllowEmptyStrings = true)]
        //[StringLength(100)]
        //public string CompanyName { get; set; }

        //[Required(AllowEmptyStrings = true)]
        //[StringLength(50)]
        //public string IdNo { get; set; }

        [Required]
        [StringLength(300)]
        public string Address1 { get; set; }

        [Required(AllowEmptyStrings = true)]
        [StringLength(300)]
        public string Address2 { get; set; }

        //[Required]
        //[StringLength(20)]
        //public string Password { get; set; }

        //[Required]
        //[StringLength(50)]
        //public string City { get; set; }

        //[Required]
        //[StringLength(50)]
        //public string State { get; set; }

        [Required]
        [StringLength(20)]
        public string Zip { get; set; }

        [StringLength(5)]
        public string CountryCode { get; set; }

        [Required]
        [StringLength(100)]
        public string ContactName { get; set; }

        [Required(AllowEmptyStrings = true)]
        [StringLength(250)]
        public string CompanyUrl { get; set; }

        //[Required]
        //[StringLength(5)]
        //public string TitleCode { get; set; }

        //[Required(AllowEmptyStrings = true)]
        //[StringLength(50)]
        //public string Designation { get; set; }

        //[Required(AllowEmptyStrings = true)]
        //[StringLength(2)]
        //public string GenderCode { get; set; }

        [Required(AllowEmptyStrings = true)]
        [StringLength(50)]
        public string State { get; set; }

        [Required(AllowEmptyStrings = true)]
        [StringLength(100)]
        public string City { get; set; }

        [Required(AllowEmptyStrings = true)]
        [StringLength(100)]
        public string Email { get; set; }

        [Required(AllowEmptyStrings = true)]
        [StringLength(100)]
        public string EmailCustomerService { get; set; }

        [Required(AllowEmptyStrings = true)]
        [StringLength(100)]
        public string EmailFinance { get; set; }

        [Required(AllowEmptyStrings = true)]
        [StringLength(100)]
        public string EmailRisk { get; set; }

        [Required(AllowEmptyStrings = true)]
        [StringLength(100)]
        public string EmailTechnical { get; set; }

        [Required(AllowEmptyStrings = true)]
        [StringLength(30)]
        public string PhoneNo { get; set; }

        [Required(AllowEmptyStrings = true)]
        [StringLength(30)]
        public string MobileNo { get; set; }

        //[Required(AllowEmptyStrings = true)]
        //[StringLength(30)]
        //public string OfficeNo { get; set; }

        //[Required(AllowEmptyStrings = true)]
        //[StringLength(10)]
        //public string OfficeExt { get; set; }

        [Required(AllowEmptyStrings = true)]
        [StringLength(30)]
        public string FaxNo { get; set; }

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

        #region Bank Information
        [Required(AllowEmptyStrings = true)]
        [StringLength(50)]
        public string BankAccountNo { get; set; }

        [Required(AllowEmptyStrings = true)]
        [StringLength(50)]
        public string BankAccountName { get; set; }

        [Required(AllowEmptyStrings = true)]
        [StringLength(20)]
        public string BankCode { get; set; }

        [Required(AllowEmptyStrings = true)]
        [StringLength(300)]
        public string BankAddress1 { get; set; }

        [Required(AllowEmptyStrings = true)]
        [StringLength(300)]
        public string BankAddress2 { get; set; }

        //[Required(AllowEmptyStrings = true)]
        //[StringLength(50)]
        //public string BankCity { get; set; }

        [Required(AllowEmptyStrings = true)]
        [StringLength(50)]
        public string BankState { get; set; }

        [Required(AllowEmptyStrings = true)]
        [StringLength(100)]
        public string BankCity { get; set; }

        [Required(AllowEmptyStrings = true)]
        [StringLength(20)]
        public string BankZip { get; set; }

        [Required(AllowEmptyStrings = true)]
        [StringLength(5)]
        public string BankCountryCode { get; set; }

        [Required(AllowEmptyStrings = true)]
        [StringLength(10)]
        public string InstitutionCode { get; set; }

        [Required(AllowEmptyStrings = true)]
        [StringLength(10)]
        public string TransitNo { get; set; }

        [Required(AllowEmptyStrings = true)]
        [StringLength(20)]
        public string SwiftNo { get; set; }
        #endregion

        [Column(TypeName = "decimal(18,2)")]
        public decimal PartnerWireFee { get; set; }
    }//end class
}//end namespace
