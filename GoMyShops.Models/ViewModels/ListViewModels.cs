using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoMyShops.Models.ViewModels
{
    public abstract class ListViewModels:ListBAL
    {
        //private readonly IHttpContextAccessor _httpContextAccessor;

        public ListViewModels() : base()
        {
            //_httpContextAccessor = httpContextAccessor;
        }
    }//end class
}//end namespace
