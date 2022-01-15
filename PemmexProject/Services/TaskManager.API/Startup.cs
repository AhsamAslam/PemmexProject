using EventBus.Messages.Services;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
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
using PemmexCommonLibs.Infrastructure.Services.LogService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using TaskManager.API.Database.context;
using TaskManager.API.Database.Repositories.Interface;
using TaskManager.API.Database.Repositories.Repository;

namespace TaskManager.API
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
            services.AddDbContext<TaskManagerContext>(options =>
               options.UseSqlServer(Configuration.GetConnectionString("TaskManagerConnection")));

            services.AddTransient<IDateTime, DateTimeService>();
            services.AddScoped<IApplicationDbContext>(provider => provider.GetService<TaskManagerContext>());
            services.AddScoped<IApprovalSettings, ApprovalSettingsRepository>();
            services.AddScoped<IBonusSettings, BonusSettingsRepository>();

            services.AddControllers();
            services.AddMediatR(Assembly.GetExecutingAssembly());
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddHttpContextAccessor();

            services.AddSingleton<ITopicClient>(x => new TopicClient(Configuration.GetValue<string>("ServiceBus:PemmexConnectionString"),
              Configuration.GetValue<string>("ServiceBus:TopicName")));
            services.AddSingleton<ITopicPublisher, TopicPublisher>();

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
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "TaskManager.API", Version = "v1" });
                c.SchemaFilter<EnumSchemaFilter>();

            });

            //services.AddSingleton<INotificationUserHub, UserConnectionManager>();
            services.AddSingleton<IFileUploadService>(x => new FileUploadService(new AzureContainerSettings
            {
                connectionString = Configuration.GetValue<string>("AzureStorage:ConnectionString"),
                containerName = Configuration.GetValue<string>("AzureStorage:HolidayDocumentsContainerName")
            }));
            services.AddScoped<ILogService>(x => new LogService(new AzureContainerSettings
            {
                connectionString = Configuration.GetValue<string>("AzureStorage:ConnectionString"),
                containerName = Configuration.GetValue<string>("AzureStorage:LogContainerName")
            }));
            services.AddCors();
            

            //services.AddCors(options =>
            //{
            //    options.AddPolicy("CorsApi",
            //        builder => builder.WithOrigins(Configuration["AllowedChatOrigins"])
            //    .AllowAnyHeader()
            //    .AllowAnyMethod());
            //});
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
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "TaskManager.API v1"));
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
            });
        }
    }
}
