using AutoMapper;
using EmotionalCalendar.Backend.Constracts.ApplicationUserContracts;
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
    public class CreateDailyEmotionCommand : IRequest
    {
        public DailyEmotionDTO DailyEmotionDTO { get; set; }
    }

    public class CreateDailyEmotionCommandHandler : IRequestHandler<CreateDailyEmotionCommand>
    {
        private readonly IEmotionEventRepository _emotionEventRepository;
        private readonly IMapper _mapper;
        private readonly IUserService _userService;

        public CreateDailyEmotionCommandHandler(IEmotionEventRepository emotionEventRepository, IMapper mapper, IUserService userService)
        {
            _emotionEventRepository = emotionEventRepository;
            _mapper = mapper;
            _userService = userService;
        }

        public async Task<Unit> Handle(CreateDailyEmotionCommand request, CancellationToken cancellationToken)
        {
            if (request is null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            if (request.DailyEmotionDTO is null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            //var dailyEmotion = _mapper.Map<DailyEmotion>(request.DailyEmotionDTO);
            var dailyEmotion = new DailyEmotion
            {
                UserId = _userService.User.Id,
                CreateDate = request.DailyEmotionDTO.CreateDate,
                EmotionRate = request.DailyEmotionDTO.EmotionRate,
                EventNote = new EventNote { Title = request.DailyEmotionDTO.EventNote.Title, Content = request.DailyEmotionDTO.EventNote.Content },
                Emotion = await _emotionEventRepository.GetEmotionByIdAsync(request.DailyEmotionDTO.EmotionId)
            };

            await _emotionEventRepository.AddDailyEmotionAsync(dailyEmotion);

            return Unit.Value;
        }
    }
}
