using CashFlow.Communication.Requests;

namespace CashFlow.Application.UseCase.Expenses.Register;

public interface IRegisterExpenseUseCase
{
    RequestRegisterExpenseJson Execute(RequestRegisterExpenseJson request);
}
