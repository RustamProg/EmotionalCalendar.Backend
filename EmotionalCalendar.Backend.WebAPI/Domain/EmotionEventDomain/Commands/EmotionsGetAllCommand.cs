using EmotionalCalendar.Backend.Models.EmotionEventModels;
using EmotionalCalendar.Backend.WebAPI.Domain.EmotionEventDomain.Repository;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace EmotionalCalendar.Backend.WebAPI.Domain.EmotionEventDomain.Commands
{
    public class EmotionsGetAllCommand : IRequest<IEnumerable<Emotion>>
    {
    }

    public class EmotionsGetAllCommandHandler : IRequestHandler<EmotionsGetAllCommand, IEnumerable<Emotion>>
    {
        private readonly IEmotionEventRepository _emotionEventRepository;

        public EmotionsGetAllCommandHandler(IEmotionEventRepository emotionEventRepository)
        {
            _emotionEventRepository = emotionEventRepository;
        }

        public async Task<IEnumerable<Emotion>> Handle(EmotionsGetAllCommand request, CancellationToken cancellationToken)
        {
            var emotions = await _emotionEventRepository.GetAllEmotionsAsync();
            return emotions;
        }
    }
}
