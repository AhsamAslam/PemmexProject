using Authentication.API.Database.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Authentication.API.Configuration
{
    public interface IUserManager
    {
        public Task<User> FindByIdentifierAsync(string name);
        public Task<User> FindByUserNameAsync(string name);
        public Task<bool> CheckPasswordAsync(string userName, string password);
        public Task<User> VerifyAndGetUserAsync(string userName, string password);

    }
}
