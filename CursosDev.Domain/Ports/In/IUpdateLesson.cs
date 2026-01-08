using CursosDev.Domain.Entities;

namespace CursosDev.Domain.Ports.In
{
    public interface IUpdateLesson
    {
        public Lesson? updateLesson(Lesson lesson);
    }
}
