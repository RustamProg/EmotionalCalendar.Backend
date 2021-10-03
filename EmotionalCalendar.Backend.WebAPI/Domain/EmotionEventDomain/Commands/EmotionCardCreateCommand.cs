using AutoMapper;
using EmotionalCalendar.Backend.Constracts.ApplicationUserContracts;
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
        public EmotionCardRequest DailyEmotionRequest { get; set; }
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

            if (request.DailyEmotionRequest is null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            var eventNote = new EventNote
            {
                Title = request.DailyEmotionRequest.EventNoteTitle,
                Content = request.DailyEmotionRequest.EventNoteContent,
                CreateDate = request.DailyEmotionRequest.CreateDate,
                UserId = _userService.User.Id,
                Emotions = new List<Emotion>()
            };

            foreach (var emotion in request.DailyEmotionRequest.Emotions)
            {
                var emotionById = await _emotionEventRepository.GetEmotionByIdAsync(emotion.EmotionId);
                eventNote.Emotions.Add(emotionById);
            }

            await _emotionEventRepository.AddEventNoteWithEmotionAsync(eventNote);

            return Unit.Value;
        }
    }
}
