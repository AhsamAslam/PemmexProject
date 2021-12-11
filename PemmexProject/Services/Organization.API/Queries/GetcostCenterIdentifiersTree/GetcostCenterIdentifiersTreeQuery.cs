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

namespace Organization.API.Queries.GetcostCenterIdentifiersTree
{
    public class GetcostCenterIdentifiersTreeQuery : IRequest<List<CostCenterResponse>>
    {
        public string costCenterIdentifier { get; set; }
    }
    public class GetcostCenterIdentifiersTreeQueryHandler : IRequestHandler<GetcostCenterIdentifiersTreeQuery, List<CostCenterResponse>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetcostCenterIdentifiersTreeQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<CostCenterResponse>> Handle(GetcostCenterIdentifiersTreeQuery request, CancellationToken cancellationToken)
        {

            string sql = "EXEC sp_GetCostCentersTree @costCenterIdentifier";

            List<SqlParameter> parms = new List<SqlParameter>
                {
                    // Create parameter(s)    
                    new SqlParameter { ParameterName = "@costCenterIdentifier", Value = request.costCenterIdentifier }
                };

            var o = _context.CostCenters.FromSqlRaw(sql, parms.ToArray()).ToList();

            //var o = await _context.CostCenters
            //    .Where(o => o.CostCenterIdentifier == request.organizationIdentifier)
            //    .ToListAsync(cancellationToken: cancellationToken);

            var e_response =  _mapper.Map<List<Entities.CostCenter>, List<CostCenterResponse>>(o);
            var recursiveData = FillRecursive(e_response, "");
            return recursiveData;

        }
        private static List<CostCenterResponse> FillRecursive(List<CostCenterResponse> employeeVms, string parentId)
        {
            return employeeVms.Where(x => x.ParentCostCenterIdentifier == parentId).Select(item => new CostCenterResponse()
            {
                CostCenterIdentifier = item.CostCenterIdentifier,
                businessIdentifier = item.businessIdentifier,
                CostCenterName = item.CostCenterName,
                ParentCostCenterIdentifier = item.ParentCostCenterIdentifier,
                children = FillRecursive(employeeVms, item.CostCenterIdentifier)

            }).ToList();
        }

    }
}
