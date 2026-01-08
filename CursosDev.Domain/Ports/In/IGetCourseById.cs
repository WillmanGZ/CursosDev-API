using CursosDev.Domain.Entities;

namespace CursosDev.Domain.Ports.In
{
    public interface IGetCourseById
    {
        public Course? Execute(Guid id);
    }
}
