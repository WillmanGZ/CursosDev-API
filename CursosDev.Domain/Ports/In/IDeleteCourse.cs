namespace CursosDev.Domain.Ports.In
{
    public interface IDeleteCourse
    {
        public bool Execute(Guid id);
    }
}
