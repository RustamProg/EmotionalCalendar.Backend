using EmotionalCalendar.Backend.WebAPI.Domain.EmotionEventDomain.Repository;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace EmotionalCalendar.Backend.WebAPI.Domain.EmotionEventDomain.Commands
{
    public class EmotionDeleteCommand : IRequest
    {
        public long EmotionId { get; set; }
    }

    public class EmotionDeleteCommandHandler : IRequestHandler<EmotionDeleteCommand>
    {
        private readonly IEmotionEventRepository _emotionEventRepository;

        public EmotionDeleteCommandHandler(IEmotionEventRepository emotionEventRepository)
        {
            _emotionEventRepository = emotionEventRepository;
        }

        public async Task<Unit> Handle(EmotionDeleteCommand request, CancellationToken cancellationToken)
        {
            if (request is null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            await _emotionEventRepository.DeleteEmotionAsync(request.EmotionId);
            return Unit.Value;
        }
    }
}
