using Authentication.API.Configuration;
using Authentication.API.Database.Repositories.Interface;
using Authentication.API.Database.Repositories.Repository;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Authentication.API.Extensions
{
    public static class CustomIdentityServerBuilderExtensions
    {
        public static IIdentityServerBuilder AddCustomUserStore(this IIdentityServerBuilder builder)
        {
            builder.Services.AddSingleton<IUserManager, UserManagerRepository>();
            builder.AddProfileService<ProfileService>();
            builder.AddResourceOwnerValidator<ResourceOwnerPasswordValidator>();

            return builder;
        }
    }
}
