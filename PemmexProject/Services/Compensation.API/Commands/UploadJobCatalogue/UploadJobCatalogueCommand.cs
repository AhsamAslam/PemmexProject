using AutoMapper;
using Compensation.API.Database.context;
using Compensation.API.Database.Entities;
using Compensation.API.Database.Repositories.Interface;
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
        private readonly IJobCatalogue _jobCatalogue;
        private readonly IMapper _mapper;
        public UploadJobCatalogueCommandHandeler(IApplicationDbContext context, IJobCatalogue jobCatalogue, IMapper mapper)
        {
            _context = context;
            _jobCatalogue = jobCatalogue;
            _mapper = mapper;
        }
        public async Task<Unit> Handle(UploadJobCatalogueCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var job_catalogues = _mapper.Map<List<JobCatalogue>>(request.JobCatalogueDtos);
                //_context.JobCatalogues.AddRange(job_catalogues);
                //await _context.SaveChangesAsync(cancellationToken);
                foreach (var item in job_catalogues)
                {
                    await _jobCatalogue.AddJobCatalogue(item);
                }
                return Unit.Value;
            }
            catch (Exception e)
            {
                throw new Exception(e.ToString());
            }
        }
    }
}
