using CursosDev.Domain.Entities;
using CursosDev.Domain.Ports.In;
using CursosDev.Domain.Ports.Out;

namespace CursosDev.Application.UseCases
{
    public class GetCourseByIdUseCase: IGetCourseById
    {
        private readonly ICourseRepository courseRepository;

        public GetCourseByIdUseCase(ICourseRepository _courseRepository)
        {
            courseRepository = _courseRepository;
        }

        public Course? Execute(Guid id)
        {
            if (id == Guid.Empty)
            {
                throw new ArgumentException("Id must be provided", nameof(id));
            }

            return courseRepository.getCourseById(id);
        }
    }
}
