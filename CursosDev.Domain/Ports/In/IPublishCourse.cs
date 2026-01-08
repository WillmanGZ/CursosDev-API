namespace CursosDev.Domain.Ports.In
{
    public interface IPublishCourse
    {
        public bool Execute(Guid id);
    }
}
