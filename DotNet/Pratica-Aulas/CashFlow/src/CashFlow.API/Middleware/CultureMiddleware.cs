using System.Globalization;

namespace CashFlow.API.Middleware;

public class CultureMiddleware
{
    private readonly RequestDelegate _next;
    public CultureMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {

        var supportedLanguages = CultureInfo.GetCultures(CultureTypes.AllCultures)
            //.Select(c => c.Name)
            .ToList();
        var requestedCulture = context.Request.Headers.AcceptLanguage.ToString();


        if (!string.IsNullOrWhiteSpace(requestedCulture)
            && supportedLanguages.Exists(l => l.Name.Equals(requestedCulture)))
        {
            var culture = new CultureInfo(requestedCulture);
            CultureInfo.CurrentCulture = culture;
            CultureInfo.CurrentUICulture = culture;
        }
        await _next(context);
    }
}
