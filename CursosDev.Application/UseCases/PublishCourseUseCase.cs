using CursosDev.Domain.Ports.In;
using CursosDev.Domain.Ports.Out;

namespace CursosDev.Application.UseCases
{
    public class PublishCourseUseCase: IPublishCourse
    {
        private readonly ICourseRepository _courseRepository;

        public PublishCourseUseCase(ICourseRepository courseRepository)
        {
            _courseRepository = courseRepository;
        }

        public bool Execute(Guid id)
        {
            var course = _courseRepository.getCourseById(id);
            if (course == null) return false;

            if (course.Lessons == null || !course.Lessons.Any())
            {
                throw new InvalidOperationException("Cannot publish a course without lessons.");
            }

            return _courseRepository.publishCourse(id);
        }
    }
}
