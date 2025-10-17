
using Petfolio.Communication.Enums;
using Petfolio.Communication.Response;

namespace Petfolio.Application.UseCase.Pet.GetById;
public class GetPetByIdUseCase
{
    public ResponsePetJson Execute(int id)
    {
        return new ResponsePetJson
        {
            Id = id,
            Name = "Buddy",
            Type = PetType.Dog,
            Birthday = new DateTime(2018, 1, 1)
        };
    }
}
