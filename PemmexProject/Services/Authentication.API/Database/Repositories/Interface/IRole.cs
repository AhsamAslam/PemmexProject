using Authentication.API.Database.Entities;
using Authentication.API.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Authentication.API.Database.Repositories.Interface
{
    public interface IRole
    {
        Task<RoleDto> SaveRole(RoleDto Role);
    }
}
