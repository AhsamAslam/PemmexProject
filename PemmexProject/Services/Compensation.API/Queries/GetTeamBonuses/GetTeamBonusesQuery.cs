using AutoMapper;
using Compensation.API.Database.context;
using Compensation.API.Database.Repositories.Interface;
using Compensation.API.Dtos;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Compensation.API.Queries.GetTeamBonuses
{
    public class GetTeamBonusesQuery : IRequest<List<UserBonus>>
    {
        public List<string> employeeIdentifiers { get; set; }
    }

    public class GetTeamBonusesQueryHandeler : IRequestHandler<GetTeamBonusesQuery, List<UserBonus>>
    {
        private readonly IBonus _bonus;
        private readonly IMapper _mapper;

        public GetTeamBonusesQueryHandeler(IBonus bonus, IMapper mapper)
        {
            _bonus = bonus;
            _mapper = mapper;
        }
        public async Task<List<UserBonus>> Handle(GetTeamBonusesQuery request, CancellationToken cancellationToken)
        {
            try
            {
                List<UserBonus> userBonuses = new List<UserBonus>();
                //var salary = await _context.CompensationSalaries
                //    .Where(e => request.employeeIdentifiers.Contains(e.EmployeeIdentifier))
                //    .GroupBy(c => c.EmployeeIdentifier)
                //    .Select(cl => new UserBonus
                //     {
                //         EmployeeIdentifier = cl.First().EmployeeIdentifier,
                //         bonusAmount = cl.Sum(c => c.one_time_bonus),
                //     })
                //    .ToListAsync(cancellationToken);
                var salary = await _bonus.GetUserBonusByEmployeeIdentifier(request.employeeIdentifiers);
                userBonuses = salary.ToList();

                return userBonuses;
            }
            catch (Exception)
            {

                throw;
            }
            
        }
    }
}
