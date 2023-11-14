using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace GoMyShops.Models
{
    public class DataDetailsSettingModels :ViewModels.DetailsViewModels
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public DataDetailsSettingModels() : base()
        {

        }

        public DataDetailsSettingModels(IHttpContextAccessor httpContextAccessor) : base()
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public int Id { get; set; }

        [Display(Name="Name" )]
        [StringLength(50)]
        public string SettingName { get; set; } = string.Empty;

        [Display(Name = "Value")]
        [StringLength(500)]
        public string SettingValue { get; set; } = string.Empty;

        [Display(Name = "Read Only")]
        public Boolean IsReadOnly { get; set; }

    }//end class

    public class DataSettingListModel :ViewModels.ActionsListViewModels
    {
        public int Id { get; set; }

        [Display(Name = "Name")]
        public string SettingName { get; set; } = string.Empty;

        [Display(Name = "Value")]
        public string SettingValue { get; set; } = string.Empty;

        public Boolean IsReadOnly { get; set; }

        [Display(Name = "Read Only")]
        public string ReadOnly { get; set; } = string.Empty;
    }



}//end namespace
