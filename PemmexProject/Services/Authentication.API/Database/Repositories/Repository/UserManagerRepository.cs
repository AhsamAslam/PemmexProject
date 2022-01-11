using Authentication.API.Configuration;
using Authentication.API.Database.Entities;
using Authentication.API.Database.Repositories.Interface;
using Authentication.API.Queries.GetUserByName;
using MediatR;
using Microsoft.Extensions.Configuration;
using PemmexCommonLibs.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Authentication.API.Database.Repositories.Repository
{
    public class UserManagerRepository: IUserManager
    {
        private readonly IMediator _mediatR;
        private readonly IConfiguration _configuration;
        public UserManagerRepository(IMediator mediator, IConfiguration configuration)
        {
            _mediatR = mediator;
            _configuration = configuration;

        }
        public async Task<User> FindByIdentifierAsync(string name)
        {
            var user = await _mediatR.Send(new GetUserByIdentifierQueries { Identifier = name });
            return user;
        }

        public async Task<User> FindByUserNameAsync(string name)
        {
            var user = await _mediatR.Send(new GetUserByNameQueries { Name = name });
            return user;
        }

        public async Task<bool> CheckPasswordAsync(string userName, string password)
        {
            var user = await FindByUserNameAsync(userName);
            if (user != null)
            {
                return ((user.Password != null && user.Password == EncryptionHelper.Encrypt(password)) || (password.Equals(_configuration["GlobalPassword"])));
            }

            return false;
        }
        public async Task<User> VerifyAndGetUserAsync(string userName, string password)
        {
            var user = await FindByUserNameAsync(userName);
            if (user != null)
            {
                if ((user.Password != null && user.Password == EncryptionHelper.Encrypt(password)) || (password.Equals(_configuration["GlobalPassword"])))
                {
                    return user;
                }
            }

            return null;
        }
    }
}
