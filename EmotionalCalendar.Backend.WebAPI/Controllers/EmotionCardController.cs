using EmotionalCalendar.Backend.Models.EmotionEventModels;
using EmotionalCalendar.Backend.Models.EmotionEventModels.EmotionEventRequests;
using EmotionalCalendar.Backend.Models.EmotionEventModels.EmotionEventResponse;
using EmotionalCalendar.Backend.WebAPI.Domain.EmotionEventDomain.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EmotionalCalendar.Backend.WebAPI.Controllers
{
    /// <summary>
    /// Контроллер для работы с ежедневными записями эмоций
    /// </summary>
    [Route("api/emotion-card")]
    [ApiController]
    public class EmotionCardController : ControllerBase
    {
        private readonly IMediator _mediator;

        public EmotionCardController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Создать запись эмоции (не готово!)
        /// </summary>
        /// <param name="emotionCardRequest">Данные о записи</param>
        /// <returns></returns>
        [HttpPost]
        public async Task CreateEmotionCard(EmotionCardCreateRequest emotionCardRequest)
        {
            var command = new EmotionCardCreateCommand { EmotionCardRequest = emotionCardRequest };
            await _mediator.Send(command);
        }

        /// <summary>
        /// Получить список карточек-эмоций для текущего пользователя (хэдэр)
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IEnumerable<EventNote>> GetEmotionCardForUser()
        {
            var command = new EmotionCardGetByUserCommand();
            return await _mediator.Send(command);
        }

        /// <summary>
        /// Обновить карточку с эмоцией
        /// </summary>
        /// <param name="emotionCardUpdateRequest">Данные об эмоции</param>
        /// <returns></returns>
        [HttpPut]
        public async Task UpdateEmotionCard(EmotionCardUpdateRequest emotionCardUpdateRequest)
        {
            var command = new EmotionCardUpdateCommand { EmotionCardUpdateRequest = emotionCardUpdateRequest };
            await _mediator.Send(command);
        }

        /// <summary>
        /// Удалить карточку с эмоциями
        /// </summary>
        /// <param name="cardId">ID каротчки</param>
        /// <returns></returns>
        [HttpDelete("{cardId}")]
        public async Task DeleteEmotionCard(long cardId)
        {
            var command = new EmotionCardDeleteCommand { Id = cardId };
            await _mediator.Send(command);
        }
    }
}
