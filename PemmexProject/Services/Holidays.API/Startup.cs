using Authentication.API.Services;
using Holidays.API.Common;
using Holidays.API.Database.context;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.ServiceBus;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using PemmexCommonLibs.Application.Helpers;
using PemmexCommonLibs.Application.Interfaces;
using PemmexCommonLibs.Infrastructure.Services;
using PemmexCommonLibs.Infrastructure.Services.FileUploadService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Holidays.API
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }


        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            
            services.AddDbContext<HolidaysContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("HolidaysConnection")));

            services.AddTransient<IDateTime, DateTimeService>();
            services.AddScoped<IApplicationDbContext>(provider => provider.GetService<HolidaysContext>());
            services.AddControllers();
            services.AddMediatR(Assembly.GetExecutingAssembly());
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddAuthentication("Bearer")
               .AddJwtBearer("Bearer", option =>
               {
                   option.Authority = "https://localhost:5001";
                   option.TokenValidationParameters = new TokenValidationParameters
                   {
                       ValidateAudience = false
                   };
               });
            services.AddAuthorization(options =>
            {
                options.AddPolicy("ClientIdPolicy", policy => policy.RequireClaim("client_id", "pemmex_mvc_client", "pemmexclient"));
            });

            services.AddHostedService<HolidayTopicReciever>();

            services.AddSingleton<ISubscriptionClient>(x => new SubscriptionClient(
                Configuration.GetValue<string>("AppSettings:QueueConnectionString"),
                Configuration.GetValue<string>("AppSettings:TopicName"),
                Configuration.GetValue<string>("AppSettings:SubscriptionName")
                ));

            services.AddScoped<ICommonHolidayDAL>(provider => new CommonHolidayDAL(provider.GetService<HolidaysContext>()));
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Holidays.API", Version = "v1" });
                c.SchemaFilter<EnumSchemaFilter>();
            });
            services.Configure<FormOptions>(options =>
            {
                options.MemoryBufferThreshold = Int32.MaxValue;
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Holidays.API v1"));
            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
