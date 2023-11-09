using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using GoMyShops.Models;
using GoMyShops.Commons;

namespace GoMyShops.Models.ViewModels
{
    public class LoginImageViewModels
    {
        //[Required]
        [Display(Name = "Username")]
        public string? UserName { get; set; } = string.Empty;

        public string? ImageCode { get; set; } = string.Empty;

        public bool IsUser { get; set; }

        public string? Src { get; set; } = string.Empty;

        public string? Type { get; set; } = string.Empty;

        public string? Message { get; set; } = string.Empty;

        [Required]
        [StringLength(50)]
        [Descriptions]
        public string? Phrase { get; set; } = string.Empty;

        [Required]
        [Display(Name = "Code")]
        public string? Code { get; set; } = string.Empty;

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        public List<LoginImageURLViewModels>? LoginImages1 { get; set; }

        public List<LoginImageURLViewModels>? LoginImages2 { get; set; }

        public List<LoginImageURLViewModels>? LoginImages3 { get; set; }

    }//end class

    public class LoginImageURLViewModels
    {
        public string? Src { get; set; } = string.Empty;
        public string? Code { get; set; } = string.Empty;

    }//end class
}//end namespace

