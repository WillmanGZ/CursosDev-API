using CursosDev.Domain.Entities;
using CursosDev.Domain.Exceptions;
using CursosDev.Domain.Ports.In;
using CursosDev.Domain.Ports.Out;

namespace CursosDev.Application.UseCases
{
    public class UpdateCourseUseCase: IUpdateCourse
    {
        private readonly ICourseRepository _courseRepository;

        public UpdateCourseUseCase(ICourseRepository courseRepository)
        {
            _courseRepository = courseRepository;
        }

        public Course? Execute(Course course)
        {
            var courseExists = _courseRepository.getCourseById(course.Id);

            if (courseExists is null)
            {
                throw new ResourceNotFoundException("Course not found");
            }

            return _courseRepository.updateCourse(course);
        }
    }
}
