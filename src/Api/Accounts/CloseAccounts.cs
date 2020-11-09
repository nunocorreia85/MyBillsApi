using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using MyBills.Application.Accounts.Commands.CloseAccount;
using MyBills.Application.Common.Exceptions;

namespace MyBills.Api.Accounts
{
    public class CloseAccounts
    {
        private readonly IMediator _mediator;

        public CloseAccounts(IMediator mediator)
        {
            _mediator = mediator;
        }

        [FunctionName("CloseAccounts")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = null)]
            HttpRequest req, ILogger log, CancellationToken token)
        {
            var ids = HttpRequestUtils.GetQueryIds(req);
            try
            {
                await _mediator.Send(new CloseAccountCommand
                {
                    Ids = ids
                }, token);
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