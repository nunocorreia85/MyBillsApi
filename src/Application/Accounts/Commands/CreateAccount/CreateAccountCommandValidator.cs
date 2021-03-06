﻿using FluentValidation;
using IbanNet;
using IbanNet.FluentValidation;
using MyBills.Application.Shared.Accounts.Commands;

namespace MyBills.Application.Accounts.Commands.CreateAccount
{
    public class UpdateAccountCommandValidator : AbstractValidator<CreateAccountCommand>
    {
        public UpdateAccountCommandValidator()
        {
            var ibanValidator = new IbanValidator();
            RuleFor(v => v.BankAccountNumber)
                .Iban(ibanValidator);
            RuleFor(v => v.UserId)
                .NotEmpty();
        }
    }
}