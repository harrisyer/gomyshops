using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoMyShops.Models.ViewModels
{
    public abstract class DetailsViewModels:BaseBAL
    {
        //private readonly IHttpContextAccessor _httpContextAccessor;

        public DetailsViewModels() : base()
        {
            //_httpContextAccessor = httpContextAccessor;
        }

        public string EditJson { get; set; } = string.Empty;
    }//end class
}//end namespacce
