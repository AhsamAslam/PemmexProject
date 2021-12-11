using Authentication.API.Dtos;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Authentication.API.Configuration
{
    public static class ClaimsProvider
    {
        public static TokenUser GetUserFromClaims(string token,int expireIn,string TokenType,string Scope)
        {
            var handler = new JwtSecurityTokenHandler();
            var tokenProvider = handler.ReadToken(token) as JwtSecurityToken;
            var user = JsonConvert.DeserializeObject<TokenUser>(tokenProvider.Claims.First(claim => claim.Type == "UserObject").Value);
            user.tokenObject.access_token = token;
            user.tokenObject.expires_in = expireIn;
            user.tokenObject.token_type = TokenType;
            user.tokenObject.scope = Scope;

            return user;
        }
    }
}
