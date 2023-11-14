using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Http;

namespace GoMyShops.Models.ViewModels
{
    public class LoginModel
    {
        //public IEnumerable<User> UserInfos { get; set; }

        //[Required]
        [Display(Name = "Email")]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [Display(Name = "Username")]
        public string UserName { get; set; }


        public string UserType { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        public string MsgType { get; set; }

        public string Msg { get; set; }

        public string Msg2 { get; set; }

        public bool IsSignUp { get; set; }
        //For Login Phrase
        //public bool IsUser { get; set; }

        //public string Src { get; set; }

        //[Required]
        //[StringLength(50)]
        //[Descriptions]
        //public string Phrase { get; set; }

        //[Required]
        //[Display(Name = "Code")]
        //public string Code { get; set; }


    }//end class

    public class LoginRedirectModel: BaseBAL
    {
        public string RedirectAction { get; set; }
        public bool IsRedirect { get; set; }
        public string Code { get; set; }
        //public string UserName { get; set; }
        public bool isSuccess { get; set; }
        public int Type { get; set; }
        public string Message { get; set; }

        private readonly IHttpContextAccessor _httpContextAccessor;

        public LoginRedirectModel(IHttpContextAccessor httpContextAccessor) : base()
        {
            _httpContextAccessor = httpContextAccessor;
        }
        

        public LoginRedirectModel(IHttpContextAccessor httpContextAccessor,string redirectAction,string code,string userName, bool isRedirect) : base()
        {
            RedirectAction = redirectAction;
            IsRedirect = isRedirect;
            Code = code;
            UserName = userName;
            Type = 0;
        }
    }//end class

}//end namespace
