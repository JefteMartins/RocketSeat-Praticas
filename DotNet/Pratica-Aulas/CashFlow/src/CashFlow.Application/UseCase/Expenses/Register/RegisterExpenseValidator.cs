using CashFlow.Communication.Requests;
using CashFlow.Exception;
using FluentValidation;

namespace CashFlow.Application.UseCase.Expenses.Register;

internal class RegisterExpenseValidator : AbstractValidator<RequestRegisterExpenseJson>
{
    public RegisterExpenseValidator()
    {
        RuleFor(x => x.Title)
            .NotEmpty().WithMessage(ResourceErrorMessages.TITLE_REQUIRED)
            .MaximumLength(100).WithMessage(ResourceErrorMessages.TITLE_LENGTH_GREATER_THAN_100);
        RuleFor(x => x.Description)
            .MaximumLength(500).WithMessage(ResourceErrorMessages.DESCRIPTION_GREATER_THAN_500);
        RuleFor(x => x.Amount)
            .GreaterThan(0).WithMessage(ResourceErrorMessages.AMOUNT_MUST_BE_GREATER_THAN_0);
        RuleFor(x => x.Date)
            .LessThanOrEqualTo(DateTime.UtcNow).WithMessage(ResourceErrorMessages.DATE_CANNOT_BE_IN_FUTURE);
        RuleFor(x => x.paymentType)
            .IsInEnum().WithMessage(ResourceErrorMessages.INVALID_PAYMENT_TYPE);
    }
}
