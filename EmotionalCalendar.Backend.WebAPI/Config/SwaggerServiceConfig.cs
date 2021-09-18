using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Swashbuckle.Swagger;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace EmotionalCalendar.Backend.WebAPI.Config
{
    public static class SwaggerServiceConfig
    {
        public static void AddSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "EmotionalCalendar.Backend.WebAPI",
                    Description = "Бэкэнд проекта \"Календарь эмоций\"",
                    Contact = new OpenApiContact
                    { Name = "Rustam Gabdulbarov,\nArtem Sobolev", Email = "emotionalcalendar@hotmail.com" }
                });
                var filePath = Path.Combine(AppContext.BaseDirectory, "EmotionalCalendar.Backend.WebAPI.xml");
                c.IncludeXmlComments(filePath);
            });
        }
    }
}
