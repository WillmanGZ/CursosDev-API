using CursosDev.Domain.Entities;
using CursosDev.Domain.Ports.In;
using CursosDev.Domain.Ports.Out;

namespace CursosDev.Application.UseCases
{
    public class GetLessonsUseCase : IGetLessons
    {
        private readonly ILessonRepository _lessonRepository;

        public GetLessonsUseCase(ILessonRepository lessonRepository)
        {
            _lessonRepository = lessonRepository;
        }

        public List<Lesson> Execute()
        {
            return _lessonRepository.GetLessons();
        }
    }
}
