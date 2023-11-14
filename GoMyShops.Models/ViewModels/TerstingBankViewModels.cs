using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
namespace GoMyShops.Models.ViewModels
{
    public class TerstingBankViewModels
    {
        [StringLength(20)]
        public string BankCode { get; set; }

        [StringLength(100)]
        public string BankName { get; set; }

        [StringLength(2)]
        public string Status { get; set; }
    }
}
