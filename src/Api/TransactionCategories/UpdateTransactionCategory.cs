using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using MyBills.Application.Common.Exceptions;
using MyBills.Application.Shared.TransactionCategories.Commands;

namespace MyBills.Api.TransactionCategories
{
    public class UpdateTransactionCategory
    {
        private readonly IMediator _mediator;

        public UpdateTransactionCategory(IMediator mediator)
        {
            _mediator = mediator;
        }

        [FunctionName("UpdateTransactionCategory")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "put", Route = "transactioncategories")]
            HttpRequestMessage req,
            ILogger log, CancellationToken token)
        {
            try
            {
                var command = await req.Content.ReadAsAsync<UpdateTransactionCategoryCommand>(token);
                await _mediator.Send(command, token);
                return new OkResult();
            }
            catch (ValidationException ex)
            {
                log.LogError("Validations Errors {errors}", ex.Errors);
                return new BadRequestObjectResult(ex.Errors);
            }
        }
    }
}