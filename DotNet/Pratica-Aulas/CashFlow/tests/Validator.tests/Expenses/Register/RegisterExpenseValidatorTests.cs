using CashFlow.Application.UseCase.Expenses;
using CashFlow.Communication.Enums;
using CashFlow.Exception;
using CommonTestUtilities.Requests;
using FluentAssertions;
using Shouldly;

namespace Validator.Tests.Expenses.Register;

public class RegisterExpenseValidatorTests
{
    [Fact]
    public void Success()
    {
        //arrange
        var validator = new ExpenseValidator();
        var request =  RequestRegisterExpenseJsonBuilder.Build();

        //act
        var result = validator.Validate(request);
        //assert
        result.ShouldNotBeNull();
    }

    [Fact]
    public void Fail_Title_Empty()
    {
        //arrange
        var validator = new ExpenseValidator();
        var request = RequestRegisterExpenseJsonBuilder.Build();
        request.Title = string.Empty;

        //act
        var result = validator.Validate(request);

        //assert
        //Shoudly version
        //result.IsValid.ShouldBeFalse();
        //result.Errors.Count.ShouldBe(1);
        //result.Errors[0].ErrorMessage.ShouldBe(ResourceErrorMessages.TITLE_REQUIRED);

        //Fluent version
        result.Errors.Should().ContainSingle().And.Contain(e => e.ErrorMessage.Equals(ResourceErrorMessages.TITLE_REQUIRED));
    }

    [Fact]
    public void Error_Date_Future()
    {
        //arrange
        var validator = new ExpenseValidator();
        var request = RequestRegisterExpenseJsonBuilder.Build();
        request.Date = DateTime.Now.AddDays(1);

        //act
        var result = validator.Validate(request);

        //assert
        //Shoudly version
        //result.IsValid.ShouldBeFalse();
        //result.Errors.Count.ShouldBe(1);
        //result.Errors[0].PropertyName.ShouldBe(ResourceErrorMessages.DATE_CANNOT_BE_IN_FUTURE);

        //Fluent version
        result.Errors.Should().ContainSingle().And.Contain(e => e.ErrorMessage.Equals(ResourceErrorMessages.DATE_CANNOT_BE_IN_FUTURE));
    }

    [Fact]
    public void Error_Payment_Type_Invalid()
    {
        //arrange
        var validator = new ExpenseValidator();
        var request = RequestRegisterExpenseJsonBuilder.Build();
        request.paymentType = (PaymentType)999; // Invalid enum value

        //act
        var result = validator.Validate(request);

        //assert
        //Shoudly version
        //result.IsValid.ShouldBeFalse();
        //result.Errors.Count.ShouldBe(1);
        //result.Errors[0].PropertyName.ShouldBe(ResourceErrorMessages.INVALID_PAYMENT_TYPE);

        //Fluent version
        result.Errors.Should().ContainSingle().And.Contain(e => e.ErrorMessage.Equals(ResourceErrorMessages.INVALID_PAYMENT_TYPE));
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    public void Error_Amount_Invalid(decimal amount)
    {
        //arrange
        var validator = new ExpenseValidator();
        var request = RequestRegisterExpenseJsonBuilder.Build();
        request.Amount = amount; 

        //act
        var result = validator.Validate(request);

        //assert
        //Shoudly version
        //result.IsValid.ShouldBeFalse();
        //result.Errors.Count.ShouldBe(1);
        //result.Errors[0].PropertyName.ShouldBe(ResourceErrorMessages.INVALID_PAYMENT_TYPE);

        //Fluent version
        result.Errors.Should().ContainSingle().And.Contain(e => e.ErrorMessage.Equals(ResourceErrorMessages.AMOUNT_MUST_BE_GREATER_THAN_0));
    }

}