using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using MyBills.Application.Accounts.Commands.CreateAccount;
using MyBills.Application.Common.Interfaces;
using Newtonsoft.Json;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace MyBills.Api
{
    public class Accounts
    {
        private readonly IMediator _mediator;

        public Accounts(IMediator mediator)
        {
            _mediator = mediator;
        }

        //[FunctionName("CreateAccount")]
        //public async Task<IActionResult> Run(
        //    [HttpTrigger(AuthorizationLevel.Function, "post", Route = "accounts")] HttpRequest req,
        //    ILogger log)
        //{
        //    log.LogInformation("Creating a new account");

        //    string requestBody = await new StreamReader(req.Body).ReadToEndAsync();

        //    var input = JsonConvert.DeserializeObject<CreateAccountCommand>(requestBody);

        //    string id = await _mediator.Send(input);

        //    log.LogInformation("This HTTP triggered function executed successfully.");

        //    return new OkObjectResult(id);
        //}

        [FunctionName("CreateAccount")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = "accounts")] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("Creating a new account");

            string id = await _mediator.Send(new CreateAccountCommand()
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