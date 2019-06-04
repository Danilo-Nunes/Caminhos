using System;
using System.Runtime.Serialization;

[Serializable]
internal class FilaVaziaException : Exception
{
    public FilaVaziaException()
    {
    }

    public FilaVaziaException(string message) : base(message)
    {
    }

    public FilaVaziaException(string message, Exception innerException) : base(message, innerException)
    {
    }

    protected FilaVaziaException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }
}