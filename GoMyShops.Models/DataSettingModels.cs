using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace GoMyShops.Models
{
    public class DataDetailsSettingModels : DetailsViewModels
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public DataDetailsSettingModels() : base(null)
        {

        }

        public DataDetailsSettingModels(IHttpContextAccessor httpContextAccessor) : base(httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public int Id { get; set; }

        [Display(Name="Name" )]
        [StringLength(50)]
        public string SettingName { get; set; }

        [Display(Name = "Value")]
        [StringLength(500)]
        public string SettingValue { get; set; }

        [Display(Name = "Read Only")]
        public Boolean IsReadOnly { get; set; }

    }//end class

    public class DataSettingListModel : ActionsListViewModels
    {
        public int Id { get; set; }

        [Display(Name = "Name")]
        public string SettingName { get; set; }

        [Display(Name = "Value")]
        public string SettingValue { get; set; }

        public Boolean IsReadOnly { get; set; }

        [Display(Name = "Read Only")]
        public string ReadOnly { get; set; }
    }



}//end namespace
