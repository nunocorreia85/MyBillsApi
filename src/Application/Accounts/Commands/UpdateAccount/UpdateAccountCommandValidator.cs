using FluentValidation;
using IbanNet;
using IbanNet.FluentValidation;

namespace MyBills.Application.Accounts.Commands.UpdateAccount
{
    public class UpdateAccountCommandValidator : AbstractValidator<UpdateAccountCommand>
    {
        public UpdateAccountCommandValidator()
        {
            var ibanValidator = new IbanValidator();
            RuleFor(v => v.BankAccountNumber)
                .Iban(ibanValidator);
        }
    }
}