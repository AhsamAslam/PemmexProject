using Authentication.API.Configuration;
using Authentication.API.Database.context;
using Authentication.API.Database.Repositories.Interface;
using Authentication.API.Database.Repositories.Repository;
using Authentication.API.Extensions;
using Authentication.API.Services;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Azure.ServiceBus;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using PemmexCommonLibs.Application.Helpers;
using PemmexCommonLibs.Application.Interfaces;
using PemmexCommonLibs.Infrastructure.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Authentication.API
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddDbContext<AuthenticationContext>(options =>
                       options.UseSqlServer(Configuration.GetConnectionString("AuthenticationConnection")));
            services.AddTransient<IDateTime, DateTimeService>();
            services.AddScoped<IApplicationDbContext>(provider => provider.GetService<AuthenticationContext>());
            services.AddScoped<IUser, UserRepository>();
            services.AddScoped<IRole, RoleRepository>();

            services.AddControllersWithViews();
            services.AddIdentityServer()
                 .AddDeveloperSigningCredential()        //This is for dev only scenarios when you don’t have a certificate to use.
                 .AddInMemoryApiScopes(Config.ApiScopes)
                 .AddInMemoryClients(Config.Clients)
                 .AddInMemoryIdentityResources(Config.IdentityResources)
                 .AddCustomUserStore();

            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddMediatR(Assembly.GetExecutingAssembly());
            services.AddHostedService<UserTopicReciever>();

            services.AddSingleton<ISubscriptionClient>(x => new SubscriptionClient(
                Configuration.GetValue<string>("AppSettings:QueueConnectionString"),
                Configuration.GetValue<string>("AppSettings:TopicName"),
                Configuration.GetValue<string>("AppSettings:SubscriptionName")
                ));
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Authentication.API", Version = "v1" });
                c.SchemaFilter<EnumSchemaFilter>();

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

            //services.AddCors(options =>
            //{
            //    options.AddPolicy("CorsApi",
            //        builder => builder.WithOrigins(Configuration.GetSection("AllowedChatOrigins").Value)
            //    .AllowAnyHeader()
            //    .AllowAnyMethod());
            //});


        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Authentication.API v1"));
            app.UseStaticFiles();
            app.UseRouting();
            app.UseIdentityServer();
            app.UseAuthentication();
            app.UseAuthorization();
            
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });

        }
    }
}
