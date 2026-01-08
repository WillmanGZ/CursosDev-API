using CursosDev.Domain.Entities;

namespace CursosDev.Domain.Ports.In
{
    public interface ICreateCourse
    {
        public Course? Execute(Course course);
    }
}
