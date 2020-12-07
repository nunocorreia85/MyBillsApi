using FluentValidation;
using MyBills.Application.Shared.Accounts.Commands.CloseAccounts;

namespace MyBills.Application.Accounts.Commands.CloseAccounts
{
    public class CloseAccountsCommandValidator : AbstractValidator<CloseAccountsCommand>
    {
        public CloseAccountsCommandValidator()
        {
            RuleFor(v => v.Ids)
                .NotNull()
                .NotEmpty();
        }
    }
}