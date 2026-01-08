namespace CursosDev.Domain.Exceptions
{
    public abstract class ResourceNotFoundException : Exception
    {
        protected ResourceNotFoundException(string message) : base(message)
        {
        }
    }
}