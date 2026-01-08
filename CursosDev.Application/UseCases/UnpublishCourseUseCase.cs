using CursosDev.Domain.Ports.In;
using CursosDev.Domain.Ports.Out;

namespace CursosDev.Application.UseCases
{
    public class UnpublishCourseUseCase: IUnpublishCourse
    {
        private readonly ICourseRepository _courseRepository;

        public UnpublishCourseUseCase(ICourseRepository courseRepository) {
            _courseRepository = courseRepository;
        }

        public bool Execute(Guid id)
        {
            return _courseRepository.unpublishCourse(id);
        }
    }
}
