using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using MyBills.Api.Common;
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
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = null)]
            HttpRequest req, CancellationToken token)
        {
            var ids = HttpRequestUtils.GetQueryIds(req);
            var bankTransactions = await _mediator.Send(
                new GetTransactionCategoriesQuery
                {
                    Ids = ids
                }, token);
            return new OkObjectResult(bankTransactions);
        }
    }
}