using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using MyBills.Api.Common;
using MyBills.Application.Accounts.Queries.GetAccounts;

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
            var ids = HttpRequestUtils.GetQueryKeyValues<long>(req, "id");
            var externalIds = HttpRequestUtils.GetQueryKeyValues<string>(req, "externalIds");
            var accounts = await _mediator.Send(
                new GetAccountsQuery
                {
                    ExternalIds = externalIds.Select(externalId => new Guid(externalId)).ToList(),
                    Ids = ids
                }, token);
            return new OkObjectResult(accounts);
        }
    }
}