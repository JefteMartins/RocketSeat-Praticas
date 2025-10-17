using Microsoft.AspNetCore.Mvc;
using Petfolio.Application.UseCase.Pet.Delete;
using Petfolio.Application.UseCase.Pet.GetAll;
using Petfolio.Application.UseCase.Pet.GetById;
using Petfolio.Application.UseCase.Pet.Register;
using Petfolio.Application.UseCase.Pet.Update;
using Petfolio.Communication.Request;
using Petfolio.Communication.Response;

namespace Petfolio.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PetController : ControllerBase
{
    [HttpPost]
    [ProducesResponseType(typeof(RequesPetJson), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ResponseErrosJson), StatusCodes.Status400BadRequest)]
    public IActionResult Register([FromBody] RequesPetJson request)
    {
        var useCase = new RegisterPetUseCase();

        var response = useCase.Execute(request);
        return Created(string.Empty, response);
    }
    [HttpPut]
    [Route("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ResponseErrosJson), StatusCodes.Status400BadRequest)]
    public IActionResult Update([FromRoute] int id, [FromBody] RequesPetJson request)
    {
        var useCase = new UpdatePetUseCase();

        useCase.Execute(id, request);
        return NoContent();
    }

    [HttpGet]
    [ProducesResponseType(typeof(List<ResponseAllPetsJson>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public IActionResult Get()
    {
        var useCase = new GetAllPetsUseCase();

        var response = useCase.Execute();

        if (response.Pets.Any())
            return Ok(response);

        return NoContent();

    }
    [HttpGet]
    [Route("{id}")]
    [ProducesResponseType(typeof(ResponsePetJson), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseErrosJson), StatusCodes.Status404NotFound)]
    public IActionResult GetById(int id)
    {
        var useCase = new GetPetByIdUseCase();
        var response = useCase.Execute(id);

        return Ok(response);
    }

    [HttpDelete]
    [Route("{id}")]
    [ProducesResponseType(typeof(ResponsePetJson), StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ResponseErrosJson), StatusCodes.Status404NotFound)]
    public IActionResult Delete(int id)
    {
        var useCase = new DeletePetByIdUseCase();
        useCase.Execute(id);

        return NoContent();
    }
}

