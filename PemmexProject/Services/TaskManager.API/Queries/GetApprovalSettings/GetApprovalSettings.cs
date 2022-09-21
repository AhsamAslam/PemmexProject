using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TaskManager.API.Database.context;
using TaskManager.API.Database.Entities;
using TaskManager.API.Dtos;

namespace TaskManager.API.Queries.GetApprovalSettings
{
    public class GetApprovalSettings : IRequest<List<ApprovalSettingDto>>
    {
        public string Id { get; set; }
    }
    public class GetApprovalSettingsQueryHandeler : IRequestHandler<GetApprovalSettings, List<ApprovalSettingDto>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetApprovalSettingsQueryHandeler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<List<ApprovalSettingDto>> Handle(GetApprovalSettings request, CancellationToken cancellationToken)
        {
            var settings = await _context.organizationApprovalSettings
                .Where(e => e.OrganizationIdentifier == request.Id)
                .Include(d => d.organizationApprovalSettingDetails)
                .ToListAsync(cancellationToken);
            return _mapper.Map<List<OrganizationApprovalSettings>, List<ApprovalSettingDto>>(settings);
        }
    }
}
