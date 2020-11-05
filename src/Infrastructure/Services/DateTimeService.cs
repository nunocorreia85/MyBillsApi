using System;
using MyBills.Application.Common.Interfaces;

namespace MyBills.Infrastructure.Services
{
    public class DateTimeService : IDateTime
    {
        public DateTime Now => DateTime.UtcNow;
    }
}