using CursosDev.Domain.Entities;
using CursosDev.Domain.Exceptions;
using CursosDev.Domain.Ports.In;
using CursosDev.Domain.Ports.Out;

namespace CursosDev.Application.UseCases
{
    public class UpdateLessonUseCase : IUpdateLesson
    {
        private readonly ILessonRepository _lessonRepository;

        public UpdateLessonUseCase(ILessonRepository lessonRepository)
        {
            _lessonRepository = lessonRepository;
        }

        public Lesson? Execute(Lesson lesson)
        {
            var existingLesson = _lessonRepository.GetLessonById(lesson.Id);

            if (existingLesson is null)
            {
                throw new ResourceNotFoundException("Lesson not found");
            }

            return _lessonRepository.UpdateLesson(lesson);
        }
    }
}
