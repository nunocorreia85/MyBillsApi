using MyBills.Application.Common.Interfaces;
using System;

namespace MyBills.Infrastructure.Services
{
    public class DateTimeService : IDateTime
    {
        public DateTime Now => DateTime.UtcNow;
    }
}
