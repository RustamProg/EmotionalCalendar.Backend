using EmotionalCalendar.Backend.WebAPI.Domain.EmotionEventDomain.Repository;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace EmotionalCalendar.Backend.WebAPI.Domain.EmotionEventDomain.Commands
{
    public class DeleteEmotionCommand : IRequest
    {
        public long EmotionId { get; set; }
    }

    public class DeleteEmotionCommandHandler : IRequestHandler<DeleteEmotionCommand>
    {
        private readonly IEmotionEventRepository _emotionEventRepository;

        public DeleteEmotionCommandHandler(IEmotionEventRepository emotionEventRepository)
        {
            _emotionEventRepository = emotionEventRepository;
        }

        public async Task<Unit> Handle(DeleteEmotionCommand request, CancellationToken cancellationToken)
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
