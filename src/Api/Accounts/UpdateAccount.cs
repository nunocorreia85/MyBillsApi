using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using MyBills.Api.Common;
using MyBills.Application.Common.Exceptions;
using MyBills.Application.Shared.Accounts.Commands;

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
            [HttpTrigger(AuthorizationLevel.Function, "put", Route = "accounts")]
            HttpRequestMessage req, ILogger log, CancellationToken token)
        {
            var jwtSecurityToken = JwtTokenUtils.GetSecurityToken(req);
            var objectId = JwtTokenUtils.GetObjectId(jwtSecurityToken);

            var command = await req.Content.ReadAsAsync<UpdateAccountCommand>(token);
            command.UserId = objectId;
            try
            {
                var id = await _mediator.Send(command, token);
                log.LogInformation("Updated account with id {id}", id);
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