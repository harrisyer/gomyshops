using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using GoMyShops.Models;
using GoMyShops.Data.Entity;
namespace GoMyShops.Mappings.Mappings
{
    public class DataSettingMap:Profile
    {
        public DataSettingMap()
        {
           CreateMap<DataDetailsSettingModels, SYS_DataSetting>()
             .ForMember(dest => dest.SettingName, opt => opt.MapFrom(src => src.SettingName))
             .ForMember(dest => dest.SettingValue, opt => opt.MapFrom(src => src.SettingValue))
             .ForMember(dest => dest.IsReadOnly, opt => opt.MapFrom(src => src.IsReadOnly))
             .ReverseMap();        
        }
    }//end class
}//end namespace