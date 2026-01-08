using CursosDev.Domain.Entities;

namespace CursosDev.Domain.Ports.Out
{
    public interface ILessonRepository
    {
        List<Lesson> GetLessons();
        Lesson? GetLessonById(Guid id);
        List<Lesson> GetLessonsByCourseId(Guid courseId);
        Lesson? CreateLesson(Lesson lesson);
        Lesson? UpdateLesson(Lesson lesson);
        bool DeleteLesson(Guid id);
    }
}
