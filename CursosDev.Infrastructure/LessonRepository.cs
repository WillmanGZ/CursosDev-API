using CursosDev.Domain.Entities;
using CursosDev.Domain.Ports.Out;
using CursosDev.Infrastructure.Persistence;

namespace CursosDev.Infrastructure
{
    public class LessonRepository : ILessonRepository
    {
        private readonly ApplicationDbContext _context;

        public LessonRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public List<Lesson> GetLessons()
        {
            return _context.Lessons.Where(l => !l.IsDeleted).ToList();
        }

        public Lesson? GetLessonById(Guid id)
        {
            return _context.Lessons.Find(id);
        }

        public List<Lesson> GetLessonsByCourseId(Guid courseId)
        {
            return _context.Lessons
                .Where(l => l.CourseId == courseId && !l.IsDeleted)
                .OrderBy(l => l.Order)
                .ToList();
        }

        public Lesson? CreateLesson(Lesson lesson)
        {
            _context.Lessons.Add(lesson);
            _context.SaveChanges();

            return lesson;
        }

        public Lesson? UpdateLesson(Lesson lesson)
        {
            var existingLesson = _context.Lessons.Find(lesson.Id);
            if (existingLesson is null || existingLesson.IsDeleted) return null;

            existingLesson.Title = lesson.Title;
            existingLesson.Order = lesson.Order;
            existingLesson.UpdatedAt = DateTime.UtcNow;

            _context.SaveChanges();
            return existingLesson;
        }

        public bool DeleteLesson(Guid id)
        {
            var lesson = _context.Lessons.Find(id);

            if (lesson is null || lesson.IsDeleted) return false;

            lesson.IsDeleted = true;
            lesson.UpdatedAt = DateTime.UtcNow;

            _context.SaveChanges();

            return true;
        }
    }
}
