namespace MyDoctor.Application.Exceptions
{
    public class BaseMapperException : Exception
    {
        public BaseMapperException() : base("Issue with the mapper") { }
    }
}
