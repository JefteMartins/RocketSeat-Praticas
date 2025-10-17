using Petfolio.Communication.Enums;

namespace Petfolio.Communication.Request;

public class RequesPetJson
{
    public string name { get; set; } = string.Empty;
    public DateTime birthDate { get; set; }
    public PetType type { get; set; }
}
