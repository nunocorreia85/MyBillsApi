using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net.Http;
using MyBills.Domain.Exceptions;

namespace MyBills.Api.Common
{
    public static class JwtTokenUtils
    {
        public static JwtSecurityToken GetSecurityToken(HttpRequestMessage req)
        {
            var handler = new JwtSecurityTokenHandler();
            var authorization = req.Headers.Authorization.Parameter;
            if (string.IsNullOrEmpty(authorization))
            {
                throw new Exception("Missing authorization header");
            }

            return handler.ReadJwtToken(authorization.Replace("Bearer ", ""));
        }

        public static Guid GetObjectId(JwtSecurityToken securityToken)
        {
            var objectIdString = securityToken.Claims.FirstOrDefault(c => c.Type == "oid")?.Value;
            if (!Guid.TryParse(objectIdString, out var objectId))
            {
                throw new MissingClaimException
                {
                    Claim = "oid"
                };
            }

            return objectId;
        }
    }
}