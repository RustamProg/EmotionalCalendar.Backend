using AutoMapper;
using EmotionalCalendar.Backend.Models.EmotionEventModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmotionalCalendar.Backend.WebAPI.Domain.EmotionEventDomain
{
    public class EmotionEventProfile : Profile
    {
        public EmotionEventProfile()
        {
            CreateMap<EmotionDTO, Emotion>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.DisplayName, opt => opt.MapFrom(src => src.DisplayName))
                .ForMember(dest => dest.RedColor, opt => opt.MapFrom(src => src.RedColor))
                .ForMember(dest => dest.BlueColor, opt => opt.MapFrom(src => src.BlueColor))
                .ForMember(dest => dest.GreenColor, opt => opt.MapFrom(src => src.GreenColor));
        }
    }
}
