using CashFlow.Communication.Requests;
using CashFlow.Exception;
using FluentValidation;

namespace CashFlow.Application.UseCase.Expenses;

public  class ExpenseValidator : AbstractValidator<RequestExpenseJson>
{
    /// <summary>
    /// Configures validation rules for RequestExpenseJson properties.
    /// </summary>
    /// <remarks>
    /// Applies the following validations:
    /// - Title: required and maximum length of 100 characters.
    /// - Description: maximum length of 500 characters.
    /// - Amount: must be greater than 0.
    /// - Date: must be less than or equal to the current UTC date/time.
    /// - paymentType: must be a valid enum value.
    /// </remarks>
    public ExpenseValidator()
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