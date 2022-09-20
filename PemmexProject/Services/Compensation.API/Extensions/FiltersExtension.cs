using Compensation.API.Filters;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Compensation.API.Extensions
{
    public static class FiltersExtension
    {
        public static void AddFilters(this IServiceCollection services,IConfiguration Configuration)
        {
            services.AddScoped<AuthorizationIdentifierFilter>();
        }
    }
}
