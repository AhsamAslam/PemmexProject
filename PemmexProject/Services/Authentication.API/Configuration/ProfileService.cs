using Authentication.API.Database.Entities;
using Authentication.API.Database.Repositories.Interface;
using AutoMapper;
using IdentityModel;
using IdentityServer4.Extensions;
using IdentityServer4.Models;
using IdentityServer4.Services;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Authentication.API.Configuration
{
    public class ProfileService:IProfileService
    {
        private IUserManager _userManager;
        private IMapper _mapper;
        public ProfileService(IUserManager userManager,IMapper mapper)
        {
            _userManager = userManager;
            _mapper = mapper;
        }
        public async Task GetProfileDataAsync(ProfileDataRequestContext context)
        {
              var sub = context.Subject.GetSubjectId();
              var user = await _userManager.FindByIdentifierAsync(context.Subject.GetSubjectId());

            var claims = new List<Claim>
              {
                new Claim("UserObject", JsonConvert.SerializeObject(user))
              };

              context.IssuedClaims = claims;
            
        }

        public async Task IsActiveAsync(IsActiveContext context)
        {
            var sub = context.Subject.GetSubjectId();
            var user = await _userManager.FindByIdentifierAsync(context.Subject.GetSubjectId());
            context.IsActive = user != null;
        }
        //private async Task<ClaimsPrincipal> GetClaims(UserDto user)
        //{
        //    if(user == null)
        //    {
        //        throw new ArgumentNullException(nameof(user));
        //    }
        //    var id = new ClaimsIdentity();
        //    id.AddClaim(new Claim(JwtClaimTypes.PreferredUserName, user.UserName));
        //    id.AddClaims(await _userManager.GetClaimsAsync(user));
        //    return new ClaimsPrincipal(id);
        //}
    }
}
