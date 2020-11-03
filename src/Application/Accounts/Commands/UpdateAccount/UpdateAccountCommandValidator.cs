using FluentValidation;
using IbanNet;
using IbanNet.FluentValidation;
using MyBills.Application.Accounts.Commands.UpdateAccount;

namespace MyBills.Application.Accounts.Commands.UpdateAccount
{
    public class UpdateAccountCommandValidator : AbstractValidator<UpdateAccountCommand>
    {
        public UpdateAccountCommandValidator()
        {
            IbanValidator ibanValidator = new IbanValidator();
            RuleFor(v => v.BankAccountNumber)
                .Iban(ibanValidator);
            RuleFor(v => v.OwnerName)
                .NotEmpty();
        }
    }
}