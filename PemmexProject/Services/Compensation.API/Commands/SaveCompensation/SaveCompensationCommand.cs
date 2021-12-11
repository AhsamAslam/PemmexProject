using AutoMapper;
using Compensation.API.Database.context;
using Compensation.API.Dtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Compensation.API.Commands.SaveCompensation
{
    public class SaveCompensationCommand : IRequest
    {
        public CompensationDto compensationDto { get; set; }
    }

    public class SaveCompensationCommandHandeler : IRequestHandler<SaveCompensationCommand>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        public SaveCompensationCommandHandeler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<Unit> Handle(SaveCompensationCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var compensation = _mapper.Map<Database.Entities.Compensation>(request.compensationDto);
                _context.Compensation.Add(compensation);
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
