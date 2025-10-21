using Bogus;
using CashFlow.Communication.Enums;
using CashFlow.Communication.Requests;
using System.ComponentModel.DataAnnotations;

namespace CommomTestUtilities.Requests;

public class RequestRegisterExpenseJsonBuilder
{
    public static RequestRegisterExpenseJson Build()
    {
        return new Faker<RequestRegisterExpenseJson>()
            .RuleFor(r => r.Title, f => f.Commerce.ProductName())
            .RuleFor(r => r.Description, f => f.Lorem.Sentence(5))
            .RuleFor(r => r.Amount, f => f.Finance.Amount())
            .RuleFor(r => r.Date, f => f.Date.Past())
            .RuleFor(r => r.paymentType, f => f.PickRandom<PaymentType>())
            .Generate();
    }
}
