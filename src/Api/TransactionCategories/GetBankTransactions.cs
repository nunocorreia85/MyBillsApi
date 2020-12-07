using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using MyBills.Api.Common;
using MyBills.Application.Shared.TransactionCategories.Queries;
using MyBills.Application.TransactionCategories.Queries;

namespace MyBills.Api.TransactionCategories
{
    public class GetTransactionCategories
    {
        private readonly IMediator _mediator;

        public GetTransactionCategories(IMediator mediator)
        {
            _mediator = mediator;
        }

        [FunctionName("GetTransactionCategories")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = "transactioncategories")]
            HttpRequest req, CancellationToken token)
        {
            var ids = HttpRequestUtils.GetQueryKeyValues<long>(req, "id");
            var bankTransactions = await _mediator.Send(
                new GetTransactionCategoriesQuery
                {
                    Ids = ids
                }, token);
            return new OkObjectResult(bankTransactions);
        }
    }
}