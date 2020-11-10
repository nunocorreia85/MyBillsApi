﻿using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using MyBills.Api.Common;
using MyBills.Application.BankTransactions.Commands.DeleteBankTransactions;

namespace MyBills.Api.BankTransactions
{
    public class DeleteBankTransactions
    {
        private readonly IMediator _mediator;

        public DeleteBankTransactions(IMediator mediator)
        {
            _mediator = mediator;
        }

        [FunctionName("DeleteBankTransactions")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = null)]
            HttpRequest req, ILogger log, CancellationToken token)
        {
            var ids = HttpRequestUtils.GetQueryIds(req);
            try
            {
                await _mediator.Send(new DeleteBankTransactionsCommand
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