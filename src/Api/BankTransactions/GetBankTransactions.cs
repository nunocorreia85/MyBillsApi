using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using MyBills.Api.Common;
using MyBills.Application.BankTransactions.Queries;

namespace MyBills.Api.BankTransactions
{
    public class GetBankTransactions
    {
        private readonly IMediator _mediator;

        public GetBankTransactions(IMediator mediator)
        {
            _mediator = mediator;
        }

        [FunctionName("GetBankTransactions")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = "banktransactions")]
            HttpRequest req, ILogger log, CancellationToken token)
        {
            var ids = HttpRequestUtils.GetQueryKeyValues<long>(req, "id");
            var bankTransactions = await _mediator.Send(
                new GetBankTransactionsQuery
                {
                    Ids = ids
                }, token);
            return new OkObjectResult(bankTransactions);
        }
    }
}