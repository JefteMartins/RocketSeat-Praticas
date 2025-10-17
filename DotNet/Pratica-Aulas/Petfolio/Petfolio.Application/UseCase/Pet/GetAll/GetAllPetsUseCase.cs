using Petfolio.Communication.Enums;
using Petfolio.Communication.Response;

namespace Petfolio.Application.UseCase.Pet.GetAll;

public class GetAllPetsUseCase
{
    public ResponseAllPetsJson Execute()
    {
        return new ResponseAllPetsJson
        {
            Pets = new List<ResponseShortPetJson>
            {
                new ResponseShortPetJson
                {
                    Id = 1,
                    Name = "Buddy",
                    Type = PetType.Dog
                },
                new ResponseShortPetJson
                {
                    Id = 2,
                    Name = "Mittens",
                    Type = PetType.Cat
                }
            }
        };
    }
}
