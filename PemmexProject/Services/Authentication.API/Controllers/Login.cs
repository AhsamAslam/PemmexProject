using Authentication.API.Commands;
using Authentication.API.Commands.ChangePassword;
using Authentication.API.Commands.ForceChangePassword;
using Authentication.API.Commands.SendPasswordEmail;
using Authentication.API.Commands.TwoStepAuthentication;
using Authentication.API.Configuration;
using Authentication.API.Dtos;
using Authentication.API.Queries.GetUserByName;
using AutoMapper;
using IdentityModel.Client;
using IdentityServer4;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using PemmexCommonLibs.Application.Helpers;
using PemmexCommonLibs.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Authentication.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class Login : ApiControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IdentityServerTools _tools;
        private readonly IMapper _mapper;
        public Login(IConfiguration configuration, IdentityServerTools tools,IMapper mapper)
        {
            _configuration = configuration;
            _tools = tools;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<ActionResult<ResponseMessage>> Post([FromBody] AuthenticationRequest request)
        {
            try
            {
                return await Mediator.Send(new TwoStepAuthenticationCommand() 
                { 
                  password = request.Password,
                  userName = request.UserName
                });
            }
            catch (Exception e)
            {
                return new ResponseMessage(false, EResponse.UnexpectedError, e.Message, null);
            }
        }
        [HttpPost]
        [Route("loginwithtwostep")]
        public async Task<ActionResult<ResponseMessage>> loginwithtwostep([FromBody] TwoStepLoginCommand request)
        {
            try
            {
                return await Mediator.Send(request);
            }
            catch (Exception e)
            {
                return new ResponseMessage(false, EResponse.UnexpectedError, e.Message, null);
            }
        }
        [HttpPost]
        [Route("forceresetpassword")]
        public async Task<ActionResult<ResponseMessage>> PostAsync([FromBody] ForceChangePasswordCommand command)
        {
            try
            {
                var data = await Mediator.Send(command);
                return new ResponseMessage(true, EResponse.OK, null, data);
            }
            catch (Exception e)
            {
                return new ResponseMessage(false, EResponse.UnexpectedError, e.Message, null);
            }
        }
        [HttpPost]
        [Route("sendpasswordemail")]
        public async Task<ActionResult<ResponseMessage>> sendPasswordEmail([FromBody] string email)
        {
            try
            {

                return await Mediator.Send(new SendPasswordEmailCommand() { email = email });
            }
            catch (Exception e)
            {
                return new ResponseMessage(false, EResponse.UnexpectedError, e.Message, null);
            }
        }
        [HttpPost]
        [Route("resetpassword")]
        public async Task<ActionResult<ResponseMessage>> ResetPassword([FromBody] ChangePasswordCommand command)
        {
            try
            {
                return await Mediator.Send(command);
            }
            catch (Exception e)
            {
                return new ResponseMessage(false, EResponse.UnexpectedError, e.Message, null);
            }
        }

        [HttpPost]
        [Route("activedirectorylogin")]
        public async Task<ActionResult<ResponseMessage>> activedirectorylogin([FromBody] string email)
        {
            try
            {
                var user = await Mediator.Send(new GetUserByNameQueries { Name = email });
                if (user != null)
                {
                    var token = await _tools.IssueClientJwtAsync(
                    clientId: "pemmexclient",
                    lifetime: 3600,
                    scopes: new[] { "organizationapi" },
                    additionalClaims: new List<Claim>
                    {
                      new Claim("UserObject", JsonConvert.SerializeObject(user))
                    });
                    var response = _mapper.Map<TokenUser>(user);
                    response.tokenObject.access_token = token;
                    response.tokenObject.expires_in = 3600;
                    response.tokenObject.token_type = "Bearer";
                    response.tokenObject.scope = "organizationapi";
                    return new ResponseMessage(false, EResponse.OK,null, response);
                }


                return new ResponseMessage(false, EResponse.NoData, "No User found", null);
            }
            catch (Exception e)
            {
                return new ResponseMessage(false, EResponse.UnexpectedError, e.Message, null);
            }
        }

    }
}
