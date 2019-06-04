using System;
using System.Runtime.Serialization;

[Serializable]
internal class PilhaCheiaException : Exception
{
    public PilhaCheiaException()
    {
    }

    public PilhaCheiaException(string message) : base(message)
    {
    }

    public PilhaCheiaException(string message, Exception innerException) : base(message, innerException)
    {
    }

    protected PilhaCheiaException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }
}