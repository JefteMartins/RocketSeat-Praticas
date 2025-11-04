using AutoMapper;
using CashFlow.Application.UseCase.Expenses.Register;
using CashFlow.Communication.Requests;
using CashFlow.Domain.Entities;
using CashFlow.Domain.Repositories;
using CashFlow.Domain.Repositories.Expenses;
using CashFlow.Exception;
using CashFlow.Exception.ExceptionBase;
using Microsoft.IdentityModel.Tokens.Experimental;

namespace CashFlow.Application.UseCase.Expenses.Update;

public class UpdateExpenseUseCase : IUpdateExpenseUseCase
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IExpensesUpdateOnlyRepository _repository;

    /// <summary>
    /// Initializes a new instance of <see cref="UpdateExpenseUseCase"/> with the required dependencies.
    /// </summary>
    /// <param name="mapper">Mapper used to map request DTOs onto domain expense entities.</param>
    /// <param name="unitOfWork">Unit of work used to commit repository changes.</param>
    /// <param name="repository">Repository used for retrieving and updating expenses.</param>
    public UpdateExpenseUseCase(IMapper mapper, IUnitOfWork unitOfWork, IExpensesUpdateOnlyRepository repository)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
        _repository = repository;
    }
    /// <summary>
    /// Updates an existing expense with values from the provided request.
    /// </summary>
    /// <param name="id">Identifier of the expense to update.</param>
    /// <param name="request">Request payload containing the updated expense properties.</param>
    /// <exception cref="ErrorOnValidationException">Thrown when the request fails validation; contains validation error messages.</exception>
    /// <exception cref="NotFoundException">Thrown when no expense with the given <paramref name="id"/> exists.</exception>
    public async Task Execute(long id, RequestExpenseJson request)
    {
        Validate(request);

        var expense = await _repository.GetById(id);

        if (expense is null)
            throw new NotFoundException(ResourceErrorMessages.EXPENSE_NOT_FOUND);

        _mapper.Map(request, expense);
        _repository.Update(expense);

        await _unitOfWork.Commit();
    }

    /// <summary>
    /// Validates the provided expense request and ensures it meets business rules.
    /// </summary>
    /// <param name="request">The expense request to validate.</param>
    /// <exception cref="ErrorOnValidationException">Thrown when validation fails; contains a list of validation error messages.</exception>
    private void Validate(RequestExpenseJson request)
    {
        var validator = new ExpenseValidator();
        var result = validator.Validate(request);

        if (!result.IsValid)
        {
            var errorMessages = result.Errors.Select(e => e.ErrorMessage).ToList();
            throw new ErrorOnValidationException(errorMessages);
        }
    }
}