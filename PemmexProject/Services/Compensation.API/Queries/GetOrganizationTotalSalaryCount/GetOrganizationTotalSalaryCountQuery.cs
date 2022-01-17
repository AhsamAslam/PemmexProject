﻿using AutoMapper;
using Compensation.API.Database.context;
using Compensation.API.Database.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Compensation.API.Queries.GetOrganizationTotalSalaryCount
{
    public class GetOrganizationTotalSalaryCountQuery : IRequest<double>
    {
        public string organizationIdentifier { get; set; }
    }

    public class GetOrganizationTotalSalaryCountQueryHandeler : IRequestHandler<GetOrganizationTotalSalaryCountQuery, double>
    {
        private ICompensationSalaryRepository _compensation;
        private readonly IMapper _mapper;

        public GetOrganizationTotalSalaryCountQueryHandeler(ICompensationSalaryRepository compensation, IMapper mapper)
        {
            _compensation = compensation;
            _mapper = mapper;
        }
        public async Task<double> Handle(GetOrganizationTotalSalaryCountQuery request, CancellationToken cancellationToken)
        {
            try
            {
                //var salary = await _context.Compensation
                //.Where(c => c.organizationIdentifier == request.organizationIdentifier)
                //.Select(o => o.TotalMonthlyPay)
                //.ToListAsync();
                var salary = await _compensation.GetCompensationTotalAmountByOrganizationIdentifier(request.organizationIdentifier);

                return salary.Sum(x => x.TotalMonthlyPay);
            }
            catch (Exception)
            {

                throw;
            }
            
        }
    }
}
