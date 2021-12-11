using AutoMapper;
using MediatR;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Organization.API.Dtos;
using Organization.API.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Organization.API.Queries.GetSubordinates
{
    public class GetsubordinatesQuery : IRequest<List<spGetEmployeeTreeForManagerDto>>
    {
        public string Id { get; set; }
    }
    public class GetsubordinatesQueryHandler : IRequestHandler<GetsubordinatesQuery, List<spGetEmployeeTreeForManagerDto>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetsubordinatesQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<spGetEmployeeTreeForManagerDto>> Handle(GetsubordinatesQuery request, CancellationToken cancellationToken)
        {
            try
            {
                

                string sql = "EXEC sp_GetEmployeeTreeForManager @EmployeeIdentifier";

                List<SqlParameter> parms = new List<SqlParameter>
                {
                    // Create parameter(s)    
                    new SqlParameter { ParameterName = "@EmployeeIdentifier", Value = request.Id }
                };

                var o = _context.SpGetEmployeeTreeForManagerDtos.FromSqlRaw(sql, parms.ToArray()).ToList();
                return o;
            }
            catch (Exception e)
            {
                throw;
            }
        }
    }
}
