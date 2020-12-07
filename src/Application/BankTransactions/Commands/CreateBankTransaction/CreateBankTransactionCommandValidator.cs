using FluentValidation;
using MyBills.Application.Shared.BankTransactions.Commands;

namespace MyBills.Application.BankTransactions.Commands.CreateBankTransaction
{
    public class CreateBankTransactionCommandValidator : AbstractValidator<CreateBankTransactionCommand>
    {
        public CreateBankTransactionCommandValidator()
        {
            RuleFor(v => v.Memo)
                .MaximumLength(200);
            RuleFor(v => v.Amount)
                .NotEqual(0);
            RuleFor(v => v.AccountId)
                .NotEqual(0);
            RuleFor(v => v.CategoryId)
                .NotEqual(0);
        }
    }
}