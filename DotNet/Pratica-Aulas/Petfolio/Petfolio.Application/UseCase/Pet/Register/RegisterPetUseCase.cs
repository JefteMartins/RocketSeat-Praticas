using Petfolio.Communication.Request;
using Petfolio.Communication.Response;
using System.Net.Http.Headers;
using System.Reflection.Metadata.Ecma335;

namespace Petfolio.Application.UseCase.Pet.Register;

public class RegisterPetUseCase
{
    public ResponseRegisteredPetJson Execute(RequesPetJson request)
    {
        return new ResponseRegisteredPetJson
        {
            Id = 1,
            Name = request.name
        };
    }

}
