namespace CursosDev.Application.DTOs
{
    public class UpdateLessonDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public int Order { get; set; }
    }
}
