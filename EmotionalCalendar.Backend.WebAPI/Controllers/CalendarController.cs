using EmotionalCalendar.Backend.Models.CalendarModels.CalendarResponses;
using EmotionalCalendar.Backend.WebAPI.Domain.CalendarDomain;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace EmotionalCalendar.Backend.WebAPI.Controllers
{
    [Route("api/calendar")]
    [ApiController]
    public class CalendarController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CalendarController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Получить календарный месяц с максимальной эмоцией на дату
        /// </summary>
        /// <param name="monthNumInYear">Номер месяца</param>
        /// <param name="year">Год</param>
        /// <returns>Календарный месяц с максимальной эмоцией на дату</returns>
        [HttpGet]
        public async Task<CalendarMonth> GetCalendarMonth(int monthNumInYear, int year)
        {
            var command = new GetCalendarCommand { MonthNumInYear = monthNumInYear, Year = year };
            return await _mediator.Send(command);
        }
    }
}
