using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PemmexAPIAggregator.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PemmexAPIAggregator.Extensions
{
    public static class ServicesExtensions
    {
        public static void AddServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddHttpContextAccessor();
            services.AddHttpClient<IHolidayService, HolidayService>(c =>
            {
                c.BaseAddress = new Uri(configuration["ApiSettings:HolidaysUrl"]);
                var serviceProvider = services.BuildServiceProvider();
                // Find the HttpContextAccessor service
                var httpContextAccessor = serviceProvider.GetService<IHttpContextAccessor>();
                // Get the bearer token from the request context (header)
                var bearerToken = httpContextAccessor.HttpContext.Request
                                      .Headers["Authorization"]
                                      .FirstOrDefault(h => h.StartsWith("bearer ", StringComparison.InvariantCultureIgnoreCase));

                // Add authorization if found
                if (bearerToken != null)
                    c.DefaultRequestHeaders.Add("Authorization", bearerToken);
            });
            services.AddHttpClient<IOrganizationService, OrganizationService>(c => {

                c.BaseAddress = new Uri(configuration["ApiSettings:OrganizationUrl"]);
                var serviceProvider = services.BuildServiceProvider();
                // Find the HttpContextAccessor service
                var httpContextAccessor = serviceProvider.GetService<IHttpContextAccessor>();
                // Get the bearer token from the request context (header)
                var bearerToken = httpContextAccessor.HttpContext.Request
                                      .Headers["Authorization"]
                                      .FirstOrDefault(h => h.StartsWith("bearer ", StringComparison.InvariantCultureIgnoreCase));

                // Add authorization if found
                if (bearerToken != null)
                    c.DefaultRequestHeaders.Add("Authorization", bearerToken);

            });
            services.AddHttpClient<IAnnualSalaryPlanning, AnnualSalaryPlanning>(c => {

                c.BaseAddress = new Uri(configuration["ApiSettings:CompensationsUrl"]);
                var serviceProvider = services.BuildServiceProvider();
                // Find the HttpContextAccessor service
                var httpContextAccessor = serviceProvider.GetService<IHttpContextAccessor>();
                // Get the bearer token from the request context (header)
                var bearerToken = httpContextAccessor.HttpContext.Request
                                      .Headers["Authorization"]
                                      .FirstOrDefault(h => h.StartsWith("bearer ", StringComparison.InvariantCultureIgnoreCase));

                // Add authorization if found
                if (bearerToken != null)
                    c.DefaultRequestHeaders.Add("Authorization", bearerToken);
            });
            services.AddHttpClient<IPerformanceBonus, PerformanceBonus>(c => {

                c.BaseAddress = new Uri(configuration["ApiSettings:EmployeeTargetsUrl"]);
                var serviceProvider = services.BuildServiceProvider();
                // Find the HttpContextAccessor service
                var httpContextAccessor = serviceProvider.GetService<IHttpContextAccessor>();
                // Get the bearer token from the request context (header)
                var bearerToken = httpContextAccessor.HttpContext.Request
                                      .Headers["Authorization"]
                                      .FirstOrDefault(h => h.StartsWith("bearer ", StringComparison.InvariantCultureIgnoreCase));

                // Add authorization if found
                if (bearerToken != null)
                    c.DefaultRequestHeaders.Add("Authorization", bearerToken);
            });
            services.AddHttpClient<ICompensationService, CompensationService>(c =>
            {
                c.BaseAddress = new Uri(configuration["ApiSettings:CompensationsUrl"]);
                var serviceProvider = services.BuildServiceProvider();
                // Find the HttpContextAccessor service
                var httpContextAccessor = serviceProvider.GetService<IHttpContextAccessor>();
                // Get the bearer token from the request context (header)
                var bearerToken = httpContextAccessor.HttpContext.Request
                                      .Headers["Authorization"]
                                      .FirstOrDefault(h => h.StartsWith("bearer ", StringComparison.InvariantCultureIgnoreCase));

                // Add authorization if found
                if (bearerToken != null)
                    c.DefaultRequestHeaders.Add("Authorization", bearerToken);
            });
            services.AddHttpClient<INotificationService, NotificationService>(c =>
            {
                c.BaseAddress = new Uri(configuration["ApiSettings:NotificationUrl"]);
                var serviceProvider = services.BuildServiceProvider();
                // Find the HttpContextAccessor service
                var httpContextAccessor = serviceProvider.GetService<IHttpContextAccessor>();
                // Get the bearer token from the request context (header)
                var bearerToken = httpContextAccessor.HttpContext.Request
                                      .Headers["Authorization"]
                                      .FirstOrDefault(h => h.StartsWith("bearer ", StringComparison.InvariantCultureIgnoreCase));

                // Add authorization if found
                if (bearerToken != null)
                    c.DefaultRequestHeaders.Add("Authorization", bearerToken);
            });
            
            services.AddHttpClient<ITaskManagerService, TaskManagerService>(c =>
            {
                c.BaseAddress = new Uri(configuration["ApiSettings:TaskManagerUrl"]);
                var serviceProvider = services.BuildServiceProvider();
                // Find the HttpContextAccessor service
                var httpContextAccessor = serviceProvider.GetService<IHttpContextAccessor>();
                // Get the bearer token from the request context (header)
                var bearerToken = httpContextAccessor.HttpContext.Request
                                      .Headers["Authorization"]
                                      .FirstOrDefault(h => h.StartsWith("bearer ", StringComparison.InvariantCultureIgnoreCase));

                // Add authorization if found
                if (bearerToken != null)
                    c.DefaultRequestHeaders.Add("Authorization", bearerToken);
            });
            services.AddHttpClient<ISurveyService, SurveyService>(c =>
            {
                c.BaseAddress = new Uri(configuration["ApiSettings:SurveyUrl"]);
                var serviceProvider = services.BuildServiceProvider();
                // Find the HttpContextAccessor service
                var httpContextAccessor = serviceProvider.GetService<IHttpContextAccessor>();
                // Get the bearer token from the request context (header)
                var bearerToken = httpContextAccessor.HttpContext.Request
                                      .Headers["Authorization"]
                                      .FirstOrDefault(h => h.StartsWith("bearer ", StringComparison.InvariantCultureIgnoreCase));

                // Add authorization if found
                if (bearerToken != null)
                    c.DefaultRequestHeaders.Add("Authorization", bearerToken);
            });
        }
    }
}
