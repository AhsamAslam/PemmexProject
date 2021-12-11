using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.EntityFrameworkCore;
using Organization.API.Interfaces;
using PemmexCommonLibs.Application.Interfaces;
using PemmexCommonLibs.Infrastructure.Services;
using Microsoft.Azure.ServiceBus;
using EventBus.Messages.Services;
using Microsoft.IdentityModel.Tokens;
using Organization.API.Services;
using PemmexCommonLibs.Infrastructure.Services.FileUploadService;
using PemmexCommonLibs.Application.Helpers;
using PemmexCommonLibs.Infrastructure.Services.LogService;

namespace Organization.API
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
            services.AddDbContext<PemmexOrganizationContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("OrganizationConnection")));

            services.AddTransient<IDateTime, DateTimeService>();
            services.AddScoped<IApplicationDbContext>(provider => provider.GetService<PemmexOrganizationContext>());
            services.AddControllers();
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddMediatR(Assembly.GetExecutingAssembly());
            services.AddSingleton<ITopicClient>(x => new TopicClient(
                Configuration.GetValue<string>("ServiceBus:PemmexConnectionString"),
                Configuration.GetValue<string>("ServiceBus:TopicName")
                ));
            services.AddSingleton<ITopicPublisher, TopicPublisher>();

            services.AddSingleton<ISubscriptionClient>(x => new SubscriptionClient(
               Configuration.GetValue<string>("ServiceBus:PemmexConnectionString"),
               Configuration.GetValue<string>("ServiceBus:TopicName"),
               Configuration.GetValue<string>("ServiceBus:SubscriptionName")
               ));

            services.AddSingleton<IFileUploadService>(x => new FileUploadService(new AzureContainerSettings { 
                connectionString = Configuration.GetValue<string>("AzureStorage:ConnectionString"),
                containerName = Configuration.GetValue<string>("AzureStorage:ContainerName")
            }));

            services.AddScoped<ILogService>(x => new LogService(new AzureContainerSettings
            {
                connectionString = Configuration.GetValue<string>("AzureStorage:ConnectionString"),
                containerName = Configuration.GetValue<string>("AzureStorage:LogContainerName")
            }));

            //services.AddHostedService<CompensationTopicReciever>();
            //services.AddHostedService<HolidayTopicReciever>();
            //services.AddHostedService<GradeTopicReciever>();
            //services.AddHostedService<ManagerTopicReciever>();
            //services.AddHostedService<TitleTopicReciever>();

            services.AddHostedService<TaskTopicReciever>();
           

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Organization.API", Version = "v1" });
                c.SchemaFilter<EnumSchemaFilter>();
            });
            services.Configure<FormOptions>(options =>
            {
                options.MemoryBufferThreshold = Int32.MaxValue;
            });

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
                options.AddPolicy("ClientIdPolicy", policy => policy.RequireClaim("client_id", "pemmex_mvc_client","pemmexclient"));
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
            }
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Organization.API v1"));

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
