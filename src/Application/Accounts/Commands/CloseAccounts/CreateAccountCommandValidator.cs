using FluentValidation;

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