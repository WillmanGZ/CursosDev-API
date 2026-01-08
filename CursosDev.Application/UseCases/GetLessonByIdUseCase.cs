using CursosDev.Domain.Entities;
using CursosDev.Domain.Ports.In;
using CursosDev.Domain.Ports.Out;

namespace CursosDev.Application.UseCases
{
    public class GetLessonByIdUseCase : IGetLessonById
    {
        private readonly ILessonRepository _lessonRepository;

        public GetLessonByIdUseCase(ILessonRepository lessonRepository)
        {
            _lessonRepository = lessonRepository;
        }

        public Lesson? Execute(Guid id)
        {
            return _lessonRepository.GetLessonById(id);
        }
    }
}
