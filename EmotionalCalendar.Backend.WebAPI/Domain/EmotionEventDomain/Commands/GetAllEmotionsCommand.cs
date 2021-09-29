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
    public class GetAllEmotionsCommand : IRequest<IEnumerable<Emotion>>
    {
    }

    public class GetAllEmotionsCommandHandler : IRequestHandler<GetAllEmotionsCommand, IEnumerable<Emotion>>
    {
        private readonly IEmotionEventRepository _emotionEventRepository;

        public GetAllEmotionsCommandHandler(IEmotionEventRepository emotionEventRepository)
        {
            _emotionEventRepository = emotionEventRepository;
        }

        public async Task<IEnumerable<Emotion>> Handle(GetAllEmotionsCommand request, CancellationToken cancellationToken)
        {
            var emotions = await _emotionEventRepository.GetAllEmotions();
            return emotions;
        }
    }
}
