using EmotionalCalendar.Backend.Models.AnalyticsModels.AnalyticsResponse;
using EmotionalCalendar.Backend.WebAPI.Domain.AnalyticsDomain.Commands;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmotionalCalendar.Backend.WebAPI.Controllers
{
    [Route("api/chart")]
    [ApiController]
    public class ChartController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ChartController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Получить данные для графика эмоций
        /// </summary>
        /// <param name="startDate">Начальная дата</param>
        /// <param name="endDate">Конечная дата</param>
        /// <param name="daysInterval">Интервал на оси Х в днях</param>
        /// <returns>Данные для графика</returns>
        [HttpGet]
        public async Task<IEnumerable<ChartData>> GetChartData(DateTime startDate, DateTime endDate, int daysInterval)
        {
            var command = new GetChartDataCommand { StartDate = startDate, EndDate = endDate, DaysInterval = daysInterval };
            return await _mediator.Send(command);
        }
    }
}
