using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Organization.API.Dtos;
using Organization.API.Entities;
using Organization.API.Interfaces;
using Organization.API.Repositories.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Organization.API.Queries.GetAllOrganizationEmployees
{
    public class GetAllBusinessesQuery : IRequest<List<BusinessVM>>
    {
        public string Id { get; set; }
    }

    public class GetAllBusinessesQueryHandeler : IRequestHandler<GetAllBusinessesQuery, List<BusinessVM>>
    {
        private readonly IBusiness _business;
        private readonly IMapper _mapper;

        public GetAllBusinessesQueryHandeler(IBusiness business, IMapper mapper)
        {
            _business = business;
            _mapper = mapper;
        }
        public async Task<List<BusinessVM>> Handle(GetAllBusinessesQuery request, CancellationToken cancellationToken)
        {
            //var businesses = await _context.Businesses
            //    .Where(e => e.ParentBusinessId == request.Id && e.IsActive == true)
            //    .ToListAsync(cancellationToken);
            var businesses = await _business.GetBusinessByParentBusinessId(request.Id);

            return _mapper.Map<List<Business>, List<BusinessVM>>(businesses.ToList());
        }
    }
}
