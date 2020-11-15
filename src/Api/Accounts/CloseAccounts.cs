using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using MyBills.Api.Common;
using MyBills.Application.Accounts.Commands.CloseAccounts;
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
            [HttpTrigger(AuthorizationLevel.Function, "delete", Route = "accounts")]
            HttpRequest req, ILogger log, CancellationToken token)
        {
            var ids = HttpRequestUtils.GetQueryIds(req);
            try
            {
                await _mediator.Send(new CloseAccountsCommand
                {
                    Ids = ids
                }, token);
                return new OkResult();
            }
            catch (ValidationException ex)
            {
                log.LogError("Validations Errors {errors}", ex.Errors);
                return new BadRequestObjectResult(ex);
            }
        }
    }
}