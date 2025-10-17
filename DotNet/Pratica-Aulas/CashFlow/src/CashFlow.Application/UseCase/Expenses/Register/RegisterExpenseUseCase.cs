using CashFlow.Communication.Enums;
using CashFlow.Communication.Requests;

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

        var errorMessages = result.Errors.Select(e => e.ErrorMessage).ToList();

        if (!result.IsValid)
        {
            throw new ArgumentException(string.Join("; ", errorMessages));
        }
    }
}
