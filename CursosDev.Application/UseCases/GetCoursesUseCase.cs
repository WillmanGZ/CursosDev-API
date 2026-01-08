using CursosDev.Domain.Entities;
using CursosDev.Domain.Ports.In;
using CursosDev.Domain.Ports.Out;

namespace CursosDev.Application.UseCases
{
    public class GetCoursesUseCase : IGetCourses
    {
        private readonly ICourseRepository courseRepository;

        public GetCoursesUseCase(ICourseRepository _courseRepository)
        {
            courseRepository = _courseRepository;
        }

        public List<Course> Execute()
        {
            return courseRepository.getCourses();
        }
    }
}
