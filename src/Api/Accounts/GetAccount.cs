using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using MyBills.Application.Accounts.Queries;

namespace MyBills.Api.Accounts
{
    public class GetAccount
    {
        private readonly IMediator _mediator;

        public GetAccount(IMediator mediator)
        {
            _mediator = mediator;
        }

        [FunctionName("GetAccount")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = null)]
            HttpRequest req, ILogger log, CancellationToken token)
        {
            var accounts = await _mediator.Send(new GetAccountsQuery
            {
                Id = long.TryParse(req.Query["id"], out var id) ? new long?(id) : null
            });
            return new OkObjectResult(accounts);
        }
    }
}