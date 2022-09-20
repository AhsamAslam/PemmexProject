using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PemmexAPIAggregator.Filters;

namespace PemmexAPIAggregator.Extensions
{
    public static class FiltersExtension
    {
        public static void AddFilters(this IServiceCollection services,IConfiguration Configuration)
        {
            services.AddScoped<AuthorizationIdentifierFilter>();
        }
    }
}
