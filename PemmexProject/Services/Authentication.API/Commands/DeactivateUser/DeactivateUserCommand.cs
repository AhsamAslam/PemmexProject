using Authentication.API.Database.context;
using Authentication.API.Database.Repositories.Interface;
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
        private readonly IUser _user;
        private readonly IMapper _mapper;
        public DeactivateUserCommandHandeler(IUser user, IMapper mapper)
        {
            _user = user;
            _mapper = mapper;
        }
        public async Task<Unit> Handle(DeactivateUserCommand request, CancellationToken cancellationToken)
        {

            try
            {
                //var user = await _context.Users.Where(x => x.Id == request.Id && x.isActive == true).FirstOrDefaultAsync(cancellationToken);
                var user = await _user.GetUserById(request.Id);
                if (user != null)
                {
                    user.isActive = false;
                    //_context.Users.Update(user);
                    //await _context.SaveChangesAsync(cancellationToken);
                    var UpdateUser = await _user.UpdateUser(user);
                }
                return Unit.Value;
            }
            catch (Exception)
            {

                throw;
            }
           
        }
    }
}
