﻿namespace CashFlow.Exception.ExceptionBase;

public class ErrorOnValidationException :  CashFlowException
{
    public List<string> Errors { get; init; }

    public ErrorOnValidationException(List<string> errorMesages) : base(string.Empty)
    {
        Errors = errorMesages;
    }
}
