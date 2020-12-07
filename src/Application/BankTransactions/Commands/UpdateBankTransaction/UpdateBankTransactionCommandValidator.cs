using FluentValidation;
using MyBills.Application.Shared.BankTransactions.Commands.UpdateBankTransaction;

namespace MyBills.Application.BankTransactions.Commands.UpdateBankTransaction
{
    public class UpdateBankTransactionCommandValidator : AbstractValidator<UpdateBankTransactionCommand>
    {
        public UpdateBankTransactionCommandValidator()
        {
            RuleFor(v => v.Memo)
                .MaximumLength(200);
            RuleFor(v => v.Amount)
                .NotEqual(0);
        }
    }
}