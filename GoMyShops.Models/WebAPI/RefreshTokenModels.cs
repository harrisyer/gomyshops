using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoMyShops.Models.WebAPI
{
    public class RefreshTokenModels: ErrorsModels
    {
        public string AccessToken { get; set; } = string.Empty;
        public string RefreshToken { get; set; }= string.Empty;
       
    }//end class
}//end namespace
