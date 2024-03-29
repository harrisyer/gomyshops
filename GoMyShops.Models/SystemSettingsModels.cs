﻿using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;
using System.ComponentModel;

namespace GoMyShops.Models
{
    public class SystemDetailSettingsModels : ViewModels.DetailsViewModels
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public SystemDetailSettingsModels() : base()
        {

        }

        public SystemDetailSettingsModels(IHttpContextAccessor httpContextAccessor) : base()
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public int Id { get; set; }

        [Display(Name = "Type")]
        public string SettingsType { get; set; } = string.Empty;

        [Display(Name = "Name")]
        [StringLength(50)]
        public string SettingName { get; set; } = string.Empty;

        [Display(Name = "Value")]
        [StringLength(500)]
        public string SettingValue { get; set; } = string.Empty;
    }

    public class SystemSettingsListModel : ViewModels.ActionsListViewModels
    {
        public int Id { get; set; }

        [Display(Name = "Type")]
        public string SettingsType { get; set; } = string.Empty;

        [Display(Name = "Name")]
        public string SettingName { get; set; } = string.Empty;

        [Display(Name = "Value")]
        public string SettingValue { get; set; } = string.Empty;


    }

}//end namespace
