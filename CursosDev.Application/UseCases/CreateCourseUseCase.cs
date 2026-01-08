using CursosDev.Domain.Entities;
using CursosDev.Domain.Ports.In;
using CursosDev.Domain.Ports.Out;

namespace CursosDev.Application.UseCases
{
    public class CreateCourseUseCase: ICreateCourse
    {
        private readonly ICourseRepository _courseRepository;

        public CreateCourseUseCase(ICourseRepository courseRepository)
        {
            _courseRepository = courseRepository;
        }

        public Course? Execute(Course course)
        {
            if (course == null)
            {
                throw new ArgumentException("Course information must be provided", nameof(course));
            }

            if (string.IsNullOrWhiteSpace(course.Title))
            {
                throw new ArgumentException("Course title must be provided", nameof(course.Title));
            }

            return _courseRepository.createCourse(course);
        }
    }
}
