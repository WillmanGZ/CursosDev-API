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
            return _courseRepository.publishCourse(id);
        }
    }
}
