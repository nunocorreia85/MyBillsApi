using System;

namespace MyBills.Domain.Exceptions
{
    public class MissingClaimException : Exception
    {
        public string Claim { get; set; }
    }
}