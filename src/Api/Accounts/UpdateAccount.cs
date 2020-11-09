using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using MyBills.Application.Accounts.Commands.UpdateAccount;
using MyBills.Application.Common.Exceptions;

namespace MyBills.Api.Accounts
{
    public class UpdateAccount
    {
        private readonly IMediator _mediator;

        public UpdateAccount(IMediator mediator)
        {
            _mediator = mediator;
        }

        [FunctionName("UpdateAccount")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = null)]
            HttpRequestMessage req, ILogger log, CancellationToken token)
        {
            var command = await req.Content.ReadAsAsync<UpdateAccountCommand>(token);
            try
            {
                var id = await _mediator.Send(command, token);
                log.LogInformation("Updated account with id {id}", id);
                return new OkResult();
            }
            catch (ValidationException e)
            {
                log.LogError("Validations Errors {errors}", e.Errors);
                return new BadRequestObjectResult("Validation Errors");
            }
        }
    }
}