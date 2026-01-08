using CursosDev.Domain.Entities;
using CursosDev.Domain.Ports.In;
using CursosDev.Domain.Ports.Out;

namespace CursosDev.Application.UseCases
{
    public class CreateLessonUseCase : ICreateLesson
    {
        private readonly ILessonRepository _lessonRepository;

        public CreateLessonUseCase(ILessonRepository lessonRepository)
        {
            _lessonRepository = lessonRepository;
        }

        public Lesson? Execute(Lesson lesson)
        {
            if (lesson == null)
            {
                throw new ArgumentException("Lesson information must be provided", nameof(lesson));
            }

            if (string.IsNullOrWhiteSpace(lesson.Title))
            {
                throw new ArgumentException("Lesson title must be provided", nameof(lesson.Title));
            }

            return _lessonRepository.CreateLesson(lesson);
        }
    }
}
