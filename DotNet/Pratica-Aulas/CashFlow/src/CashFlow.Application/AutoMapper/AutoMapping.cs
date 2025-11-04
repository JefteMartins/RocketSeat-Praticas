using AutoMapper;
using CashFlow.Communication.Requests;
using CashFlow.Communication.Response;
using CashFlow.Communication.Responses;
using CashFlow.Domain.Entities;

namespace CashFlow.Application.AutoMapper;

public class AutoMapping : Profile
{
    public AutoMapping()
    {
        RequestToEntity();
        EntityToResponse();
    }

    /// <summary>
    /// Configures AutoMapper to map from the RequestExpenseJson request DTO to the Expense domain entity.
    /// </summary>
    private void RequestToEntity()
    {
        CreateMap<RequestExpenseJson, Expense>();
    }

    /// <summary>
    /// Configures AutoMapper mappings from the Expense domain entity to the response DTOs.
    /// </summary>
    private void EntityToResponse()
    {
        CreateMap<Expense, ResponseRegisteredExpenseJson>();
        CreateMap<Expense, ResponseShortExpenseJson>();
        CreateMap<Expense, ResponseExpenseJson>();
    }
}