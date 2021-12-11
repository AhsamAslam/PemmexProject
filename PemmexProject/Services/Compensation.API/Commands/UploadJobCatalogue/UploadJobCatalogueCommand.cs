using AutoMapper;
using Compensation.API.Database.context;
using Compensation.API.Database.Entities;
using Compensation.API.Dtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Compensation.API.Commands.UploadJobCatalogue
{
    public class UploadJobCatalogueCommand : IRequest
    {
        public List<JobCatalogueDto> JobCatalogueDtos { get; set; }
    }

    public class UploadJobCatalogueCommandHandeler : IRequestHandler<UploadJobCatalogueCommand>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        public UploadJobCatalogueCommandHandeler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<Unit> Handle(UploadJobCatalogueCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var job_catalogues = _mapper.Map<List<JobCatalogue>>(request.JobCatalogueDtos);
                _context.JobCatalogues.AddRange(job_catalogues);
                await _context.SaveChangesAsync(cancellationToken);
                return Unit.Value;
            }
            catch (Exception e)
            {
                throw new Exception(e.ToString());
            }
        }
    }
}
