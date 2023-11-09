using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace GoMyShops.Models.ViewModels
{
    public class EntryBaseViewModels
    {
        public string PurchaseAmount { get; set; }

        public string OrderDescription { get; set; }

        public string OrderDetail { get; set; }

        public string CurrencyText { get; set; }

        public string Email { get; set; }

        public string CustomerPaymentPageText { get; set; }

        //public string IPAddress { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Address { get; set; }

        public string City { get; set; }

        public string State { get; set; }

        public string Zip { get; set; }

        public string Country { get; set; }

        public string Phone { get; set; }

        public string IsSameAsBilling { get; set; }

        public string ShipFirstName { get; set; }

        public string ShipLastName { get; set; }

        public string ShipAddress { get; set; }

        public string ShipCity { get; set; }

        public string ShipState { get; set; }

        public string ShipZip { get; set; }

        public string ShipCountry { get; set; }

        //public string CardHolderIP { get; set; }

        public string CardTypeText { get; set; }

        public string IssuerName { get; set; }

        public string TransactionOriginatedURL { get; set; }

        public Guid SessionID { get; set; }

        public Guid Signature { get; set; }
    }

    public class CreditCardEntryViewModels : EntryBaseViewModels
    {
        [Display(Name = "Card Holder Name")]
        public string CardHolderName { get; set; }

        [Display(Name = "Card Number")]
        public string CardNo { get; set; }

        [Display(Name = "Expiry Month")]
        public string CardExpireMonth { get; set; }

        [Display(Name = "Expiry Year")]
        public string CardExpireYear { get; set; }

        [Display(Name = "CVV")]
        public string SecurityCode { get; set; }

        public string MerchantURL { get; set; }

        public string MerchantResponseData { get; set; }

        public int PaymentTimeoutSecond { get; set; }

        public string PaymentTimeoutDateTime { get; set; }

        //Harris Add       
        [Display(Name = "TID Code")]
        public string CustomerTIDCode { get; set; }

        [Display(Name = "New Company Logo")]
        public string Logo { get; set; }

        [Display(Name = "Header Note")]
        public string HeaderNote { get; set; }

        [Display(Name = "Content Note")]
        public string ContentNote { get; set; }

        [Display(Name = "Themes")]
        public string ThemeCode { get; set; }

        public string MerchantName { get; set; }

        public string MerchantLogo { get; set; }

        public string AvailablePaymentMethod { get; set; }

        public int EntryType { get; set; }

    }

    public class FPXPaymentEntryViewModels : EntryBaseViewModels
    {
        public string BankID { get; set; }

        public IEnumerable<SelectListItem> BankListDDL { get; set; }

        public string MerchantURL { get; set; }

        public string MerchantResponseData { get; set; }

        public int PaymentTimeoutSecond { get; set; }

        public string PaymentTimeoutDateTime { get; set; }

        public string MerchantName { get; set; }

        public string MerchantLogo { get; set; }

        public string AvailablePaymentMethod { get; set; }

        public int EntryType { get; set; }
    }

    public class PaymentOptionEntryViewModels
    {
        public string PaymentPostString { get; set; }

         public string PaymentMethod { get; set; }

        public IEnumerable<SelectListItem> PaymentMethodDDL { get; set; }

        public string Email { get; set; }

        public string Phone { get; set; }

        public string MerchantURL { get; set; }

        public string MerchantResponseData { get; set; }

        public int PaymentTimeoutSecond { get; set; }

        public string PaymentTimeoutDateTime { get; set; }


        public Guid SessionID { get; set; }

        public string SessionIDString { get; set; }

        public string OrderDescription { get; set; }

        public string OrderDetail { get; set; }

        public string CurrencyText { get; set; }

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:N2}")]
        public decimal PurchaseAmount { get; set; }

        public string MerchantName { get; set; }

        public string MerchantLogo { get; set; }

        public string AvailablePaymentMethod { get; set; }

        public int EntryType { get; set; }
    }

    public class ErrorPageViewModel
    {
        public string errorCode { get; set; }
        public string errorDesc { get; set; }
    }

    public class ErrorPagePaymentInitViewModel
    {
        public string errorCode { get; set; }
        public string errorDesc { get; set; }
        public string OrderDesc { get; set; }
        public string TidCode { get; set; }
        public string MerchantResponseData { get; set; }
        public string ReturnURL { get; set; }
    }

    public class PaymentConfirmationViewModels : ErrorPageViewModel
    {
        public string OrderID { get; set; }

        public decimal PurchaseAmount { get;  set; }

        public string TransactionOriginatedURL { get; set; }

        public string OrderDescription { get; set; }

        public string TransactionStatus { get; set; }

        public string Currency { get; set; }

        public string MerchantURL { get; set; }

        public string MerchantResponseData { get; set; }

        public int EntryType { get; set; }

        public string CardType { get; set; }

        public string Reason { get; set; }
        
        public string BankReferenceID { get; set; }

        public DateTime TransactionTime { get; set; }

        public string BankID { get; set; }

    }

    public class ALCB_pxResViewModels
    {
        public string reqPostString { get; set; }
    }
}
