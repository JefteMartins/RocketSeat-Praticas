using CashFlow.Communication.Enums;
using CashFlow.Communication.Requests;
using CashFlow.Exception.ExceptionBase;

namespace CashFlow.Application.UseCase.Expenses.Register;

public class RegisterExpenseUseCase
{
    public RequestRegisterExpenseJson Execute(RequestRegisterExpenseJson request) 
    {

        Validate(request);

        return new RequestRegisterExpenseJson();
    }

    private void Validate(RequestRegisterExpenseJson request)
    {
        var validator = new RegisterExpenseValidator();

        var result = validator.Validate(request);


        if (!result.IsValid)
        {
            var errorMessages = result.Errors.Select(e => e.ErrorMessage).ToList();
            throw new ErrorOnValidationException(errorMessages);
        }
    }
}
