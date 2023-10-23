using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoMyShops.Models
{
    public abstract class DetailsViewModels
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public DetailsViewModels(IHttpContextAccessor httpContextAccessor) 
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public string EditJson { get; set; }
    }//end class
}//end namespacce
