namespace CursosDev.Domain.Entities
{
    public enum CourseStatus
    {
        Draft,
        Published
    }

    public class Course
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public CourseStatus Status { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public ICollection<Lesson> Lessons { get; set; } = new List<Lesson>();
    }
}