using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace GoMyShops.Models.ViewModels
{
    public class LinkPaymentEntryViewModels
    {
        public class PaymentInitResponse
        {
            [Required]
            [StringLength(16, MinimumLength = 10)]
            [AlphaNumeric]
            public string CustomerPaymentPageText { get; set; }

            [Required]
            [StringLength(50)]
            [Descriptions]
            public string OrderDescription { get; set; }

            [Required]
            [StringLength(250)]
            [Descriptions]
            public string OrderDetail { get; set; }

            [Required]
            [StringLength(3)]
            [Alpha]
            public string CurrencyText { get; set; }

            [Required]
            [Range(0, 9999999.99)]
            public decimal PurchaseAmount { get; set; }

            [Required]
            [StringLength(100)]
            [Descriptions]
            public string FirstName { get; set; }

            [Required]
            [StringLength(100)]
            [Descriptions]
            public string LastName { get; set; }

            [Required]
            [StringLength(250)]
            [Descriptions]
            public string Address { get; set; }

            [Required]
            [StringLength(50)]
            [Descriptions]
            public string City { get; set; }

            [Required]
            [StringLength(50)]
            [Descriptions]
            public string State { get; set; }

            [Required]
            [StringLength(50)]
            [PositiveInteger]
            public string Zip { get; set; }

            [Required]
            [StringLength(2, MinimumLength = 2)]
            [Descriptions]
            public string Country { get; set; }

            [Required]
            [StringLength(250)]
            [EmailAddress]
            public string Email { get; set; }

            [Required]
            [StringLength(50)]
            [PositiveInteger]
            public string Phone { get; set; }

            [Required]
            [StringLength(1)]
            [PositiveInteger]
            public string IsSameAsBilling { get; set; }

            //[Required(AllowEmptyStrings = true)]
            [StringLength(100)]
            [Descriptions]
            public string ShipFirstName { get; set; }

            //[Required(AllowEmptyStrings = true)]
            [StringLength(100)]
            [Descriptions]
            public string ShipLastName { get; set; }

            //[Required(AllowEmptyStrings = true)]
            [StringLength(250)]
            [Descriptions]
            public string ShipAddress { get; set; }

            //[Required(AllowEmptyStrings = true)]
            [StringLength(50)]
            [Descriptions]
            public string ShipCity { get; set; }

            //[Required(AllowEmptyStrings = true)]
            [StringLength(50)]
            [Descriptions]
            public string ShipState { get; set; }

            //[Required(AllowEmptyStrings = true)]
            [StringLength(50)]
            [PositiveInteger]
            public string ShipZip { get; set; }

            //[Required(AllowEmptyStrings = true)]
            [StringLength(2)]
            [Descriptions]
            public string ShipCountry { get; set; }

            //[Required]
            [AlphaNumeric]
            public string Signature { get; set; }

            [Descriptions]
            public Guid SessionID { get; set; }

            public string MerchantName { get; set; }

            public string MerchantLogo { get; set; }

            [Required]
            public string TokenId { get; set; }

            public List<SelectListItem> StateDDL { get; set; }
            public List<SelectListItem> CountryDDL { get; set; }
        }

        public class LinkPaymentProcessResponseModel
        {
            public bool isSuccess { get; set; }
            public string ErrorCode { get; set; }
            public string ErrorDesc { get; set; }
            public string PaymentPostString { get; set; }
            public string MerchantReponse { get; set; }
            public string MerchantURL { get; set; }
        }

        public class PaymentProcessResponse
        {
            public bool isSuccess { set; get; }
            public string ErrorCode { set; get; }
            public string ErrorDesc { set; get; }
            public string LpPostString { set; get; }
        }
    }
}
