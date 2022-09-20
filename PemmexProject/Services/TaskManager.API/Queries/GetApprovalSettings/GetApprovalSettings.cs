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
using TaskManager.API.Database.Repositories.Interface;
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
        private readonly IApprovalSettings _approvalSettings;
        private readonly IMapper _mapper;

        public GetApprovalSettingsQueryHandeler(IApplicationDbContext context, IApprovalSettings approvalSettings, IMapper mapper)
        {
            _context = context;
            _approvalSettings = approvalSettings;
            _mapper = mapper;
        }
        public async Task<List<ApprovalSettingDto>> Handle(GetApprovalSettings request, CancellationToken cancellationToken)
        {
            try
            {
                var settings = await _approvalSettings.GetOrganizationApprovalSettingsByOrganizationIdentifier(request.Id);
                return _mapper.Map<List<OrganizationApprovalSettings>, List<ApprovalSettingDto>>(settings.ToList());
            }
            catch (Exception)
            {

                throw;
            }
            
        }
    }
}
