using CashFlow.Communication.Requests;
using CashFlow.Communication.Response;

namespace CashFlow.Application.UseCase.Expenses.Register;

public interface IRegisterExpenseUseCase
{
    /// <summary>
/// Registers a new expense using the provided request and returns the registered expense details.
/// </summary>
/// <param name="request">The expense data to register.</param>
/// <returns>A <see cref="ResponseRegisteredExpenseJson"/> with the stored expense's identifier and related details.</returns>
Task<ResponseRegisteredExpenseJson> Execute(RequestExpenseJson request);
}