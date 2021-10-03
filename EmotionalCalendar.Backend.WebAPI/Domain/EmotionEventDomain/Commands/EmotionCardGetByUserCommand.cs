using EmotionalCalendar.Backend.Constracts.ApplicationUserContracts;
using EmotionalCalendar.Backend.Models.EmotionEventModels;
using EmotionalCalendar.Backend.Models.EmotionEventModels.EmotionEventResponse;
using EmotionalCalendar.Backend.WebAPI.Domain.EmotionEventDomain.Repository;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace EmotionalCalendar.Backend.WebAPI.Domain.EmotionEventDomain.Commands
{
    public class EmotionCardGetByUserCommand : IRequest<IEnumerable<EventNote>>
    {
    }

    public class EmotionCardGetByUserCommandHandler : IRequestHandler<EmotionCardGetByUserCommand, IEnumerable<EventNote>>
    {
        private readonly IUserService _userService;
        private readonly IEmotionEventRepository _emotionEventRepository;

        public EmotionCardGetByUserCommandHandler(IUserService userService, IEmotionEventRepository emotionEventRepository)
        {
            _userService = userService;
            _emotionEventRepository = emotionEventRepository;
        }

        public async Task<IEnumerable<EventNote>> Handle(EmotionCardGetByUserCommand request, CancellationToken cancellationToken)
        {
            if (request is null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            var response = await _emotionEventRepository.GetEventNotesWithEmotionsByUserId(_userService.User.Id);

            return response;
        }
    }
}
