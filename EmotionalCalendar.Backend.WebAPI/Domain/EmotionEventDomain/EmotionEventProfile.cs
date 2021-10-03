using AutoMapper;
using EmotionalCalendar.Backend.Models.EmotionEventModels;
using EmotionalCalendar.Backend.Models.EmotionEventModels.EmotionEventRequests;
using EmotionalCalendar.Backend.WebAPI.Domain.EmotionEventDomain.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmotionalCalendar.Backend.WebAPI.Domain.EmotionEventDomain
{
    public class EmotionEventProfile : Profile
    {
        private readonly IEmotionEventRepository _emotionEventRepository;

        public EmotionEventProfile(IEmotionEventRepository emotionEventRepository)
        {
            _emotionEventRepository = emotionEventRepository;

            CreateMap<EmotionCreateRequest, Emotion>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.DisplayName, opt => opt.MapFrom(src => src.DisplayName))
                .ForMember(dest => dest.RedColor, opt => opt.MapFrom(src => src.RedColor))
                .ForMember(dest => dest.BlueColor, opt => opt.MapFrom(src => src.BlueColor))
                .ForMember(dest => dest.GreenColor, opt => opt.MapFrom(src => src.GreenColor));

            CreateMap<EventNoteRequest, EventNote>()
                .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Title))
                .ForMember(dest => dest.Content, opt => opt.MapFrom(src => src.Content));
        }
    }
}
