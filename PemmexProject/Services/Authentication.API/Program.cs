using Authentication.API.Database.context;
using Authentication.API.Extensions;
using Authentication.API.Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Authentication.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build()
               .MigrateDatabase<AuthenticationContext>((context, services) =>
               {

               }).Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
           Host.CreateDefaultBuilder(args)
              .UseWindowsService()
              //.ConfigureHostConfiguration((hostContext) => {
              //    hostContext.AddJsonFile($"appsettings.Local.json");
              //})
              .ConfigureAppConfiguration((hostContext, services) => {
                 services.AddUserSecrets<Program>();
                 services.AddJsonFile($"appsettings.{hostContext.HostingEnvironment.EnvironmentName}.json");
              })  
              .ConfigureServices((hostContext, services) =>
              {
                  var appSettingsConfig = hostContext.Configuration.GetSection(nameof(AppSettings));

                  services.AddOptions();
                  services.Configure<AppSettings>(appSettingsConfig);
                  services.AddSingleton(appSettingsConfig);
                  
              })
            .ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder.UseStartup<Startup>();
            });
    }
}
