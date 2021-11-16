using AutoMapper;
using EmotionalCalendar.Backend.Constracts.EmotionalEventContracts;
using EmotionalCalendar.Backend.Models.EmotionEventModels;
using EmotionalCalendar.Backend.Models.EmotionEventModels.EmotionEventRequests;
using EmotionalCalendar.Backend.WebAPI.Domain.EmotionEventDomain.Repository;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace EmotionalCalendar.Backend.WebAPI.Domain.EmotionEventDomain.Commands
{
    public class EmotionCreateCommand : IRequest
    {
        public EmotionCreateRequest EmotionRequest { get; set; }
    }

    public class EmotionCreateCommandHandler : IRequestHandler<EmotionCreateCommand>
    {
        private readonly IEmotionEventRepository _emotionEventRepository;
        private readonly IMapper _mapper;

        public EmotionCreateCommandHandler(IEmotionEventRepository emotionEventRepository, IMapper mapper)
        {
            _emotionEventRepository = emotionEventRepository;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(EmotionCreateCommand request, CancellationToken cancellationToken)
        {
            if (request is null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            var emotion = _mapper.Map<Emotion>(request.EmotionRequest);

            Validator.ValidateEmotion(emotion);
            await _emotionEventRepository.AddEmotionAsync(emotion);
            await _emotionEventRepository.SaveDataAsync();
            return Unit.Value;
        }
    }
}
