namespace CashFlow.Exception.ExceptionBase;

public class NotFoundException : CashFlowException
{
    public NotFoundException(string Message) : base(Message)
    {
    }
}
