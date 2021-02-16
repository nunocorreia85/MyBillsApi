using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using MyBills.Application.Shared.Accounts.Queries;
using MyBills.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace MyBills.Api.Accounts
{
    public class GetAccounts
    {
        private readonly IMediator _mediator;

        public GetAccounts(IMediator mediator)
        {
            _mediator = mediator;
        }

        [FunctionName("GetAccounts")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = "accounts")]
            HttpRequest req, ILogger log, CancellationToken token)
        {
            JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();
            var securityToken = GetSecurityToken(req, handler);
            var objectId = GetObjectId(securityToken);

            var accounts = await _mediator.Send(
                new GetAccountsQuery { ExternalIds = new List<Guid> { objectId } }, token);
            return new OkObjectResult(accounts);
        }

        private static JwtSecurityToken GetSecurityToken(HttpRequest req, JwtSecurityTokenHandler handler)
        {
            var authorization = req.Headers["Authorization"].ToList();
            if(authorization[0].Length == 0)
            {
                throw new Exception("Missing authorization header");
            }
            return handler.ReadJwtToken(authorization[0].Replace("Bearer ", ""));
        }

        private static Guid GetObjectId(JwtSecurityToken securityToken)
        {
            string objectIdString = securityToken.Claims.FirstOrDefault(c => c.Type == "oid")?.Value;
            if (!Guid.TryParse(objectIdString, out var objectId))
            {
                throw new MissingClaimException()
                {
                    Claim = "oid"
                };
            }
            return objectId;
        }
    }
}