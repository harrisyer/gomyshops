using AutoMapper;
using GoMyShops.Commons;
using GoMyShops.Models;
using GoMyShops.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using GoMyShops.Models.ViewModels;

namespace GoMyShops.Mappings.Mappings
{
    public class SignUpMap : Profile
    {
        public SignUpMap()
        {
            CreateMap<SignUpDetailsViewModels, SignUp>()
              .ForMember(dest => dest.SignUpName, opt => opt.MapFrom(src => src.SignUpName))
              .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
              .ForMember(dest => dest.ContactNo, opt => opt.MapFrom(src => src.ContactNo))
              .ForMember(dest => dest.CompanyRegistrationNumber, opt => opt.MapFrom(src => src.CompanyRegistrationNumber))
              .ForMember(dest => dest.CompanyName, opt => opt.MapFrom(src => src.CompanyName))
              .ForMember(dest => dest.CreatedBy, opt => opt.MapFrom(src => src.CreatedBy))
              .ForMember(dest => dest.ModifiedBy, opt => opt.MapFrom(src => src.ModifiedBy))
              .ForMember(dest => dest.ContactNo, opt => opt.MapFrom(src => src.ContactNo))
              .ReverseMap();
        }

        
    }//end class
}//end namespace


