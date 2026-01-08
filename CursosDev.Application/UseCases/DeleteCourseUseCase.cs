using CursosDev.Domain.Ports.In;
using CursosDev.Domain.Ports.Out;

namespace CursosDev.Application.UseCases
{
    public class DeleteCourseUseCase: IDeleteCourse
    {
        private readonly ICourseRepository _courseRepository;

        public DeleteCourseUseCase(ICourseRepository courseRepository) {
            _courseRepository = courseRepository;
        }

        public bool Execute(Guid id)
        {
            return _courseRepository.deleteCourse(id);
        }
    }
}
