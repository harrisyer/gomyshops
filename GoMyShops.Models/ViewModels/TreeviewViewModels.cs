using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
namespace GoMyShops.Models.ViewModels
{
    public class TreeviewViewModels
    {
        [Display(Name = "Paths")]
        public string FilePath { get; set; }
        public IEnumerable<SelectListItem> FilePathDDL { get; set; }
    }//end class
}//end namespace