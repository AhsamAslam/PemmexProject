using Compensation.API.Database.Entities;
using Compensation.API.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Compensation.API.Database.Repositories.Interface
{
    public interface IBonus
    {
        Task<IEnumerable<CompensationSalaries>> GetCompensationSalariesByEmployeeIdentifier(string EmployeeIdentifier);
        Task<IEnumerable<UserBonus>> GetUserBonusByEmployeeIdentifier(List<string> EmployeeIdentifier);
        Task<IEnumerable<UserBonus>> GetUserBonusByOrganizationIdentifiers(string OrganizationIdentifiers);

    }
}
