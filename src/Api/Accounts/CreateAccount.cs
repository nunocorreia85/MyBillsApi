using System.Net.Http;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using MyBills.Application.Accounts.Commands.CreateAccount;

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
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = null)]
            HttpRequestMessage req, ILogger log)
        {
            var command = await req.Content.ReadAsAsync<CreateAccountCommand>();
            var id = await _mediator.Send(command);

            if (id == 0)
            {
                log.LogError("Failed to create the account account {OwnerName}", command.OwnerName);
                return new BadRequestObjectResult("Failed to create the account account");
            }

            log.LogInformation("Account created with id {id}", id);
            return new OkObjectResult(id);
        }
    }
}