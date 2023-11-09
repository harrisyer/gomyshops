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
    public class DataGenerationViewModels
    {
        [Display(Name = "Date From")]
        public string srcDateFrom { get; set; }

        [Display(Name = "Date To")]
        public string srcDateTo { get; set; }
    }//end class
}//end namespace
