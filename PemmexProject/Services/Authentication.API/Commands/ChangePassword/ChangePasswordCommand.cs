using Authentication.API.Configuration;
using Authentication.API.Database.context;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PemmexCommonLibs.Application.Helpers;
using PemmexCommonLibs.Application.Interfaces;
using PemmexCommonLibs.Domain.Common;
using PemmexCommonLibs.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Authentication.API.Commands.ChangePassword
{
    public class ChangePasswordCommand : IRequest<ResponseMessage>
    {
        public string code { get; set; }
        public string email { get; set; }
        public string password { get; set; }
    }

    public class ChangePasswordCommandHandeler : IRequestHandler<ChangePasswordCommand, ResponseMessage>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly IDateTime _dateTime;
        private readonly IUserManager _userManager;
        public ChangePasswordCommandHandeler(IApplicationDbContext context, IMapper mapper, 
            IDateTime dateTime, IUserManager userManager)
        {
            _context = context;
            _mapper = mapper;
            _dateTime = dateTime;
            _userManager = userManager;
        }
        public async Task<ResponseMessage> Handle(ChangePasswordCommand request, CancellationToken cancellationToken)
        {

            var user = await _userManager.FindByUserNameAsync(request.email);
            if (user != null)
            {
                
                if (user.PasswordResetCode != request.code)
                {
                    return new ResponseMessage(false, EResponse.NoPermission, "Code Did not match", request);
                }
                else if ((_dateTime.Now - user.PasswordResetCodeTime).TotalDays > 1)
                {
                    return new ResponseMessage(false, EResponse.NoPermission, "Code Expired Please request for reset again.", request);
                }
                user.Password = EncryptionHelper.Encrypt(request.password);
                user.PasswordResetCode = "";
                _context.Users.Update(user);
                await _context.SaveChangesAsync(cancellationToken);
                return new ResponseMessage(false, EResponse.OK, "Password Changed Successfully", request);
            }

            return new ResponseMessage(false, EResponse.NoData, "No User Found", request);
        }
    }
}
