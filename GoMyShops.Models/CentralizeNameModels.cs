using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace GoMyShops.Models
{
    public class CentralizeDetailsNameModels : DataDetailsSettingModels
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CentralizeDetailsNameModels() : base(null)
        {

        }

        public CentralizeDetailsNameModels(IHttpContextAccessor httpContextAccessor) : base()
        {
            _httpContextAccessor = httpContextAccessor;
        }

        [Display(Name = "SQL Text")]
        public string SQLText { get; set; }

        [Display(Name = "Extra Suffix Name")]
        [StringLength(100)]
        public string ExtraValue { get; set; }

        public byte[] byteArray { get; set; }


    }//end class

    public class CentralizeFileModel
    {
        public string FileName { get; set; }

        public byte[] ByteArray { get; set; }
    }
}//end namespace
