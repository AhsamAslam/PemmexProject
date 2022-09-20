using IdentityServer4.EntityFramework.DbContexts;
using IdentityServer4.EntityFramework.Mappers;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Pemmex.Identity.Models;
using PemmexCommonLibs.Application.Helpers;
using PemmexCommonLibs.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pemmex.Identity.Data
{
    public static class DatabaseInitializer
    {
        public static void PopulateIdentityServer(IApplicationBuilder app)
        {
            using var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope();
            serviceScope.ServiceProvider.GetRequiredService<PersistedGrantDbContext>().Database.Migrate();

            var context = serviceScope.ServiceProvider.GetRequiredService<ConfigurationDbContext>();

            context.Database.Migrate();

            foreach (var client in Config.Clients)
            {
                var item = context.Clients.SingleOrDefault(c => c.ClientId == client.ClientId);

                if (item == null)
                {
                    context.Clients.Add(client.ToEntity());
                    context.SaveChanges();
                }
            }

            foreach (var resource in Config.ApiResources)
            {
                var item = context.ApiResources.SingleOrDefault(c => c.Name == resource.Name);

                if (item == null)
                {
                    context.ApiResources.Add(resource.ToEntity());
                }
            }
            foreach (var resource in Config.IdentityResources)
            {
                var item = context.IdentityResources.SingleOrDefault(c => c.Name == resource.Name);

                if (item == null)
                {
                    context.IdentityResources.Add(resource.ToEntity());
                }
            }

            foreach (var scope in Config.ApiScopes)
            {
                var item = context.ApiScopes.SingleOrDefault(c => c.Name == scope.Name);

                if (item == null)
                {
                    context.ApiScopes.Add(scope.ToEntity());
                }
            }
            context.SaveChanges();
            var _roleManager = serviceScope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            var task = Task.Run(async () =>
            {
                var role = await _roleManager.Roles.Where(r => r.Name == Enum.GetName(Roles.user)).FirstOrDefaultAsync();
                if (role == null)
                {
                    var user = new ApplicationRole { Name = Enum.GetName(Roles.user), NormalizedName = Enum.GetName(Roles.user) };
                    await _roleManager.Roles.AddAsync(user);
                    var manager = new ApplicationRole { Name = Enum.GetName(Roles.manager), NormalizedName = Enum.GetName(Roles.manager) };
                    await _roleManager.AddAsync(manager);
                    var admin = new ApplicationRole { Name = Enum.GetName(Roles.admin), NormalizedName = Enum.GetName(Roles.admin) };
                    await _roleManager.AddAsync(admin);
                    var grouphr = new ApplicationRole { Name = Enum.GetName(Roles.grouphr), NormalizedName = Enum.GetName(Roles.grouphr) };
                    await _roleManager.AddAsync(grouphr);
                    var buhr = new ApplicationRole { Name = Enum.GetName(Roles.buhr), NormalizedName = Enum.GetName(Roles.buhr) };
                    await _roleManager.AddAsync(buhr);
                    await _roleManager.SaveChangesAsync();
                }
                var password = new PasswordHasher<ApplicationUser>();
                

                var SuperAdmin = new ApplicationUser
                {
                    FirstName = "super",
                    LastName = "admin",
                    Email = "superadmin@gmail.com",
                    UserName = "superadmin",
                    EmailConfirmed = true,
                    NormalizedUserName = "superadmin",
                    NormalizedEmail = "superadmin@gmail.com"
                };
                var hashed = password.HashPassword(SuperAdmin, "secret");
                SuperAdmin.PasswordHash = hashed;
                if (!_roleManager.Users.Any(u => u.UserName == SuperAdmin.UserName))
                {
                    await _roleManager.AddAsync(SuperAdmin);
                    await _roleManager.SaveChangesAsync();
                }

                if (_roleManager.Users.Any(u => u.UserName == SuperAdmin.UserName))
                {
                    var rol = await _roleManager.Roles.Where(r => r.Name == Enum.GetName(Roles.admin)).FirstOrDefaultAsync();
                    SuperAdmin.UserRoles.Add(new ApplicationUserRole()
                    {
                        Role = rol
                    });
                    
                    await _roleManager.SaveChangesAsync();

                }
            });
            task.GetAwaiter().GetResult();
        }
    }
}
