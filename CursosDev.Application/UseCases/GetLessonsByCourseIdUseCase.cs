using CursosDev.Domain.Entities;
using CursosDev.Domain.Ports.In;
using CursosDev.Domain.Ports.Out;

namespace CursosDev.Application.UseCases
{
    public class GetLessonsByCourseIdUseCase : IGetLessonsByCourseId
    {
        private readonly ILessonRepository _lessonRepository;

        public GetLessonsByCourseIdUseCase(ILessonRepository lessonRepository)
        {
            _lessonRepository = lessonRepository;
        }

        public List<Lesson> Execute(Guid courseId)
        {
            return _lessonRepository.GetLessonsByCourseId(courseId);
        }
    }
}
