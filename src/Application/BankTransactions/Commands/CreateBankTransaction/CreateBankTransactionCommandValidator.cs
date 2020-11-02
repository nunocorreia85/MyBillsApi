using FluentValidation;

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
        }
    }
}