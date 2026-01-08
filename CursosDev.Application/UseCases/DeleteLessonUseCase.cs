using CursosDev.Domain.Ports.In;
using CursosDev.Domain.Ports.Out;

namespace CursosDev.Application.UseCases
{
    public class DeleteLessonUseCase : IDeleteLesson
    {
        private readonly ILessonRepository _lessonRepository;

        public DeleteLessonUseCase(ILessonRepository lessonRepository)
        {
            _lessonRepository = lessonRepository;
        }

        public bool Execute(Guid id)
        {
            return _lessonRepository.DeleteLesson(id);
        }
    }
}
