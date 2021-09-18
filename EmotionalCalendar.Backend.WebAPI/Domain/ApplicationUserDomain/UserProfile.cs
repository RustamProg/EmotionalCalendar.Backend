using AutoMapper;
using EmotionalCalendar.Backend.Models.ApplicationUserModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmotionalCalendar.Backend.WebAPI.Domain.ApplicationUserDomain
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<ApplicationUserDTO, ApplicationUser>()
                .ForMember(dest => dest.Username, opt => opt.MapFrom(src => src.Username))
                .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.FirstName))
                .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.LastName))
                .ForMember(dest => dest.Role, opt => opt.MapFrom(src => src.Role));
        }
    }
}
