using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Organization.API.Dtos;
using Organization.API.Interfaces;
using Organization.API.Repositories.Interface;
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
        private readonly IBusiness _business;
        private readonly IMapper _mapper;

        public GetOrganizationQueryHandeler(IBusiness business, IMapper mapper)
        {
            _business = business;
            _mapper = mapper;
        }
        public async Task<BusinessVM> Handle(GetOrganizationQuery request, CancellationToken cancellationToken)
        {
            try
            {
                //var organization = await _context.Businesses
                //    .Where(e => e.ParentBusinessId == request.Id && e.IsActive == true)
                //    .FirstOrDefaultAsync(cancellationToken);
                var organization = await _business.GetBusinessByParentBusinessIdIsActive(request.Id);

                return _mapper.Map<Entities.Business, BusinessVM>(organization);
            }
            catch (System.Exception)
            {

                throw;
            }
            
        }
    }
}
