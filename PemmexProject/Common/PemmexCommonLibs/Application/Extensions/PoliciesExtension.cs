using Microsoft.Extensions.DependencyInjection;
using PemmexCommonLibs.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PemmexCommonLibs.Application.Extensions
{
    public static class PoliciesExtension
    {
        public static void AddPolicies(this IServiceCollection services)
        {
            services.AddAuthorization(options =>
            {
                options.AddPolicy("User", policy =>
                policy.RequireClaim("role", Enum.GetName(Roles.user), Enum.GetName(Roles.admin),
                Enum.GetName(Roles.buhr), Enum.GetName(Roles.grouphr),
                Enum.GetName(Roles.manager)));

                options.AddPolicy("Manager", policy =>
                policy.RequireClaim("role",Enum.GetName(Roles.admin),
                Enum.GetName(Roles.buhr), Enum.GetName(Roles.grouphr),
                Enum.GetName(Roles.manager)));

                options.AddPolicy("BuHR", policy =>
                policy.RequireClaim("role", Enum.GetName(Roles.admin),
                Enum.GetName(Roles.buhr), Enum.GetName(Roles.grouphr)));

                options.AddPolicy("GroupHR", policy =>
                policy.RequireClaim("role", Enum.GetName(Roles.admin)
                , Enum.GetName(Roles.grouphr)));
            });
        }
    }
}
