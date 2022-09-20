using Survey.API.Filters;

namespace Survey.API.Extensions
{
    public static class FiltersExtension
    {
        public static void AddFilters(this IServiceCollection services, IConfiguration Configuration)
        {
            services.AddScoped<AuthorizationIdentifierFilter>();
        }
    }
}
