using CashFlow.Communication.Requests;

namespace CashFlow.Application.UseCase.Expenses.Update;

public interface IUpdateExpenseUseCase
{
    /// <summary>
/// Updates an existing expense using the provided update payload.
/// </summary>
/// <param name="id">Identifier of the expense to update.</param>
/// <param name="request">Payload containing the fields to update for the expense.</param>
Task Execute(long id, RequestExpenseJson request);
}