using CursosDev.Application.DTOs;
using CursosDev.Domain.Entities;
using CursosDev.Domain.Ports.In;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CursosDev.Api.Controllers
{
    [ApiController]
    [Route("api/lessons")]
    [Authorize]
    public class LessonsController : ControllerBase
    {
        private readonly IGetLessons _getLessonsUseCase;
        private readonly IGetLessonById _getLessonByIdUseCase;
        private readonly IGetLessonsByCourseId _getLessonsByCourseIdUseCase;
        private readonly ICreateLesson _createLessonUseCase;
        private readonly IUpdateLesson _updateLessonUseCase;
        private readonly IDeleteLesson _deleteLessonUseCase;

        public LessonsController(
            IGetLessons getLessonsUseCase,
            IGetLessonById getLessonByIdUseCase,
            IGetLessonsByCourseId getLessonsByCourseIdUseCase,
            ICreateLesson createLessonUseCase,
            IUpdateLesson updateLessonUseCase,
            IDeleteLesson deleteLessonUseCase)
        {
            _getLessonsUseCase = getLessonsUseCase;
            _getLessonByIdUseCase = getLessonByIdUseCase;
            _getLessonsByCourseIdUseCase = getLessonsByCourseIdUseCase;
            _createLessonUseCase = createLessonUseCase;
            _updateLessonUseCase = updateLessonUseCase;
            _deleteLessonUseCase = deleteLessonUseCase;
        }

        [HttpGet]
        [ProducesResponseType(typeof(List<Lesson>), 200)]
        public IActionResult GetAll()
        {
            var lessons = _getLessonsUseCase.Execute();
            return Ok(lessons);
        }

        [HttpGet("{id:guid}")]
        [ProducesResponseType(typeof(Lesson), 200)]
        [ProducesResponseType(404)]
        public IActionResult GetById(Guid id)
        {
            var lesson = _getLessonByIdUseCase.Execute(id);
            if (lesson == null) return NotFound(new { message = "Lesson not found" });

            return Ok(lesson);
        }

        [HttpGet("by-course/{courseId:guid}")]
        [ProducesResponseType(typeof(List<Lesson>), 200)]
        public IActionResult GetByCourseId(Guid courseId)
        {
            var lessons = _getLessonsByCourseIdUseCase.Execute(courseId);
            return Ok(lessons);
        }

        [HttpPost]
        [ProducesResponseType(typeof(Lesson), 201)]
        [ProducesResponseType(400)]
        public IActionResult Create([FromBody] CreateLessonDto request)
        {
            if (string.IsNullOrWhiteSpace(request.Title))
                return BadRequest(new { message = "The title must be provided" });

            var lesson = new Lesson
            {
                Id = Guid.NewGuid(),
                CourseId = request.CourseId,
                Title = request.Title,
                Order = request.Order,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            var createdLesson = _createLessonUseCase.Execute(lesson);

            return CreatedAtAction(nameof(GetById), new { id = createdLesson?.Id }, createdLesson);
        }

        [HttpPut("{id:guid}")]
        [ProducesResponseType(typeof(Lesson), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult Update(Guid id, [FromBody] UpdateLessonDto request)
        {
            if (id != request.Id) return BadRequest("ID mismatch");

            var lesson = new Lesson
            {
                Id = request.Id,
                Title = request.Title,
                Order = request.Order,
                UpdatedAt = DateTime.UtcNow
            };

            var updatedLesson = _updateLessonUseCase.Execute(lesson);

            if (updatedLesson == null) return NotFound();

            return Ok(updatedLesson);
        }

        [HttpDelete("{id:guid}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public IActionResult Delete(Guid id)
        {
            var success = _deleteLessonUseCase.Execute(id);

            if (!success) return NotFound();

            return Ok();
        }
    }
}
