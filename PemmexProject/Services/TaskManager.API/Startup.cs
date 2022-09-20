using EventBus.Messages.Services;
using IdentityServer4.AccessTokenValidation;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.ServiceBus;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Net.Http.Headers;
using Microsoft.OpenApi.Models;
using PemmexCommonLibs.Application.Extensions;
using PemmexCommonLibs.Application.Helpers;
using PemmexCommonLibs.Application.Interfaces;
using PemmexCommonLibs.Infrastructure.Services;
using PemmexCommonLibs.Infrastructure.Services.FileUploadService;
using PemmexCommonLibs.Infrastructure.Services.LogService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Security.Principal;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using TaskManager.API.Database.context;
using TaskManager.API.Extensions;
using TaskManager.API.NotificationHub;

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
            var Identity = Configuration["IdentityUrl"];
            services.AddDbContext<TaskManagerContext>(options =>
               options.UseSqlServer(Configuration.GetConnectionString("TaskManagerConnection")));

            services.AddHttpContextAccessor();
            services.AddTransient<IPrincipal>(provider => provider.GetService<IHttpContextAccessor>().HttpContext.User);

            services.AddTransient<IDateTime, DateTimeService>();
            services.AddScoped<IApplicationDbContext>(provider => provider.GetService<TaskManagerContext>());

            services.AddControllers();
            services.AddMediatR(Assembly.GetExecutingAssembly());
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddHttpContextAccessor();

            services.AddScoped<INotificationRepository, NotificationRepository>();
            services.AddSingleton<IUserConnectionManager, UserConnectionManager>();

            services.AddSingleton<ITopicClient>(x => new TopicClient(Configuration.GetValue<string>("AppSettings:QueueConnectionString"),
              Configuration.GetValue<string>("AppSettings:TopicName")));
            services.AddSingleton<ITopicPublisher, TopicPublisher>();

            services.AddAuthentication(IdentityServerAuthenticationDefaults.AuthenticationScheme)
            .AddIdentityServerAuthentication(options =>
            {
                 // base-address of your identityserver
                options.Authority = Identity;
                 // name of the API resource
                options.ApiName = "TaskManager.API";

            });

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "TaskManager.API", Version = "v1" });
                c.SchemaFilter<EnumSchemaFilter>();
                c.CustomSchemaIds(x => x.FullName);
                c.AddSecurityDefinition(
                    "Bearer",
                    new OpenApiSecurityScheme
                    {
                        Type = SecuritySchemeType.Http,
                        BearerFormat = "JWT",
                        Scheme = "Bearer",
                        In = ParameterLocation.Header,
                        Name = HeaderNames.Authorization
                    }
                );
                c.AddSecurityRequirement(new OpenApiSecurityRequirement()
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            },
                            Scheme = "oauth2",
                            Name = "Bearer",
                            In = ParameterLocation.Header,

                        },
                        new List<string>()
                    }
                });
            });
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
            services.AddSignalR();
            services.AddPolicies();

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
                endpoints.MapHub<NotificationUserHub>("/NotificationUserHub");
            });
        }
    }
}
