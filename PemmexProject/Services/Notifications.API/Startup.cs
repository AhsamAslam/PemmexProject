using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Notifications.API.Database.context;
using Notifications.API.Database.Repositories.Interface;
using Notifications.API.Database.Repositories.Repository;
using PemmexCommonLibs.Application.Helpers;
using PemmexCommonLibs.Application.Interfaces;
using PemmexCommonLibs.Infrastructure.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Notifications.API
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
            services.AddDbContext<NotificationContext>(options =>
               options.UseSqlServer(Configuration.GetConnectionString("NotificationConnection")));

            services.AddTransient<IDateTime, DateTimeService>();
            services.AddScoped<IApplicationDbContext>(provider => provider.GetService<NotificationContext>());

            services.AddControllers();
            services.AddMediatR(Assembly.GetExecutingAssembly());
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddHttpContextAccessor();

            services.AddScoped<Database.Repositories.Interface.INotification, NotificationRepository>();
            services.AddSingleton<IUserConnectionManager, UserConnectionManagerRepository>();

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
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Notification.API", Version = "v1" });
                c.SchemaFilter<EnumSchemaFilter>();

            });
            services.AddCors();
            services.AddSignalR();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Notification.API v1"));
            app.UseRouting();
            app.UseCors(cors => {
                cors.AllowAnyHeader();
                cors.AllowAnyMethod();
                cors.AllowCredentials();
                cors.WithOrigins(Configuration["AllowedChatOrigins"]);
            });

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHub<NotificationUserHubRepository>("/NotificationUserHub");
            });
        }
    }
}
