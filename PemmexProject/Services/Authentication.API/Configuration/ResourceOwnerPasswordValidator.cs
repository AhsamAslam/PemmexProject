using Authentication.API.Database.Repositories.Interface;
using IdentityModel;
using IdentityServer4.Models;
using IdentityServer4.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Authentication.API.Configuration
{
    public class ResourceOwnerPasswordValidator : IResourceOwnerPasswordValidator
    {
        private readonly IUserManager _userManager;
        public ResourceOwnerPasswordValidator(IUserManager userManager)
        {
            _userManager = userManager;
        }
        public async Task ValidateAsync(ResourceOwnerPasswordValidationContext context)
        {

            if (await _userManager.CheckPasswordAsync(context.UserName, context.Password))
            {
                var user = await _userManager.FindByUserNameAsync(context.UserName);
                context.Result = new GrantValidationResult(user.EmployeeIdentifier, OidcConstants.AuthenticationMethods.Password);
            }

            await Task.CompletedTask;
        }

    }
}
