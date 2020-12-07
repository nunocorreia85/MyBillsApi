using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using MyBills.Api.Common;
using MyBills.Application.Shared.TransactionCategories.Commands;
using MyBills.Application.TransactionCategories.Commands.DisableTransactionCategories;

namespace MyBills.Api.TransactionCategories
{
    public class DisableTransactionCategories
    {
        private readonly IMediator _mediator;

        public DisableTransactionCategories(IMediator mediator)
        {
            _mediator = mediator;
        }

        [FunctionName("DisableTransactionCategories")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "delete", Route = "transactioncategories")]
            HttpRequest req, ILogger log, CancellationToken token)
        {
            var ids = HttpRequestUtils.GetQueryKeyValues<long>(req, "id");
            try
            {
                await _mediator.Send(new DisableTransactionCategoriesCommand
                {
                    Ids = ids
                }, token);
                return new OkResult();
            }
            catch (ValidationException ex)
            {
                log.LogError("Validations Errors {errors}", ex.Errors);
                return new BadRequestObjectResult(ex);
            }
        }
    }
}