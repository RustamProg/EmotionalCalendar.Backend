using EmotionalCalendar.Backend.Constracts.ApplicationUserContracts;
using EmotionalCalendar.Backend.Constracts.EmotionalEventContracts;
using EmotionalCalendar.Backend.Models.AnalyticsModels.AnalyticsResponse;
using EmotionalCalendar.Backend.Models.EmotionEventModels;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace EmotionalCalendar.Backend.WebAPI.Domain.AnalyticsDomain.Commands
{
    public class GetChartDataCommand : IRequest<IEnumerable<ChartData>>
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int DaysInterval { get; set; }
    }

    public class GetChartDataCommandHandler : IRequestHandler<GetChartDataCommand, IEnumerable<ChartData>>
    {
        private readonly IEmotionEventRepository _emotionEventRepository;
        private readonly IUserService _userService;

        public GetChartDataCommandHandler(IEmotionEventRepository emotionEventRepository, IUserService userService)
        {
            this._emotionEventRepository = emotionEventRepository;
            _userService = userService;
        }

        public async Task<IEnumerable<ChartData>> Handle(GetChartDataCommand request, CancellationToken cancellationToken)
        {
            var response = new List<ChartData>();
            var allEmotions = await _emotionEventRepository.GetAllEmotionsAsync();

            foreach (var emotion in allEmotions)
            {
                var emotionChartData = (await _emotionEventRepository.GetEmotionEventRatesByUserAndEmotion(_userService.User.Id, emotion.Id))
                    .GroupBy(p => p.CreateDate)
                    .OrderBy(p => p.Key)
                    .ToDictionary(p => p.Key.Date, p => p.Average(x => x.EmotionEventRates.Average(y => y.EmotionRate)));

                var chartData = await CreateChartData(emotion, emotionChartData, request);
                response.Add(chartData);
            }

            return response;
        }

        private async Task<ChartData> CreateChartData(Emotion emotion, IDictionary<DateTime, double> emotionChartData, GetChartDataCommand request)
        {
            var chartData = new ChartData
            {
                DisplayName = emotion.DisplayName,
                Name = emotion.Name,
                Color = new ChartColor { RedColor = emotion.RedColor, GreenColor = emotion.GreenColor, BlueColor = emotion.BlueColor }
            };

            for (DateTime currentDate = request.StartDate.Date; currentDate <= request.EndDate; currentDate = currentDate.AddDays(request.DaysInterval))
            {
                var avgValues = emotionChartData
                    .Where(x => x.Key >= currentDate && x.Key < currentDate.AddDays(request.DaysInterval));

                double? value = avgValues.Any() ? avgValues.Average(x => x.Value) : null;

                chartData.Data.Add(new ChartItem { axisX = currentDate.ToString("dd.MM"), axisY = value });
            }

            return chartData;
        }
    }
}
