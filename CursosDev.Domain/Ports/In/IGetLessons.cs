using CursosDev.Domain.Entities;

namespace CursosDev.Domain.Ports.In
{
    public interface IGetLessons
    {
        public List<Lesson> getLessons();
    }
}
