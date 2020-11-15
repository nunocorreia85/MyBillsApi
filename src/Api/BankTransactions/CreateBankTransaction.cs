using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using MyBills.Application.BankTransactions.Commands.CreateBankTransaction;
using MyBills.Application.Common.Exceptions;

namespace MyBills.Api.BankTransactions
{
    public class CreateBankTransaction
    {
        private readonly IMediator _mediator;

        public CreateBankTransaction(IMediator mediator)
        {
            _mediator = mediator;
        }

        [FunctionName("CreateBankTransaction")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = "banktransactions")]
            HttpRequestMessage req, ILogger log, CancellationToken token)
        {
            try
            {
                var command = await req.Content.ReadAsAsync<CreateBankTransactionCommand>(token);
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