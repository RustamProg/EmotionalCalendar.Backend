using EmotionalCalendar.Backend.Constracts.ApplicationUserContracts;
using EmotionalCalendar.Backend.Constracts.EmotionalEventContracts;
using EmotionalCalendar.Backend.Models.CalendarModels.CalendarResponses;
using EmotionalCalendar.Backend.Models.EmotionEventModels;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace EmotionalCalendar.Backend.WebAPI.Domain.CalendarDomain
{
    public class GetCalendarCommand : IRequest<CalendarMonth>
    {
        public int MonthNumInYear { get; set; }
        public int Year { get; set; }
    }

    public class GetCalendarCommandHandler : IRequestHandler<GetCalendarCommand, CalendarMonth>
    {
        private readonly IEmotionEventRepository _emotionEventRepository;
        private readonly IUserService _userService;

        public GetCalendarCommandHandler(IEmotionEventRepository emotionEventRepository, IUserService userService)
        {
            _emotionEventRepository = emotionEventRepository;
            _userService = userService;
        }

        public async Task<CalendarMonth> Handle(GetCalendarCommand request, CancellationToken cancellationToken)
        {
            if (request is null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            var response = new CalendarMonth();
            var firstDate = GetFirstDate(request.Year, request.MonthNumInYear);
            var lastDate = GetLastDate(request.Year, request.MonthNumInYear);

            var currentMonth = new DateTime(request.Year, request.MonthNumInYear, 1);

            response.FullDate = currentMonth.ToString("Y");
            response.MonthNumInYear = request.MonthNumInYear;

            var emotionCalendarData = (await _emotionEventRepository.GetEmotionEventRatesByUser(_userService.User.Id))
                .Where(x => x.CreateDate.Date >= firstDate.Date && x.CreateDate < lastDate.Date)
                .GroupBy(p => p.CreateDate)
                .ToDictionary(p => p.Key.Date);

            var maxEmotions = GetMaxEmotionsPerDate(emotionCalendarData);

            for (DateTime currentDate = firstDate; currentDate <= lastDate; currentDate = currentDate.AddDays(1))
            {
                var day = new CalendarDay
                { 
                    CalendarDate = currentDate,
                    MaxEmotionId = maxEmotions.ContainsKey(currentDate) ? maxEmotions[currentDate] : null,
                    IsCurrentMonth = currentDate.Month == currentMonth.Month ? true : false
                };

                response.Days.Add(day);
            }

            return response;
        }

        private IDictionary<DateTime, long?> GetMaxEmotionsPerDate(IDictionary<DateTime, IGrouping<DateTime, EventNote>> emotionGroups)
        {
            var result = new Dictionary<DateTime, long?>();

            foreach (var emotionGroup in emotionGroups)
            {
                long? maxEmotionId = null;
                int? maxEmotionValue = null;
                foreach (var group in emotionGroup.Value)
                {
                    (var curMaxEmotionValue, var curMaxEmotionId) = group.EmotionEventRates
                        .Where(x => x.EmotionRate == group.EmotionEventRates.Max(m => m.EmotionRate))
                        .Select(p => (p.EmotionRate, p.EmotionId))
                        .FirstOrDefault();

                    if (!maxEmotionValue.HasValue)
                    {
                        maxEmotionId = curMaxEmotionId;
                        maxEmotionValue = curMaxEmotionValue;
                    }
                    else if (maxEmotionValue < curMaxEmotionValue)
                    {
                        maxEmotionValue = curMaxEmotionValue;
                        maxEmotionId = curMaxEmotionId;
                    }
                }

                result.TryAdd(emotionGroup.Key.Date, maxEmotionId);
            }

            return result;
        }

        private DateTime GetFirstDate(int year, int month)
        {
            var date = new DateTime(year, month, 1);
            var firstDayOfWeek = date.DayOfWeek;

            if (firstDayOfWeek != DayOfWeek.Monday)
            {
                if (firstDayOfWeek == DayOfWeek.Sunday)
                {
                    date = date.AddDays(-6);
                }
                else
                {
                    date = date.AddDays(-(int)firstDayOfWeek + 1);
                }
            }

            return date;
        }

        private DateTime GetLastDate(int year, int month)
        {
            var date = new DateTime(year, month, DateTime.DaysInMonth(year, month));
            var lastDayOfWeek = date.DayOfWeek;

            if (lastDayOfWeek != DayOfWeek.Sunday)
            {
                date = date.AddDays(7 - (int)lastDayOfWeek);
            }

            return date;
        }
    }
}
