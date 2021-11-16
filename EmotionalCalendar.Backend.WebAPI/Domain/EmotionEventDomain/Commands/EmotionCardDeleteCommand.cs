using EmotionalCalendar.Backend.Constracts.EmotionalEventContracts;
using EmotionalCalendar.Backend.WebAPI.Domain.EmotionEventDomain.Repository;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace EmotionalCalendar.Backend.WebAPI.Domain.EmotionEventDomain.Commands
{
    public class EmotionCardDeleteCommand : IRequest
    {
        public long Id { get; set; }
    }

    public class EmotionCardDeleteCommandHandler : IRequestHandler<EmotionCardDeleteCommand>
    {
        private readonly IEmotionEventRepository _emotionEventRepository;

        public EmotionCardDeleteCommandHandler(IEmotionEventRepository emotionEventRepository)
        {
            _emotionEventRepository = emotionEventRepository;
        }

        public async Task<Unit> Handle(EmotionCardDeleteCommand request, CancellationToken cancellationToken)
        {
            if (request is null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            await _emotionEventRepository.DeleteEventNoteAsync(request.Id);
            await _emotionEventRepository.SaveDataAsync();
            return Unit.Value;
        }
    }
}
