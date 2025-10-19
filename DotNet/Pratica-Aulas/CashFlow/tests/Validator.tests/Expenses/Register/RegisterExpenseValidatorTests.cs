﻿using CashFlow.Application.UseCase.Expenses.Register;
using CashFlow.Communication.Enums;
using CashFlow.Communication.Requests;
using CashFlow.Exception;

namespace Validator.Tests.Expenses.Register
{
    public class RegisterExpenseValidatorTests
    {
        [Fact]
        public void Success()
        {
            // Arrange
            var validator = new RegisterExpenseValidator();
            var request = new RequestRegisterExpenseJson
            {
                Title = "Almoço",
                Description = "Almoço de negócios",
                Amount = 50.00m,
                Date = DateTime.UtcNow.AddDays(-1),
                paymentType = PaymentType.CreditCard
            };

            // Act
            var result = validator.Validate(request);

            // Assert
            Assert.True(result.IsValid);
        }

        [Fact]
        public void Error_Title_Empty()
        {
            // Arrange
            var validator = new RegisterExpenseValidator();
            var request = new RequestRegisterExpenseJson
            {
                Title = string.Empty,
                Description = "Descrição válida",
                Amount = 50.00m,
                Date = DateTime.UtcNow.AddDays(-1),
                paymentType = PaymentType.Cash
            };

            // Act
            var result = validator.Validate(request);

            // Assert
            Assert.False(result.IsValid);
            Assert.Single(result.Errors);
            Assert.Contains(result.Errors, e => e.ErrorMessage == ResourceErrorMessages.TITLE_REQUIRED);
        }

        [Fact]
        public void Error_Title_Greater_Than_100_Characters()
        {
            // Arrange
            var validator = new RegisterExpenseValidator();
            var request = new RequestRegisterExpenseJson
            {
                Title = new string('A', 101),
                Description = "Descrição válida",
                Amount = 50.00m,
                Date = DateTime.UtcNow.AddDays(-1),
                paymentType = PaymentType.Cash
            };

            // Act
            var result = validator.Validate(request);

            // Assert
            Assert.False(result.IsValid);
            Assert.Single(result.Errors);
            Assert.Contains(result.Errors, e => e.ErrorMessage == ResourceErrorMessages.TITLE_LENGTH_GREATER_THAN_100);
        }

        [Fact]
        public void Error_Description_Greater_Than_500_Characters()
        {
            // Arrange
            var validator = new RegisterExpenseValidator();
            var request = new RequestRegisterExpenseJson
            {
                Title = "Título válido",
                Description = new string('B', 501),
                Amount = 50.00m,
                Date = DateTime.UtcNow.AddDays(-1),
                paymentType = PaymentType.DebitCard
            };

            // Act
            var result = validator.Validate(request);

            // Assert
            Assert.False(result.IsValid);
            Assert.Single(result.Errors);
            Assert.Contains(result.Errors, e => e.ErrorMessage == ResourceErrorMessages.DESCRIPTION_GREATER_THAN_500);
        }

        [Fact]
        public void Success_Description_Empty()
        {
            // Arrange
            var validator = new RegisterExpenseValidator();
            var request = new RequestRegisterExpenseJson
            {
                Title = "Título válido",
                Description = string.Empty,
                Amount = 50.00m,
                Date = DateTime.UtcNow.AddDays(-1),
                paymentType = PaymentType.Cash
            };

            // Act
            var result = validator.Validate(request);

            // Assert
            Assert.True(result.IsValid);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        [InlineData(-100.50)]
        public void Error_Amount_Must_Be_Greater_Than_Zero(decimal amount)
        {
            // Arrange
            var validator = new RegisterExpenseValidator();
            var request = new RequestRegisterExpenseJson
            {
                Title = "Título válido",
                Description = "Descrição válida",
                Amount = amount,
                Date = DateTime.UtcNow.AddDays(-1),
                paymentType = PaymentType.Cash
            };

            // Act
            var result = validator.Validate(request);

            // Assert
            Assert.False(result.IsValid);
            Assert.Single(result.Errors);
            Assert.Contains(result.Errors, e => e.ErrorMessage == ResourceErrorMessages.AMOUNT_MUST_BE_GREATER_THAN_0);
        }

        [Fact]
        public void Error_Date_Cannot_Be_In_Future()
        {
            // Arrange
            var validator = new RegisterExpenseValidator();
            var request = new RequestRegisterExpenseJson
            {
                Title = "Título válido",
                Description = "Descrição válida",
                Amount = 50.00m,
                Date = DateTime.UtcNow.AddDays(1),
                paymentType = PaymentType.CreditCard
            };

            // Act
            var result = validator.Validate(request);

            // Assert
            Assert.False(result.IsValid);
            Assert.Single(result.Errors);
            Assert.Contains(result.Errors, e => e.ErrorMessage == ResourceErrorMessages.DATE_CANNOT_BE_IN_FUTURE);
        }

        [Fact]
        public void Error_Invalid_Payment_Type()
        {
            // Arrange
            var validator = new RegisterExpenseValidator();
            var request = new RequestRegisterExpenseJson
            {
                Title = "Título válido",
                Description = "Descrição válida",
                Amount = 50.00m,
                Date = DateTime.UtcNow.AddDays(-1),
                paymentType = (PaymentType)999 // Valor inválido do enum
            };

            // Act
            var result = validator.Validate(request);

            // Assert
            Assert.False(result.IsValid);
            Assert.Single(result.Errors);
            Assert.Contains(result.Errors, e => e.ErrorMessage == ResourceErrorMessages.INVALID_PAYMENT_TYPE);
        }

        [Theory]
        [InlineData(PaymentType.Cash)]
        [InlineData(PaymentType.CreditCard)]
        [InlineData(PaymentType.DebitCard)]
        [InlineData(PaymentType.ElectronicTransfer)]
        public void Success_Valid_Payment_Types(PaymentType paymentType)
        {
            // Arrange
            var validator = new RegisterExpenseValidator();
            var request = new RequestRegisterExpenseJson
            {
                Title = "Título válido",
                Description = "Descrição válida",
                Amount = 50.00m,
                Date = DateTime.UtcNow.AddDays(-1),
                paymentType = paymentType
            };

            // Act
            var result = validator.Validate(request);

            // Assert
            Assert.True(result.IsValid);
        }

        [Fact]
        public void Error_Multiple_Validations()
        {
            // Arrange
            var validator = new RegisterExpenseValidator();
            var request = new RequestRegisterExpenseJson
            {
                Title = string.Empty,
                Description = new string('C', 501),
                Amount = -10.00m,
                Date = DateTime.UtcNow.AddDays(5),
                paymentType = (PaymentType)999
            };

            // Act
            var result = validator.Validate(request);

            // Assert
            Assert.False(result.IsValid);
            Assert.Equal(5, result.Errors.Count);
            Assert.Contains(result.Errors, e => e.ErrorMessage == ResourceErrorMessages.TITLE_REQUIRED);
            Assert.Contains(result.Errors, e => e.ErrorMessage == ResourceErrorMessages.DESCRIPTION_GREATER_THAN_500);
            Assert.Contains(result.Errors, e => e.ErrorMessage == ResourceErrorMessages.AMOUNT_MUST_BE_GREATER_THAN_0);
            Assert.Contains(result.Errors, e => e.ErrorMessage == ResourceErrorMessages.DATE_CANNOT_BE_IN_FUTURE);
            Assert.Contains(result.Errors, e => e.ErrorMessage == ResourceErrorMessages.INVALID_PAYMENT_TYPE);
        }
    }
}