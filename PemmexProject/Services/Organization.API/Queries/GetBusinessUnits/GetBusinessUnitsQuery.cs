using AutoMapper;
using MediatR;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Organization.API.Database.Context;
using Organization.API.Database.Entities;
using Organization.API.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Organization.API.Queries.GetBusinessUnits
{
    public class GetBusinessUnitsQuery : IRequest<List<sp_GetBusinessUnitsDto>>
    {
        public string organizationIdentifier { get; set; }
    }
    public class GetBusinessUnitsQueryHandler : IRequestHandler<GetBusinessUnitsQuery, List<sp_GetBusinessUnitsDto>>
    {
        private readonly IApplicationDbContext _context;

        public GetBusinessUnitsQueryHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<sp_GetBusinessUnitsDto>> Handle(GetBusinessUnitsQuery request, CancellationToken cancellationToken)
        {
            try
            {
                string sql = "EXEC sp_GetBusinessUnits @OrganizationIdentifier";

                List<SqlParameter> parms = new List<SqlParameter>
                {
                   // Create parameter(s)    
                   new SqlParameter { ParameterName = "@OrganizationIdentifier", Value = request.organizationIdentifier }
                };
                return _context.sp_GetBusinessUnits.FromSqlRaw(sql, parms.ToArray()).ToList();
            }
            catch(Exception)
            {
                throw;
            }
        }
    }
}
