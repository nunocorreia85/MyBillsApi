using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using MyBills.Api.Common;
using MyBills.Application.Shared.Accounts.Queries;

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
            HttpRequestMessage req, ILogger log, CancellationToken token)
        {
            var securityToken = JwtTokenUtils.GetSecurityToken(req);
            var objectId = JwtTokenUtils.GetObjectId(securityToken);

            var accounts = await _mediator.Send(
                new GetAccountQuery {UserId = objectId}, token);
            return new OkObjectResult(accounts);
        }
    }
}