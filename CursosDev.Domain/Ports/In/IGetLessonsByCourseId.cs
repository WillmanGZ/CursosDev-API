using CursosDev.Domain.Entities;

namespace CursosDev.Domain.Ports.In
{
    public interface IGetLessonsByCourseId
    {
        public List<Lesson> Execute(Guid courseId);
    }
}
