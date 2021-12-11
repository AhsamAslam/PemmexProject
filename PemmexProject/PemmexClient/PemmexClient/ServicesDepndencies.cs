using Microsoft.Extensions.DependencyInjection;
using PemmexClient.APIServices;
using PemmexClient.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PemmexClient
{
    public static class ServicesDepndencies
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddScoped<IEmployeeService,EmployeeService>();
            services.AddScoped<IOrganizationService, OrganizationService>();

            return services;
        }
    }
}
