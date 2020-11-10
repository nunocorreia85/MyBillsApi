using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using MediatR;
using System.Net.Http;
using System.Threading;
using MyBills.Application.Common.Exceptions;
using MyBills.Application.TransactionCategories.Commands.CreateTransactionCategory;
using System.Runtime.InteropServices.ComTypes;

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
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = null)] HttpRequestMessage req,
            ILogger log, CancellationToken token)
        {
            try
            {
                var command = await req.Content.ReadAsAsync<CreateTransactionCategoryCommand>(token);
                long id = await _mediator.Send(command, token);
                return new OkObjectResult(id);
            }
            catch (ValidationException ex)
            {
                log.LogError("Validations Errors {errors}", ex.Errors);
                return new BadRequestObjectResult(ex.Errors);
            }
        }
    }

    public class UpdateTransactionCategory
    {
        private readonly IMediator _mediator;

        public UpdateTransactionCategory(IMediator mediator)
        {
            _mediator = mediator;
        }

        [FunctionName("UpdateTransactionCategory")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "put", Route = null)] HttpRequestMessage req,
            ILogger log, CancellationToken token)
        {
            try
            {
                var command = await req.Content.ReadAsAsync<CreateTransactionCategoryCommand>(token);
                long id = await _mediator.Send(command, token);
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