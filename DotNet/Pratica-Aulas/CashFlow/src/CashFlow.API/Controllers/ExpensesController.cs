using CashFlow.Application.UseCase.Expenses.Register;
using CashFlow.Communication.Reponses;
using CashFlow.Communication.Requests;
using CashFlow.Exception.ExceptionBase;
using Microsoft.AspNetCore.Mvc;

namespace CashFlow.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ExpensesController : ControllerBase
{
    [HttpPost]
    public IActionResult Register([FromBody] RequestRegisterExpenseJson request)
    {
        try
        {
            var useCase = new RegisterExpenseUseCase();
            var response = useCase.Execute(request);
            return Created(string.Empty, response);
        }
        catch (ErrorOnValidationException ex)
        {
            var erroResponse = new ResponseErrorJson(ex.Errors);

            return BadRequest(erroResponse);
        }
        catch
        {
            var erroResponse = new ResponseErrorJson("Unkown error");

            return StatusCode(StatusCodes.Status500InternalServerError, erroResponse);
        }
        finally
        {
            // Any cleanup code can go here if needed
        }
    }
}
