using CashFlow.Domain.Entities;

namespace CashFlow.Domain.Repositories.Expenses;

public interface IExpensesUpdateOnlyRepository
{
    /// <summary>
/// Retrieves an expense by its identifier.
/// </summary>
/// <param name="id">The identifier of the expense to retrieve.</param>
/// <returns>The <see cref="Expense"/> with the specified identifier, or <c>null</c> if no matching expense exists.</returns>
Task<Expense?> GetById(long id);
    /// <summary>
/// Persists the provided Expense entity's updated state to the repository.
/// </summary>
/// <param name="expense">Expense entity containing the updated values to be saved.</param>
void Update(Expense expense);
}