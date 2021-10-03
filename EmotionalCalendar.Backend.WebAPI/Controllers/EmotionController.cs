using EmotionalCalendar.Backend.Models.EmotionEventModels;
using EmotionalCalendar.Backend.Models.EmotionEventModels.EmotionEventRequests;
using EmotionalCalendar.Backend.WebAPI.Domain.EmotionEventDomain.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EmotionalCalendar.Backend.WebAPI.Controllers
{
    /// <summary>
    /// Управление эмоциями (справочник)
    /// </summary>
    [Route("api/emotion")]
    [ApiController]
    public class EmotionController : ControllerBase
    {
        private readonly IMediator _mediator;

        public EmotionController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Получить весь справочник эмоций
        /// </summary>
        /// <returns>Справочник эмоций</returns>
        [HttpGet]
        public async Task<IEnumerable<Emotion>> GetAllEmotions()
        {
            return await _mediator.Send(new EmotionsGetAllCommand());
        }

        /// <summary>
        /// Создать эмоцию
        /// </summary>
        /// <param name="emotionRequest">Данные об эмоции</param>
        /// <returns></returns>
        [HttpPost]
        public async Task CreateEmotion(EmotionCreateRequest emotionRequest)
        {
            var command = new EmotionCreateCommand { EmotionRequest = emotionRequest };
            await _mediator.Send(command);
        }

        /// <summary>
        /// Обновить эмоцию в справочнике
        /// </summary>
        /// <param name="emotionRequest">Новые данные об эмоции</param>
        /// <returns></returns>
        [HttpPut]
        public async Task UpdateEmotion(EmotionUpdateRequest emotionRequest)
        {
            var command = new EmotionUpdateCommand { EmotionRequest = emotionRequest };
            await _mediator.Send(command);
        }

        /// <summary>
        /// Удалить эмоцию из справочника
        /// </summary>
        /// <param name="emotionId">ID эмоции</param>
        /// <returns></returns>
        [HttpDelete("{emotionId}")]
        public async Task DeleteEmotion(long emotionId)
        {
            var command = new EmotionDeleteCommand { EmotionId = emotionId };
            await _mediator.Send(command);
        }
    }
}
