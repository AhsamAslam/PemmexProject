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

namespace Compensation.API.Queries.GetUserBonuses
{
    public class GetUserBonusesQuery : IRequest<List<UserBonus>>
    {
        public string employeeIdentifier { get; set; }
    }

    public class GetUserBonusesQueryHandeler : IRequestHandler<GetUserBonusesQuery, List<UserBonus>>
    {
        private readonly IBonus _bonus;
        private readonly IMapper _mapper;

        public GetUserBonusesQueryHandeler(IBonus bonus, IMapper mapper)
        {
            _bonus = bonus;
            _mapper = mapper;
        }
        public async Task<List<UserBonus>> Handle(GetUserBonusesQuery request, CancellationToken cancellationToken)
        {
            List<UserBonus> userBonuses = new List<UserBonus>();
            //var salary = await _context.CompensationSalaries
            //    .Where(e => e.EmployeeIdentifier == request.employeeIdentifier)
            //    .Where(c => (c.IssuedDate.Year == DateTime.Now.Year))
            //    .ToListAsync(cancellationToken);
            var salary = await _bonus.GetCompensationSalariesByEmployeeIdentifier(request.employeeIdentifier);
            foreach(var s in salary)
            {
                userBonuses.Add(new UserBonus()
                {
                    bonusAmount = s.one_time_bonus,
                    bonusDateTime = s.IssuedDate,
                    EmployeeIdentifier = s.EmployeeIdentifier
                });
            }

            return userBonuses;
        }
    }
}
