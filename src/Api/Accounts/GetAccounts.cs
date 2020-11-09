using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Primitives;
using MyBills.Application.Accounts.Queries;

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
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = null)]
            HttpRequest req, ILogger log, CancellationToken token)
        {
            var ids = HttpRequestUtils.GetQueryIds(req);
            var accounts = await _mediator.Send(new GetAccountsQuery
            {
                Ids = ids
            }, token);
            return new OkObjectResult(accounts);
        }
    }
}