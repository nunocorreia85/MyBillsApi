using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using MyBills.Application.Accounts.Commands.CreateAccount;

namespace MyBills.Api
{
    public class Accounts
    {
        private readonly IMediator _mediator;

        public Accounts(IMediator mediator)
        {
            _mediator = mediator;
        }

        [FunctionName("CreateAccount")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "accounts")]
            HttpRequest req,
            ILogger log)
        {
            log.LogInformation("Creating a new account");

            var id = await _mediator.Send(new CreateAccountCommand
            {
                Balance = 1,
                BankAccountNumber = "DE89370400440532013000",
                OwnerName = "Joao Beleza"
            });

            log.LogInformation("This HTTP triggered function executed successfully.");

            return new OkObjectResult(id);
        }
    }
}