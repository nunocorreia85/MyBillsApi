using FluentValidation;

namespace MyBills.Application.TransactionCategories.Commands.CreateTransactionCategory
{
    public class CreateTransactionCategoryCommandValidator : AbstractValidator<CreateTransactionCategoryCommand>
    {
        public CreateTransactionCategoryCommandValidator()
        {
            RuleFor(v => v.AccountId)
                .NotEqual(0);
            RuleFor(v => v.Name)
                .NotEmpty();
            RuleFor(v => v.Description)
                .MaximumLength(200);
        }
    }
}