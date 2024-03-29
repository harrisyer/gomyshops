﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
namespace GoMyShops.Models
{
    public class CaptchaResponse
    {
        [JsonProperty("success")]
        public bool Success
        {
            get;
            set;
        }
        [JsonProperty("error-codes")]
        public List<string>? ErrorMessage
        {
            get;
            set;
        }
    }//end class
}//end namespace
