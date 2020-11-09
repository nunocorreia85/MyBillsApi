using System.Collections.Generic;
using MediatR;
using MyBills.Domain.Entities;

namespace MyBills.Application.Accounts.Queries
{
    public class GetAccountsQuery : IRequest<List<Account>>
    {
        public List<long> Ids { get; set; }
    }
}