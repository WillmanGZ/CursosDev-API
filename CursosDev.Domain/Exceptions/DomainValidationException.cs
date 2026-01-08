namespace CursosDev.Domain.Exceptions
{
    public abstract class DomainValidationException : Exception
    {
        protected DomainValidationException(string message) : base(message)
        {
        }
    }
}