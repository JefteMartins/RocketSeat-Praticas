using CashFlow.Communication.Reponses;
using CashFlow.Communication.Requests;

namespace CashFlow.Application.UseCase.Expenses.Register;

public interface IRegisterExpenseUseCase
{
    Task<ResponseRegisteredExpenseJson> Execute(RequestRegisterExpenseJson request);
}
