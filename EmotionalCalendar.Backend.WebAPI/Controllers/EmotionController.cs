using EmotionalCalendar.Backend.Models.EmotionEventModels;
using EmotionalCalendar.Backend.WebAPI.Domain.EmotionEventDomain.Commands;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
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
            return await _mediator.Send(new GetAllEmotionsCommand());
        }

        /// <summary>
        /// Создать эмоцию
        /// </summary>
        /// <param name="emotionDTO">Данные об эмоции</param>
        /// <returns></returns>
        [HttpPost]
        public async Task CreateEmotion(EmotionDTO emotionDTO)
        {
            var command = new CreateEmotionCommand { EmotionDTO = emotionDTO };
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
            var command = new DeleteEmotionCommand { EmotionId = emotionId };
            await _mediator.Send(command);
        }
    }
}
