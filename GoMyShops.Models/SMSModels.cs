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

namespace GoMyShops.Models
{
    public class SMSSendModels
    {
        public string UserName { get; set; } = string.Empty;
    }

    public class SMSModels
    {
        [StringLength(20, MinimumLength = 8)]
        public string Userkey { get; set; } = string.Empty;

        [StringLength(20, MinimumLength = 8)]
        public string Password { get; set; } = string.Empty;

        [StringLength(20, MinimumLength = 5)]
        public string MsgID { get; set; } = string.Empty;

        [StringLength(12, MinimumLength = 12)]//YYYYMMDDHHMMSS
        public string TimeStamp { get; set; } = string.Empty;

        [StringLength(20, MinimumLength = 3)]
        public string ServiceID { get; set; } = string.Empty;

        [StringLength(200)]
        public string aMsg { get; set; } = string.Empty;


        public string Mobile { get; set; } = string.Empty;

        public string MCN { get; set; } = string.Empty;

        [StringLength(20, MinimumLength = 3)]
        public string ChargeCode { get; set; } = string.Empty;

        [StringLength(2, MinimumLength = 2)]
        public string MsgType { get; set; } = string.Empty;

    }//end class
}//end namespace
