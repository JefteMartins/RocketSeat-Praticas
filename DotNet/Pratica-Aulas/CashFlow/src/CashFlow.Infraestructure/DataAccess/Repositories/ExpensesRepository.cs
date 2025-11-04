using CashFlow.Domain.Entities;
using CashFlow.Domain.Repositories.Expenses;
using Microsoft.EntityFrameworkCore;

namespace CashFlow.Infraestructure.DataAccess.Repositories;

internal class ExpensesRepository : IExpensesReadOnlyRepository, IExpensesWriteOnlyRepository, IExpensesUpdateOnlyRepository
{
    private readonly CashFlowDbContext _dbContext;
    /// <summary>
    /// Initializes a new instance of ExpensesRepository using the provided database context.
    /// </summary>
    /// <param name="dbContext">The CashFlowDbContext used to access and persist expense data.</param>
    public ExpensesRepository(CashFlowDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task Add(Expense expense)
    {

        await _dbContext.Expenses.AddAsync(expense);
    }

    /// <summary>
    /// Retrieve all expense records from the database.
    /// </summary>
    /// <returns>A list of all <see cref="Expense"/> records; an empty list if none exist.</returns>
    public async Task<List<Expense>> GetAll()
    {
        return await _dbContext.Expenses.AsNoTracking().ToListAsync();
    }

    /// <summary>
    /// Retrieves an expense by its identifier using a read-only query.
    /// </summary>
    /// <returns>The matching Expense if found, otherwise null.</returns>
    async Task<Expense?> IExpensesReadOnlyRepository.GetById(long id)
    {
        return await _dbContext.Expenses.AsNoTracking().FirstOrDefaultAsync(e => e.Id == id);
    }

    /// <summary>
    /// Retrieves an expense by its identifier for update operations.
    /// </summary>
    /// <param name="id">The identifier of the expense to retrieve.</param>
    /// <returns>The matching <see cref="Expense"/> if found; otherwise <c>null</c>.</returns>
    async Task<Expense?> IExpensesUpdateOnlyRepository.GetById(long id)
    {
        return await _dbContext.Expenses.FirstOrDefaultAsync(e => e.Id == id);
    }

    /// <summary>
    /// Deletes the expense with the specified id from the DbContext.
    /// </summary>
    /// <param name="id">The identifier of the expense to remove.</param>
    /// <returns>True if the expense was found and removed, false otherwise.</returns>
    public async Task<bool> Delete(long id)
    {
        var expense = await _dbContext.Expenses.FirstOrDefaultAsync(e => e.Id == id);
        if (expense == null)
        {
            return false;
        }
        _dbContext.Expenses.Remove(expense);
        return true;
    }

    /// <summary>
    /// Marks an existing expense entity as modified in the repository's DbContext so its changes are persisted on save.
    /// </summary>
    /// <param name="expense">The expense entity containing updated values to be tracked for persistence.</param>
    public void Update(Expense expense)
    {
        _dbContext.Expenses.Update(expense);
    }
}