using CursosDev.Domain.Entities;
using CursosDev.Domain.Ports.Out;
using CursosDev.Infrastructure.Persistence;

namespace CursosDev.Infrastructure
{
    public class CourseRepository : ICourseRepository
    {
        private readonly ApplicationDbContext _context;

        public CourseRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public List<Course> getCourses()
        {
            return _context.Courses.ToList();
        }

        public Course? getCourseById(Guid id)
        {
            return _context.Courses.Find(id);
        }

        public Course? createCourse(Course course)
        {
            _context.Courses.Add(course);
            _context.SaveChanges();

            return course;
        }

        public Course? updateCourse(Course course)
        {
            var local = _context.Courses.Local.FirstOrDefault(c => c.Id == course.Id);
            if (local != null)
            {
                _context.Entry(local).State = Microsoft.EntityFrameworkCore.EntityState.Detached;
            }

            _context.Courses.Update(course);
            _context.SaveChanges();

            return course;
        }

        public bool publishCourse(Guid id)
        {
            var course = _context.Courses.Find(id);

            if (course is null || course.IsDeleted) return false;

            course.Status = CourseStatus.Published;
            course.UpdatedAt = DateTime.UtcNow;

            _context.SaveChanges();
            return true;
        }

        public bool unpublishCourse(Guid id)
        {
            var course = _context.Courses.Find(id);

            if (course is null || course.IsDeleted) return false;

            course.Status = CourseStatus.Draft;
            course.UpdatedAt = DateTime.UtcNow;

            _context.SaveChanges();
            return true;
        }

        public bool deleteCourse(Guid id)
        {
            var course = _context.Courses.Find(id);

            if (course is null || course.IsDeleted) return false;

            course.IsDeleted = true;
            course.UpdatedAt = DateTime.UtcNow;

            _context.SaveChanges();

            return true;
        }
    }
}