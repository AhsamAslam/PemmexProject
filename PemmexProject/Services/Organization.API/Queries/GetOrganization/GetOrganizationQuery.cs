using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Organization.API.Dtos;
using Organization.API.Interfaces;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Organization.API.Queries.GetOrganization
{
    public class GetOrganizationQuery : IRequest<BusinessVM>
    {
        public string Id { get; set; }
    }

    public class GetOrganizationQueryHandeler : IRequestHandler<GetOrganizationQuery, BusinessVM>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetOrganizationQueryHandeler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<BusinessVM> Handle(GetOrganizationQuery request, CancellationToken cancellationToken)
        {
            var organization = await _context.Businesses
                .Where(e => e.ParentBusinessId == request.Id && e.IsActive == true)
                .FirstOrDefaultAsync(cancellationToken);

            return _mapper.Map<Entities.Business, BusinessVM>(organization);
        }
    }
}
