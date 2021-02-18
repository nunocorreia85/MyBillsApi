using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using MyBills.Application.Shared.BankTransactions.Commands;

namespace MyBills.Api.BankTransactions
{
    public class UpdateBankTransaction
    {
        private readonly IMediator _mediator;

        public UpdateBankTransaction(IMediator mediator)
        {
            _mediator = mediator;
        }

        [FunctionName("UpdateBankTransaction")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "put", Route = "banktransactions")]
            HttpRequestMessage req, ILogger log, CancellationToken token)
        {
            try
            {
                var command = await req.Content.ReadAsAsync<UpdateBankTransactionCommand>(token);
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