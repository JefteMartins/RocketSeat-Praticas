using Petfolio.Communication.Request;
using Petfolio.Communication.Response;

namespace Petfolio.Application.UseCase.Pet.Update;

public class UpdatePetUseCase
{
    public ResponseRegisteredPetJson Execute(int id, RequesPetJson request)
    {
        return new ResponseRegisteredPetJson
        {
            Id = 1,
            Name = request.name
        };
    }
}
