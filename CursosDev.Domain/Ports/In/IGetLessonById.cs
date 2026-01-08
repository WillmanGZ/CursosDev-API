using CursosDev.Domain.Entities;

namespace CursosDev.Domain.Ports.In
{
    public interface IGetLessonById
    {
        public Lesson? Execute(Guid id);
    }
}
