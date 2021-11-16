using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmotionalCalendar.Backend.Models.AnalyticsModels.AnalyticsResponse
{
    /// <summary>
    /// Модель для построения графика эмоций
    /// </summary>
    public class ChartData
    {
        /// <summary>
        /// Данные об эмоции на конкретную дату
        /// </summary>
        public List<ChartItem> Data { get; set; } = new List<ChartItem>();

        /// <summary>
        /// Данные о цвете
        /// </summary>
        public ChartColor Color { get; set; }

        /// <summary>
        /// Наименование для отображения в приложении
        /// </summary>
        public string DisplayName { get; set; }

        /// <summary>
        /// Техническое наименование
        /// </summary>
        public string Name { get; set; }
    }
}
