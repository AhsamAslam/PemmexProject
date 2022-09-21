using Authentication.API.Database.context;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Authentication.API.Commands.DeactivateUser
{
    public class DeactivateUserCommand : IRequest
    {
        public Guid Id { get; set; }
    }

    public class DeactivateUserCommandHandeler : IRequestHandler<DeactivateUserCommand>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        public DeactivateUserCommandHandeler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<Unit> Handle(DeactivateUserCommand request, CancellationToken cancellationToken)
        {

            var user = await _context.Users.Where(x => x.Id == request.Id && x.isActive == true).FirstOrDefaultAsync(cancellationToken);
            if (user != null)
            {
                user.isActive = false;
                _context.Users.Update(user);
                await _context.SaveChangesAsync(cancellationToken);
            }
            return Unit.Value;
        }
    }
}
