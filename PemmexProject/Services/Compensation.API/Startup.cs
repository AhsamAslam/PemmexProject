using Compensation.API.Database.context;
using Compensation.API.Database.Context;
using Compensation.API.Database.Interfaces;
using Compensation.API.Database.Repositories;
using Compensation.API.Extensions;
using Compensation.API.Services;
using EventBus.Messages.Services;
using IdentityServer4.AccessTokenValidation;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
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
using PemmexCommonLibs.Domain.Enums;
using PemmexCommonLibs.Infrastructure.Services;
using PemmexCommonLibs.Infrastructure.Services.FileUploadService;
using PemmexCommonLibs.Infrastructure.Services.LogService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Security.Principal;
using System.Threading.Tasks;

namespace Compensation.API
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
            services.AddDbContext<CompensationContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("CompensationConnection")));


            services.AddTransient<IDateTime, DateTimeService>();
            services.AddScoped<IApplicationDbContext>(provider => provider.GetService<CompensationContext>());
            services.AddScoped<ICompensationSalaryRepository, CompensationSalaryRepository>();
            services.AddScoped<IFunctionalBudgetRepository, FunctionalBudgetRepository>();
            services.AddScoped<IJobCatalogueRepository, JobCatalogueRepository>();
            services.AddControllers();
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddMediatR(Assembly.GetExecutingAssembly());
            services.AddHostedService<CompensationTopicReciever>();
            services.AddSingleton<ITopicClient>(x => new TopicClient(Configuration.GetValue<string>("AppSettings:QueueConnectionString"),
              Configuration.GetValue<string>("AppSettings:TopicName")));
            services.AddSingleton<ITopicPublisher, TopicPublisher>();

            services.AddHttpContextAccessor();
            services.AddTransient<IPrincipal>(provider => provider.GetService<IHttpContextAccessor>().HttpContext.User);

            services.AddSingleton<IFileUploadService>(x => new FileUploadService(new AzureContainerSettings
            {
                connectionString = Configuration.GetValue<string>("AzureStorage:ConnectionString"),
                containerName = Configuration.GetValue<string>("AzureStorage:ContainerName")
            }));
            services.AddSingleton<ISubscriptionClient>(x => new SubscriptionClient(
               Configuration.GetValue<string>("AppSettings:QueueConnectionString"),
               Configuration.GetValue<string>("AppSettings:TopicName"),
               Configuration.GetValue<string>("AppSettings:SubscriptionName")
               ));
            services.AddScoped<ILogService>(x => new LogService(new AzureContainerSettings
            {
                connectionString = Configuration.GetValue<string>("AzureStorage:ConnectionString"),
                containerName = Configuration.GetValue<string>("AzureStorage:LogContainerName")
            }));
            
            services.AddAuthentication(IdentityServerAuthenticationDefaults.AuthenticationScheme)
            .AddIdentityServerAuthentication(options =>
            {
                 // base-address of your identityserver
                options.Authority = Identity;
                 // name of the API resource
                options.ApiName = "Compensation.API";
            });

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Compensation.API", Version = "v1" });
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

            services.Configure<FormOptions>(options =>
            {
                options.MemoryBufferThreshold = Int32.MaxValue;
            });

            services.AddPolicies();
            services.AddFilters(Configuration);

            //services.AddSingleton<IAuthorizationHandler, PermissionHandler>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                
            }
            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Compensation.API v1"));

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
