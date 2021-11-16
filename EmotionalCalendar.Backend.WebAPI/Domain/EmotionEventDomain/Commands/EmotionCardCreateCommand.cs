using AutoMapper;
using EmotionalCalendar.Backend.Constracts.ApplicationUserContracts;
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
    public class EmotionCardCreateCommand : IRequest
    {
        public EmotionCardCreateRequest EmotionCardRequest { get; set; }
    }

    public class EmotionCardCreateCommandHandler : IRequestHandler<EmotionCardCreateCommand>
    {
        private readonly IEmotionEventRepository _emotionEventRepository;
        private readonly IMapper _mapper;
        private readonly IUserService _userService;

        public EmotionCardCreateCommandHandler(IEmotionEventRepository emotionEventRepository, IMapper mapper, IUserService userService)
        {
            _emotionEventRepository = emotionEventRepository;
            _mapper = mapper;
            _userService = userService;
        }

        public async Task<Unit> Handle(EmotionCardCreateCommand request, CancellationToken cancellationToken)
        {
            if (request is null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            if (request.EmotionCardRequest is null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            var eventNote = new EventNote
            {
                Title = request.EmotionCardRequest.EventNoteTitle,
                Content = request.EmotionCardRequest.EventNoteContent,
                CreateDate = request.EmotionCardRequest.CreateDate,
                UserId = _userService.User.Id,
            };

            foreach (var emotionRate in request.EmotionCardRequest.Emotions)
            {
                var emotion = await _emotionEventRepository.GetEmotionByIdAsync(emotionRate.EmotionId);
                var emotionEventRate = new EmotionEventRate
                {
                    EventNote = eventNote,
                    EmotionRate = emotionRate.EmotionRate
                };

                emotion.EmotionEventRates.Add(emotionEventRate);
                Validator.ValidateEmotion(emotion);
            }

            Validator.ValidateEventNote(eventNote);
            await _emotionEventRepository.AddEventNoteWithEmotionAsync(eventNote);
            await _emotionEventRepository.SaveDataAsync();

            return Unit.Value;
        }
    }
}
