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
using MyBills.Application.TransactionCategories.Commands.CreateTransactionCategory;

namespace MyBills.Api.TransactionCategories
{
    public class CreateTransactionCategory
    {
        private readonly IMediator _mediator;

        public CreateTransactionCategory(IMediator mediator)
        {
            _mediator = mediator;
        }

        [FunctionName("CreateTransactionCategory")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = "transactioncategories")]
            HttpRequestMessage req,
            ILogger log, CancellationToken token)
        {
            try
            {
                var command = await req.Content.ReadAsAsync<CreateTransactionCategoryCommand>(token);
                var id = await _mediator.Send(command, token);
                return new OkObjectResult(id);
            }
            catch (ValidationException ex)
            {
                log.LogError("Validations Errors {errors}", ex.Errors);
                return new BadRequestObjectResult(ex.Errors);
            }
        }
    }
}