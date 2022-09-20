using Authentication.API.Configuration;
using Authentication.API.Database.context;
using Authentication.API.Database.Repositories.Interface;
using AutoMapper;
using IdentityModel.Client;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using PemmexCommonLibs.Application.Helpers;
using PemmexCommonLibs.Application.Interfaces;
using PemmexCommonLibs.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Authentication.API.Commands
{
    public class TwoStepLoginCommand : IRequest<ResponseMessage>
    {
        public string userName { get; set; }
        public string password { get; set; }
        public string code { get; set; }
    }
    public class TwoStepLoginCommandHandeler : IRequestHandler<TwoStepLoginCommand, ResponseMessage>
    {
        private readonly IConfiguration _configuration;
        private readonly IUserManager _userManager;
        private readonly IDateTime _dateTime;
        public TwoStepLoginCommandHandeler(IConfiguration configuration,IUserManager userManager,IDateTime dateTime)
        {
            _configuration = configuration;
            _userManager = userManager;
            _dateTime = dateTime;
        }
        public async Task<ResponseMessage> Handle(TwoStepLoginCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var user = await _userManager.VerifyAndGetUserAsync(request.userName,request.password);
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
                    user.PasswordResetCode = "";
                    var client = new HttpClient();
                    var identityServerResponse = await client.RequestPasswordTokenAsync(new PasswordTokenRequest
                    {
                        Address = $"{_configuration.GetValue<string>("Configurations:applicationUrl")}/connect/token",
                        GrantType = _configuration.GetValue<string>("Configurations:GrantType"),

                        ClientId = _configuration.GetValue<string>("Configurations:ClientId"),
                        ClientSecret = _configuration.GetValue<string>("Configurations:ClientSecret"),
                        Scope = _configuration.GetValue<string>("Configurations:Scope"),

                        UserName = request.userName,
                        Password = request.password
                    });
                    var claims = ClaimsProvider.GetUserFromClaims(identityServerResponse.AccessToken,
                        identityServerResponse.ExpiresIn, identityServerResponse.TokenType
                        , identityServerResponse.Scope);
                    return new ResponseMessage(false, EResponse.NoData, "User Data", claims);
                }
                return new ResponseMessage(false, EResponse.NoData, "No User Found or credentials did not match", request.userName);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
