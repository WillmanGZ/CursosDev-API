using CursosDev.Domain.Entities;

namespace CursosDev.Domain.Ports.In
{
    public interface IUpdateCourse
    {
        public Course? Execute(Course course);
    }
}
