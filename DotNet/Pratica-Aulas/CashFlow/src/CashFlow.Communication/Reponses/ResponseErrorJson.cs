namespace CashFlow.Communication.Reponses
{
    public class ResponseErrorJson
    {
        public List<string> ErrorMessages { get; init; } = new List<string>();

        public ResponseErrorJson(List<string> errorMessage)
        {
            ErrorMessages = errorMessage;
        }

        public ResponseErrorJson(string errorMessage)
        {
            ErrorMessages = [errorMessage];
        }
    }
}
