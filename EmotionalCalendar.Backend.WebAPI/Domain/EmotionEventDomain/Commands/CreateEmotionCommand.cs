using AutoMapper;
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
    public class CreateEmotionCommand : IRequest
    {
        public EmotionDTO EmotionDTO { get; set; }
    }

    public class CreateEmotionCommandHandler : IRequestHandler<CreateEmotionCommand>
    {
        private readonly IEmotionEventRepository _emotionEventRepository;
        private readonly IMapper _mapper;

        public CreateEmotionCommandHandler(IEmotionEventRepository emotionEventRepository, IMapper mapper)
        {
            _emotionEventRepository = emotionEventRepository;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(CreateEmotionCommand request, CancellationToken cancellationToken)
        {
            if (request is null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            var emotion = _mapper.Map<Emotion>(request.EmotionDTO);
            await _emotionEventRepository.AddEmotion(emotion);

            return Unit.Value;
        }
    }
}
