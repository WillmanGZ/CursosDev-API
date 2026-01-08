using CursosDev.Domain.Entities;

namespace CursosDev.Domain.Ports.In
{
    public interface IGetCourses
    {
        public List<Course> Execute();
    }
}
