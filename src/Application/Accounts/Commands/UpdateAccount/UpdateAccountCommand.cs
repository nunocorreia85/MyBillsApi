using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyBills.Application.Accounts.Commands.UpdateAccount
{
    public class UpdateAccountCommand : IRequest
    {
        public string Id { get; set; }
        public string OwnerName { get; set; }
        public string BankAccountNumber { get; set; }
    }
}