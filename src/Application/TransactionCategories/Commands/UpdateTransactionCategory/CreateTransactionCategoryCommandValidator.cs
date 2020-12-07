using FluentValidation;
using MyBills.Application.Shared.TransactionCategories.Commands.UpdateTransactionCategory;

namespace MyBills.Application.TransactionCategories.Commands.UpdateTransactionCategory
{
    public class UpdateTransactionCategoryCommandValidator : AbstractValidator<UpdateTransactionCategoryCommand>
    {
        public UpdateTransactionCategoryCommandValidator()
        {
            RuleFor(v => v.Name)
                .NotEmpty();
            RuleFor(v => v.Description)
                .MaximumLength(200);
        }
    }
}