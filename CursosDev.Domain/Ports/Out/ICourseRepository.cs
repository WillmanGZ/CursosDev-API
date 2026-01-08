using CursosDev.Domain.Entities;

namespace CursosDev.Domain.Ports.Out
{
    public interface ICourseRepository
    {
        public List<Course> getCourses();
        public Course? getCourseById(Guid id);
        public Course? createCourse(Course course);
        public Course? updateCourse(Course course);
        public bool publishCourse(Guid id);
        public bool unpublishCourse(Guid id);
        public bool deleteCourse(Guid id);
    }
}
