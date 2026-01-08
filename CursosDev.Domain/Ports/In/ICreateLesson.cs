using CursosDev.Domain.Entities;

namespace CursosDev.Domain.Ports.In
{
    public interface ICreateLesson
    {
        public Lesson? CreateLesson(Lesson lesson);
    }
}
