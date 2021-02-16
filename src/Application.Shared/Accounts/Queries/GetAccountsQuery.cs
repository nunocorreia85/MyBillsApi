using MediatR;
using MyBills.Domain.Entities;
using System;
using System.Collections.Generic;

namespace MyBills.Application.Shared.Accounts.Queries
{
    public class GetAccountsQuery : IRequest<IEnumerable<Account>>
    {
        public GetAccountsQuery()
        {
            Ids = new List<long>();
            ExternalIds = new List<Guid>();
        }

        public List<long> Ids { get; set; }

        public List<Guid> ExternalIds { get; set; }
    }
}