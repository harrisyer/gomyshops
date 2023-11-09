using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
//using System.Data.Entity.Spatial;

namespace GoMyShops.Data.Entity
{
    //[Table("Customer")]
    public partial class Customer
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(30)]
        //[Index("IX_Customer_CustomerCode", 1, IsUnique = true)]
        public string CustomerCode { get; set; }

        [Required(AllowEmptyStrings = true)]
        [StringLength(100)]
        public string CustomerName { get; set; }

        [Required(AllowEmptyStrings = true)]
        [StringLength(30)]      
        public string OnboardingCode { get; set; }

        [Required(AllowEmptyStrings = true)]
        [StringLength(100)]
        public string BusinessEntityName { get; set; }

        [Required(AllowEmptyStrings = true)]
        [StringLength(2)]
        public string BusinessType { get; set; }
        
        //[Required]
        //[StringLength(20)]
        //public string Password { get; set; }

        [Required]
        [StringLength(20)]
        public string AccountManagerCode { get; set; }

        [Required(AllowEmptyStrings = true)]
        [StringLength(30)]
        //[Index("IX_Customer_PartnerCode", 1, IsUnique = false)]
        public string PartnerCode { get; set; }

        [Required(AllowEmptyStrings = true)]
        [StringLength(100)]
        public string TradingAsDba { get; set; }

        [Required(AllowEmptyStrings = true)]
        [StringLength(50)]
        public string IdNo { get; set; }

        [Required]
        [StringLength(300)]
        public string Address1 { get; set; }

        [Required(AllowEmptyStrings = true)]
        [StringLength(300)]
        public string Address2 { get; set; }


        [Required(AllowEmptyStrings = true)]
        [StringLength(100)]
        public string City { get; set; }


        [Required(AllowEmptyStrings = true)]
        [StringLength(50)]
        public string State { get; set; }

        [Required]
        [StringLength(20)]
        public string Zip { get; set; }

        [StringLength(5)]
        public string CountryCode { get; set; }
              
        [Required(AllowEmptyStrings = true)]
        public string CompanyUrl { get; set; }
      
       
        public DateTime EstablishedDate { get; set; }

        [Required(AllowEmptyStrings = true)]
        [StringLength(50)]
        public string Personnel { get; set; }

        [Required(AllowEmptyStrings = true)]
        [StringLength(20)]
        public string TitleCode { get; set; }

        [Required(AllowEmptyStrings = true)]
        [StringLength(50)]
        public string TitleOther { get; set; }

        [Required(AllowEmptyStrings = true)]
        [StringLength(50)]
        public string Designation { get; set; }

        [Required(AllowEmptyStrings = true)]
        [StringLength(2)]
        public string GenderCode { get; set; }

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

        [Required(AllowEmptyStrings = true)]
        [StringLength(30)]
        public string OfficeNo { get; set; }

        [Required(AllowEmptyStrings = true)]
        [StringLength(10)]
        public string OfficeExt { get; set; }

        [Required(AllowEmptyStrings = true)]
        [StringLength(30)]
        public string FaxNo { get; set; }

        [Required]
        public bool ContactMerchant { get; set; }

        [Required]
        public bool ContactReseller { get; set; }

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

        [Required(AllowEmptyStrings = true)]
        [StringLength(100)]
        public string BankCity { get; set; }

        [Required(AllowEmptyStrings = true)]
        [StringLength(50)]
        public string BankState { get; set; }

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

        [Required(AllowEmptyStrings = true)]
        [StringLength(10)]
        public string RateReserve { get; set; }

        [Required(AllowEmptyStrings = true)]
        [StringLength(10)]
        public string RateRefundRatio { get; set; }

        [Required(AllowEmptyStrings = true)]
        [StringLength(10)]
        public string ReserveMonth { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal FeeMonthly { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal FeeYearly { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal FeeHighRisk { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal FeeSettle { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal FeeVoid { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal FeeRefund { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal FeeDecline { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal FeeRetrieval { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal FeeChargeBack { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal FeeSetup { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal FeeWire { get; set; }

        [Required]
        [StringLength(5)]
        public string ApplicationStatus { get; set; }

        [Required(AllowEmptyStrings = true)]
        [StringLength(500)]
        public string ApplicationRemark { get; set; }

        public DateTime? ApplicationDate { get; set; }

        [Required]
        public bool WhiteList { get; set; }

        [Required(AllowEmptyStrings = true)]
        [StringLength(30)]
        public string CreateCheckBy1 { get; set; }

        public DateTime? CreateCheckBy1Time { get; set; }

        [Required(AllowEmptyStrings = true)]
        [StringLength(30)]
        public string CreateCheckBy2 { get; set; }

        public DateTime? CreateCheckBy2Time { get; set; }

        [Required(AllowEmptyStrings = true)]
        [StringLength(30)]
        public string EditCheckBy1 { get; set; }

        public DateTime? EditCheckBy1Time { get; set; }

        [Required(AllowEmptyStrings = true)]
        [StringLength(30)]
        public string EditCheckBy2 { get; set; }

        public DateTime? EditCheckBy2Time { get; set; }

        [Required]
        [StringLength(30)]
        public string CreatedBy { get; set; }

        public DateTime CreatedTime { get; set; }

        [StringLength(30)]
        public string ModifiedBy { get; set; }

        public DateTime? ModifiedTime { get; set; }

        [Required(AllowEmptyStrings = true)]
        [StringLength(30)]
        public string ModifiedApplicationStatusBy { get; set; }

        public DateTime? ModifiedApplicationStatusTime { get; set; }

        [Required(AllowEmptyStrings = true)]
        [StringLength(30)]
        public string ModifiedStatusBy { get; set; }

        public DateTime? ModifiedStatusTime { get; set; }

        [Required]
        [StringLength(2)]
        public string Status { get; set; }

        [Required(AllowEmptyStrings = true)]
        [StringLength(500)]
        public string ApproveRemark { get; set; }

    }//end class
}//end namespace
