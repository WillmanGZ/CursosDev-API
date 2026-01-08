namespace CursosDev.Domain.Ports.In
{
    public interface IUnpublishCourse
    {
        public bool Execute(Guid id);
    }
}
