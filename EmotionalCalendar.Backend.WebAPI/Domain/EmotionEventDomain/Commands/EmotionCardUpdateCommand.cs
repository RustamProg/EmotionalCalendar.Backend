using AutoMapper;
using EmotionalCalendar.Backend.AppDbContext;
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
    public class EmotionCardUpdateCommand : IRequest
    {
        public EmotionCardUpdateRequest EmotionCardUpdateRequest { get; set; }
    }

    public class EmotionCardUpdateCommandHandler : IRequestHandler<EmotionCardUpdateCommand>
    {
        private readonly IEmotionEventRepository _emotionEventRepository;
        private readonly IMapper _mapper;
        private readonly IUserService _userService;

        public EmotionCardUpdateCommandHandler(IEmotionEventRepository emotionEventRepository, IUserService userService, IMapper mapper)
        {
            _emotionEventRepository = emotionEventRepository;
            _userService = userService;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(EmotionCardUpdateCommand request, CancellationToken cancellationToken)
        {
            if (request is null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            if (request.EmotionCardUpdateRequest is null)
            {
                throw new ArgumentNullException(nameof(request.EmotionCardUpdateRequest));
            }

            var oldEmotionEvent = await _emotionEventRepository.GetEventNoteByIdAsync(request.EmotionCardUpdateRequest.Id);
            await _emotionEventRepository.DeleteEventNoteAsync(request.EmotionCardUpdateRequest.Id);

            var eventNote = new EventNote
            {
                Title = request.EmotionCardUpdateRequest.EventNoteTitle,
                Content = request.EmotionCardUpdateRequest.EventNoteContent,
                CreateDate = oldEmotionEvent.CreateDate,
                UserId = _userService.User.Id
            };

            foreach (var emotion in request.EmotionCardUpdateRequest.Emotions)
            {
                var emotionFromDb = await _emotionEventRepository.GetEmotionByIdAsync(emotion.EmotionId);
                emotionFromDb.EmotionEventRates.Add(new EmotionEventRate { EventNote = eventNote, EmotionRate = emotion.EmotionRate });
                Validator.ValidateEmotion(emotionFromDb);
            }

            Validator.ValidateEventNote(eventNote);
            await _emotionEventRepository.AddEventNoteWithEmotionAsync(eventNote);
            await _emotionEventRepository.SaveDataAsync();
            return Unit.Value;
        }
    }
}
