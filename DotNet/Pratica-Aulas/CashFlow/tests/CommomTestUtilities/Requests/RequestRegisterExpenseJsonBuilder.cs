using Bogus;
using CashFlow.Communication.Enums;
using CashFlow.Communication.Requests;
using System.ComponentModel.DataAnnotations;

namespace CommonTestUtilities.Requests;

public class RequestRegisterExpenseJsonBuilder
{
    /// <summary>
    /// Builds a RequestExpenseJson populated with randomized test data.
    /// </summary>
    /// <returns>A RequestExpenseJson populated with randomized values for Title, Description, Amount, Date, and paymentType.</returns>
    public static RequestExpenseJson Build()
    {
        return new Faker<RequestExpenseJson>()
            .RuleFor(r => r.Title, f => f.Commerce.ProductName())
            .RuleFor(r => r.Description, f => f.Lorem.Sentence(5))
            .RuleFor(r => r.Amount, f => f.Finance.Amount())
            .RuleFor(r => r.Date, f => f.Date.Past())
            .RuleFor(r => r.paymentType, f => f.PickRandom<PaymentType>())
            .Generate();
    }
}