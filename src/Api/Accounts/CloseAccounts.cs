using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using MyBills.Api.Common;
using MyBills.Application.Common.Exceptions;
using MyBills.Application.Shared.Accounts.Commands;

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
            HttpRequestMessage req, ILogger log, CancellationToken token)
        {
            var jwtSecurityToken = JwtTokenUtils.GetSecurityToken(req);
            var objectId = JwtTokenUtils.GetObjectId(jwtSecurityToken);

            try
            {
                await _mediator.Send(new CloseAccountsCommand
                {
                    UserId = objectId
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