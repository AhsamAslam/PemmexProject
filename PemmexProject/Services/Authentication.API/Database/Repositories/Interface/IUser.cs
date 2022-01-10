using Authentication.API.Database.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Authentication.API.Database.Repositories.Interface
{
    public interface IUser
    {
        Task<User> UpdateUser(User User);
        Task<User> GetUserById(Guid Id);

        Task<IEnumerable<User>> GetAllUsers();
    }
}
