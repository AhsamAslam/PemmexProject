using AutoMapper;
using Compensation.API.Database.context;
using Compensation.API.Database.Interfaces;
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
        public string[] employeeIdentifiers { get; set; }
    }

    public class GetTeamBonusesQueryHandeler : IRequestHandler<GetTeamBonusesQuery, List<UserBonus>>
    {
        private readonly ICompensationSalaryRepository _context;
        private readonly IMapper _mapper;

        public GetTeamBonusesQueryHandeler(ICompensationSalaryRepository context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<List<UserBonus>> Handle(GetTeamBonusesQuery request, CancellationToken cancellationToken)
        {
            try
            {
                List<UserBonus> userBonuses = new List<UserBonus>();
                var compensationSalaries = await _context.GetTeamBonus(request.employeeIdentifiers);
                foreach(var c in compensationSalaries)
                {
                    UserBonus b = new UserBonus()
                    {
                        bonusAmount = c.one_time_bonus,
                        EmployeeIdentifier = c.EmployeeIdentifier
                    };
                    userBonuses.Add(b);
                }
                return userBonuses;
            }
            catch(Exception)
            {
                throw;
            }
        }
    }
}
