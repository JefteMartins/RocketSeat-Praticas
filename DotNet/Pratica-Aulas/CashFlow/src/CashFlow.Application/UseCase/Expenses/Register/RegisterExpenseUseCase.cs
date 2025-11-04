using AutoMapper;
using CashFlow.Communication.Requests;
using CashFlow.Communication.Response;
using CashFlow.Domain.Entities;
using CashFlow.Domain.Repositories;
using CashFlow.Domain.Repositories.Expenses;
using CashFlow.Exception.ExceptionBase;

namespace CashFlow.Application.UseCase.Expenses.Register;

public class RegisterExpenseUseCase : IRegisterExpenseUseCase
{

    private readonly IExpensesWriteOnlyRepository _repository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    /// <summary>
    /// Initializes a new instance of <see cref="RegisterExpenseUseCase"/> with the required dependencies.
    /// </summary>
    public RegisterExpenseUseCase(
        IExpensesWriteOnlyRepository repository,
        IUnitOfWork unitOfWork,
        IMapper mapper)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }
    /// <summary>
    /// Registers a new expense using the provided request and returns the registered expense representation.
    /// </summary>
    /// <param name="request">The expense data to register.</param>
    /// <returns>The registered expense mapped to a <c>ResponseRegisteredExpenseJson</c>.</returns>
    public async Task<ResponseRegisteredExpenseJson> Execute(RequestExpenseJson request) 
    {

        Validate(request);

        var entity = _mapper.Map<Expense>(request);

        await _repository.Add(entity);

        await _unitOfWork.Commit();

        return _mapper.Map<ResponseRegisteredExpenseJson>(entity) ;
    }

    /// <summary>
    /// Validates the provided expense request and enforces business/input rules.
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