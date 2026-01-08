namespace CursosDev.Domain.Ports.In
{
    public interface IDeleteLesson
    {
        public bool Execute(Guid id);
    }
}
