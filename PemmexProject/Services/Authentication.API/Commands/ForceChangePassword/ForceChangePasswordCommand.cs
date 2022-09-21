using Authentication.API.Database.context;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PemmexCommonLibs.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Authentication.API.Commands.ForceChangePassword
{
    public class ForceChangePasswordCommand : IRequest
    {
        public Guid UserId { get; set; }
        public string password { get; set; }
    }

    public class ForceChangePasswordCommandHandeler : IRequestHandler<ForceChangePasswordCommand>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        public ForceChangePasswordCommandHandeler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<Unit> Handle(ForceChangePasswordCommand request, CancellationToken cancellationToken)
        {

            var user = await _context.Users.Where(x => x.Id == request.UserId && x.isActive == true).FirstOrDefaultAsync(cancellationToken);
            if (user != null)
            {
                user.Password = EncryptionHelper.Encrypt(request.password);
                user.IsPasswordReset = true;
                _context.Users.Update(user);
                await _context.SaveChangesAsync(cancellationToken);
            }
            return Unit.Value;
        }
    }
}
