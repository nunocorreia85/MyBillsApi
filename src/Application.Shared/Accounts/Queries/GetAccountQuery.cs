﻿using System;
using System.Collections.Generic;
using MediatR;
using MyBills.Domain.Entities;

namespace MyBills.Application.Shared.Accounts.Queries
{
    public class GetAccountQuery : IRequest<IEnumerable<Account>>
    {
        public Guid UserId { get; set; }
    }
}