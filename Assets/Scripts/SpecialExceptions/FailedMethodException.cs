using System;

public class FailedMethodException : Exception
{
    public FailedMethodException() { }

    public FailedMethodException(string message) : base(message) { }

    public FailedMethodException(string message, Exception inner) : base(message, inner) { }

}