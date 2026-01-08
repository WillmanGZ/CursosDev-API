using CursosDev.Application.DTOs;
using CursosDev.Domain.Entities;
using CursosDev.Domain.Ports.In;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CursosDev.Api.Controllers
{
    [ApiController]
    [Route("api/courses")]
    [Authorize]
    public class CoursesController : ControllerBase
    {
        private readonly IGetCourses _getCoursesUseCase;
        private readonly IGetCourseById _getCourseByIdUseCase;
        private readonly ICreateCourse _createCourseUseCase;
        private readonly IUpdateCourse _updateCourseUseCase;
        private readonly IPublishCourse _publishCourseUseCase;
        private readonly IUnpublishCourse _unpublishCourseUseCase;
        private readonly IDeleteCourse _deleteCourseUseCase;

        public CoursesController(
            IGetCourses getCoursesUseCase,
            IGetCourseById getCourseByIdUseCase,
            ICreateCourse createCourseUseCase,
            IUpdateCourse updateCourseUseCase,
            IPublishCourse publishCourseUseCase,
            IUnpublishCourse unpublishCourseUseCase,
            IDeleteCourse deleteCourseUseCase)
        {
            _getCoursesUseCase = getCoursesUseCase;
            _getCourseByIdUseCase = getCourseByIdUseCase;
            _createCourseUseCase = createCourseUseCase;
            _updateCourseUseCase = updateCourseUseCase;
            _publishCourseUseCase = publishCourseUseCase;
            _unpublishCourseUseCase = unpublishCourseUseCase;
            _deleteCourseUseCase = deleteCourseUseCase;
        }

        [HttpGet]
        [ProducesResponseType(typeof(List<Course>), 200)]
        public IActionResult GetAll()
        {
            var courses = _getCoursesUseCase.Execute();
            return Ok(courses);
        }

        [HttpGet("{id:guid}")]
        [ProducesResponseType(typeof(Course), 200)]
        [ProducesResponseType(404)]
        public IActionResult GetById(Guid id)
        {
            var course = _getCourseByIdUseCase.Execute(id);
            if (course == null) return NotFound(new { message = "Course not found" });

            return Ok(course);
        }

        [HttpPost]
        [ProducesResponseType(typeof(Course), 201)]
        [ProducesResponseType(400)]
        public IActionResult Create([FromBody] CreateCourseDto request)
        {
            if (string.IsNullOrWhiteSpace(request.Title))
                return BadRequest(new { message = "The title must be provided" });

            var course = new Course
            {
                Id = Guid.NewGuid(),
                Title = request.Title,
                Status = CourseStatus.Draft,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            var createdCourse = _createCourseUseCase.Execute(course);

            return CreatedAtAction(nameof(GetById), new { id = createdCourse?.Id }, createdCourse);
        }

        [HttpPut("{id:guid}")]
        [ProducesResponseType(typeof(Course), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult Update(Guid id, [FromBody] UpdateCourseDto request)
        {
            if (id != request.Id) return BadRequest("ID mismatch");

            var course = new Course
            {
                Id = request.Id,
                Title = request.Title,
                UpdatedAt = DateTime.UtcNow
            };

            var updatedCourse = _updateCourseUseCase.Execute(course);

            if (updatedCourse == null) return NotFound();

            return Ok(updatedCourse);
        }

        [HttpPatch("{id:guid}/publish")]
        public IActionResult Publish(Guid id)
        {
            try
            {
                var success = _publishCourseUseCase.Execute(id);
                if (!success) return NotFound();
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPatch("{id:guid}/unpublish")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public IActionResult Unpublish(Guid id)
        {
            var success = _unpublishCourseUseCase.Execute(id);

            if (!success) return NotFound();

            return Ok();
        }

        [HttpDelete("{id:guid}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public IActionResult Delete(Guid id)
        {
            var success = _deleteCourseUseCase.Execute(id);

            if (!success) return NotFound();

            return Ok();
        }
    }
}