﻿using System.Collections.Generic;
using MediatR;
using MyBills.Domain.Entities;

namespace MyBills.Application.Accounts.Queries.GetAccounts
{
    public class GetAccountsQuery : IRequest<IEnumerable<Account>>
    {
        public List<long> Ids { get; set; }
    }
}