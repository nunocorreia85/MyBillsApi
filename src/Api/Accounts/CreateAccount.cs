using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using MyBills.Application.Common.Exceptions;
using MyBills.Application.Shared.Accounts.Commands;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace MyBills.Api.Accounts
{
    public class CreateAccount
    {
        private readonly IMediator _mediator;

        public CreateAccount(IMediator mediator)
        {
            _mediator = mediator;
        }

        [FunctionName("CreateAccount")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = "accounts")]
            HttpRequestMessage req, ILogger log, CancellationToken token)
        {
            var command = await req.Content.ReadAsAsync<CreateAccountCommand>(token);
            try
            {
                var id = await _mediator.Send(command, token);
                log.LogInformation("Account created with id {id}", id);
                return new OkObjectResult(id);
            }
            catch (ValidationException ex)
            {
                log.LogError("Validations Errors {errors}", ex.Errors);
                return new BadRequestObjectResult(ex);
            }
        }
    }
}