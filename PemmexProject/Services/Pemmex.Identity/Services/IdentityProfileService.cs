using AutoMapper;
using IdentityModel;
using IdentityServer4.Extensions;
using IdentityServer4.Models;
using IdentityServer4.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Pemmex.Identity.Data;
using Pemmex.Identity.Dtos;
using Pemmex.Identity.Helpers;
using Pemmex.Identity.Models;
using PemmexCommonLibs.Application.Helpers;
using PemmexCommonLibs.Domain.Common.Dtos;
using PemmexCommonLibs.Domain.Enums;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Pemmex.Identity.Services
{
    public class IdentityProfileService : IProfileService
    {

        private readonly IUserClaimsPrincipalFactory<ApplicationUser> _claimsFactory;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<ApplicationRole> _roleManager;
        private readonly IMapper _mapper;
        private readonly IServiceProvider _serviceProvider;
        private readonly IConfiguration _configuration;
        private ConnectionMultiplexer _Connection = null;
        public IdentityProfileService(IUserClaimsPrincipalFactory<ApplicationUser> claimsFactory,
            UserManager<ApplicationUser> userManager,IMapper mapper,
            RoleManager<ApplicationRole> roleManager,
            IServiceProvider serviceProvider, IConfiguration configuration)
        {
            _claimsFactory = claimsFactory;
            _userManager = userManager;
            _roleManager = roleManager;
            _mapper = mapper;
            _serviceProvider = serviceProvider;
            _configuration = configuration;           
        }
        public async Task GetProfileDataAsync(ProfileDataRequestContext context)
        {
            try
            {
                Console.WriteLine("Line No : 55");
                if (_Connection == null || !_Connection.IsConnected)
                {
                    _Connection = await ConnectionMultiplexer.ConnectAsync(_configuration["RedisConnectionStrings"]);
                }
                Console.WriteLine("Line No : 60");
                var claims = new List<Claim>();
                var Database = _Connection.GetDatabase();
                Console.WriteLine("Line No : 63");
                var sub = context.Subject.GetSubjectId();
                Console.WriteLine("Line No : 65");
                var user = await _userManager.FindByIdAsync(sub);
                Console.WriteLine("Line No : 67");
                var manager = await _userManager.Users
                    .Where(u => u.EmployeeIdentifier == user.ManagerIdentifier).FirstOrDefaultAsync();
                if (manager != null)
                {
                    user.ManagerName = manager.FirstName + " " + manager.LastName;
                }
                Console.WriteLine("Line No : 73");
                var dto = _mapper.Map<UserDto>(user);
                if (_userManager.SupportsUserRole)
                {
                    var roles = await _userManager.GetRolesAsync(user);
                    dto.Role.AddRange(roles);
                    foreach (var r in roles)
                    {
                        claims.Add(new Claim(JwtClaimTypes.Role, r));
                        if (_roleManager.SupportsRoleClaims)
                        {
                            ApplicationRole identityRole = await _roleManager.FindByNameAsync(r);
                            if (identityRole != null)
                            {
                                claims.AddRange(await _roleManager.GetClaimsAsync(identityRole));
                            }
                        }
                    }
                    using (var scope = _serviceProvider.CreateScope())
                    {
                        var _context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                        var o = await _context.Users.FromSqlRaw($"sp_GetAllManagerTree {user.EmployeeIdentifier}").ToListAsync();
                        var managers = _mapper.Map<List<ApplicationUser>, List<UserDto>>(o);
                        foreach (var m in managers)
                        {
                            dto.ManagerHeirarchy.Add(m.ManagerIdentifier);
                        }
                        if (roles.Contains(Enum.GetName(Roles.buhr)))
                        {
                            var b_units = await _context.Users.Where(u => u.Id == user.Id)
                                .Include(b => b.UserBusinessUnits).FirstOrDefaultAsync();
                            //var b_units = user.UserBusinessUnits.ToList();
                            List<RedisTeamDto> team = new List<RedisTeamDto>();
                            foreach (var b in b_units.UserBusinessUnits)
                            {
                                var jsonEmployees = await Database.StringGetAsync("BUnit_" + user.EmployeeIdentifier).ConfigureAwait(false);
                                var value = string.IsNullOrWhiteSpace(jsonEmployees)
                                ? default(List<RedisTeamDto>)
                                : JsonConvert.DeserializeObject<List<RedisTeamDto>>(jsonEmployees);
                                if (value == null)
                                {
                                    var unit = await _userManager.Users.FirstOrDefaultAsync(u => u.CostCenterIdentifier == b.BUnitIdentifier);
                                    var employees = await _context.Users.FromSqlRaw($"sp_GetEmployeeTreeForManager {unit.EmployeeIdentifier}").ToListAsync();
                                    foreach (var e in employees)
                                    {
                                        team.Add(new RedisTeamDto()
                                        {
                                            EmployeeIdentifier = e.EmployeeIdentifier,
                                            ManagerIdentifier = e.ManagerIdentifier
                                        });
                                    }
                                }
                            }
                            if (team.Count > 0)
                            {
                                await Database.StringSetAsync("BUnit_" + user.EmployeeIdentifier, JsonConvert.SerializeObject(team)).ConfigureAwait(false);
                                await Database.KeyExpireAsync("Team_" + user.EmployeeIdentifier, TimeSpan.FromMinutes(1440)).ConfigureAwait(false);
                            }

                        }
                        else if (roles.Contains(Enum.GetName(Roles.manager)))
                        {

                            var jsonEmployees = await Database.StringGetAsync("Team_" + user.EmployeeIdentifier).ConfigureAwait(false);
                            var value = string.IsNullOrWhiteSpace(jsonEmployees)
                            ? default(List<RedisTeamDto>)
                            : JsonConvert.DeserializeObject<List<RedisTeamDto>>(jsonEmployees);
                            if (value == null)
                            {
                                var employees = await _context.Users.FromSqlRaw($"sp_GetEmployeeTreeForManager {user.EmployeeIdentifier}").ToListAsync();
                                List<RedisTeamDto> team = new List<RedisTeamDto>();
                                foreach (var e in employees)
                                {
                                    team.Add(new RedisTeamDto()
                                    {
                                        EmployeeIdentifier = e.EmployeeIdentifier,
                                        ManagerIdentifier = e.ManagerIdentifier
                                    });
                                }
                                if (team.Count > 0)
                                {
                                    await Database.StringSetAsync("Team_" + user.EmployeeIdentifier, JsonConvert.SerializeObject(team)).ConfigureAwait(false);
                                    await Database.KeyExpireAsync("Team_" + user.EmployeeIdentifier, TimeSpan.FromMinutes(1440)).ConfigureAwait(false);
                                }
                            }
                        }

                    }
                }

                claims.Add(new Claim("UserObject", JsonConvert.SerializeObject(dto)));
                context.IssuedClaims = claims;
            }
            catch(Exception e)
            {
                throw e;
            }

        }
        public async Task IsActiveAsync(IsActiveContext context)
        {
            var sub = context.Subject.GetSubjectId();
            var user = await _userManager.FindByIdAsync(sub);
            context.IsActive = user != null;
        }
    }
}
