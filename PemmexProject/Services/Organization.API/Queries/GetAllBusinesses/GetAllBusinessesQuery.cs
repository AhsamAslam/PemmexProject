using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Organization.API.Dtos;
using Organization.API.Entities;
using Organization.API.Interfaces;
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
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetAllBusinessesQueryHandeler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<List<BusinessVM>> Handle(GetAllBusinessesQuery request, CancellationToken cancellationToken)
        {
            var businesses = await _context.Businesses
                .Where(e => e.BusinessIdentifier == request.Id && e.IsActive == true)
                .ToListAsync(cancellationToken);

            return _mapper.Map<List<Business>, List<BusinessVM>>(businesses);
        }
    }
}
