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
    /// Контроллер для работы с ежедневными записями эмоций
    /// </summary>
    [Route("api/daily-emotion")]
    [ApiController]
    public class DailyEmotionController : ControllerBase
    {
        private readonly IMediator _mediator;

        public DailyEmotionController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Создать запись эмоции (не готово!)
        /// </summary>
        /// <param name="dailyEmotionDTO">Данные о записи</param>
        /// <returns></returns>
        [HttpPost]
        public async Task CreateDailyEmotion(DailyEmotionDTO dailyEmotionDTO)
        {
            var command = new CreateDailyEmotionCommand { DailyEmotionDTO = dailyEmotionDTO };
            await _mediator.Send(command);
        }
    }
}
