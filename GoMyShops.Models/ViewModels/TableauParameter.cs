using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace GoMyShops.Models.ViewModels
{

    public interface ITableauParameter
    {
         string Type { get; set; }

         string DashboardCode { get; set; }
    }

    public class TableauParameter: ITableauParameter
    {
       public  string Type { get; set; }

       public string DashboardCode { get; set; }
    }
}
