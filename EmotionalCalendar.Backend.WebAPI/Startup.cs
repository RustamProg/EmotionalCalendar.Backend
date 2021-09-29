using EmotionalCalendar.Backend.AppDbContext;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EmotionalCalendar.Backend.Constracts.ApplicationUserContracts;
using EmotionalCalendar.Backend.WebAPI.Domain.ApplicationUserDomain;
using EmotionalCalendar.Backend.WebAPI.Middlewares;
using MediatR;
using EmotionalCalendar.Backend.WebAPI.Config;
using EmotionalCalendar.Backend.WebAPI.Domain.EmotionEventDomain.Repository;
using AutoMapper;
using EmotionalCalendar.Backend.WebAPI.Domain.EmotionEventDomain;

namespace EmotionalCalendar.Backend.WebAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(options => 
                options.UseSqlServer(Configuration.GetConnectionString("MainConnection"),
                b => b.MigrationsAssembly("EmotionalCalendar.Backend.WebAPI")));

            services.AddControllers();

            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IEmotionEventRepository, EmotionEventRepository>();

            services.AddCors(options =>
            {
                options.AddPolicy("AllowAll", builder =>
                {
                    builder.AllowAnyOrigin();
                    builder.AllowAnyHeader();
                    builder.AllowAnyMethod();
                });
            });

            services.AddMediatR(typeof(Startup).Assembly);
            services.AddScoped(provider => new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new EmotionEventProfile(provider.GetService<IEmotionEventRepository>()));
            }).CreateMapper());
            services.AddSwagger();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "EmotionalCalendar.Backend.WebAPI v1"));
            }

            app.UseRouting();

            app.UseCors("AllowAll");

            app.UseAuthorization();

            app.UseMiddleware<ExceptionMiddleware>();

            app.UseMiddleware<UserMiddleware>();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
