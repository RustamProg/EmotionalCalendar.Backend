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
    public class EmotionUpdateCommand : IRequest
    {
        public EmotionUpdateRequest EmotionRequest { get; set; }
    }

    public class EmotionUpdateCommandHandler : IRequestHandler<EmotionUpdateCommand>
    {
        private readonly IEmotionEventRepository _emotionEventRepository;

        public EmotionUpdateCommandHandler(IEmotionEventRepository emotionEventRepository)
        {
            _emotionEventRepository = emotionEventRepository;
        }

        public async Task<Unit> Handle(EmotionUpdateCommand request, CancellationToken cancellationToken)
        {
            if (request is null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            if (request.EmotionRequest is null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            var emotion = await _emotionEventRepository.GetEmotionByIdAsync(request.EmotionRequest.EmotionId);

            emotion.Name = request.EmotionRequest.Name;
            emotion.DisplayName = request.EmotionRequest.DisplayName;
            emotion.RedColor = request.EmotionRequest.RedColor;
            emotion.GreenColor = request.EmotionRequest.GreenColor;
            emotion.BlueColor = request.EmotionRequest.BlueColor;

            Validator.ValidateEmotion(emotion);
            await _emotionEventRepository.UpdateEmotionAsync(emotion);
            await _emotionEventRepository.SaveDataAsync();

            return Unit.Value;
        }
    }
}
