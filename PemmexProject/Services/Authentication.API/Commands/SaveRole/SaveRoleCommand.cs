using Authentication.API.Database.context;
using Authentication.API.Dtos;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PemmexCommonLibs.Application.Helpers;
using PemmexCommonLibs.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Authentication.API.Commands.SaveRole
{
    public class SaveRoleCommand : IRequest
    {
        public RoleDto role { get; set; }
    }

    public class SaveRoleCommandHandeler : IRequestHandler<SaveRoleCommand>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        public SaveRoleCommandHandeler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<Unit> Handle(SaveRoleCommand request, CancellationToken cancellationToken)
        {

            var user = await _context.Users.Where(x => x.Id == request.role.UserId && x.isActive == true).FirstOrDefaultAsync(cancellationToken);
            if(user != null)
            {
                if(Enum.IsDefined(typeof(Roles), request.role.role))
                {
                    user.Role = (int) request.role.role;
                    await _context.SaveChangesAsync(cancellationToken);
                }
            }
            return Unit.Value;
        }
    }
}
