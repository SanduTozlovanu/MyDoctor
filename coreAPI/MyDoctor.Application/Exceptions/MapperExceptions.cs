using System.Runtime.Serialization;

namespace MyDoctor.Application.Exceptions
{
    [Serializable]
    public class BaseMapperException : Exception
    {
        public BaseMapperException() : base("Issue with the mapper") { }
        protected BaseMapperException(SerializationInfo info, StreamingContext context) : base(info, context) { }

    }
}
