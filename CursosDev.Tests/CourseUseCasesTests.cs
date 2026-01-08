using Moq;
using CursosDev.Application.UseCases;
using CursosDev.Domain.Entities;
using CursosDev.Domain.Ports.Out;

namespace CursosDev.Tests
{
    public class CourseUseCasesTests
    {
        private readonly Mock<ICourseRepository> _courseRepositoryMock;
        private readonly PublishCourseUseCase _publishCourseUseCase;
        private readonly DeleteCourseUseCase _deleteCourseUseCase;

        public CourseUseCasesTests()
        {
            _courseRepositoryMock = new Mock<ICourseRepository>();
            _publishCourseUseCase = new PublishCourseUseCase(_courseRepositoryMock.Object);
            _deleteCourseUseCase = new DeleteCourseUseCase(_courseRepositoryMock.Object);
        }

        [Fact]
        public void PublishCourse_WithLessons_ShouldSucceed()
        {
            // Arrange
            var courseId = Guid.NewGuid();
            var course = new Course 
            { 
                Id = courseId, 
                Title = "Test Course",
                Status = CourseStatus.Draft,
                Lessons = new List<Lesson> { new Lesson { Id = Guid.NewGuid(), Title = "Intro" } }
            };

            _courseRepositoryMock.Setup(r => r.getCourseById(courseId)).Returns(course);
            _courseRepositoryMock.Setup(r => r.publishCourse(courseId)).Returns(true);

            // Act
            var result = _publishCourseUseCase.Execute(courseId);

            // Assert
            Assert.True(result);
            _courseRepositoryMock.Verify(r => r.publishCourse(courseId), Times.Once);
        }

        [Fact]
        public void PublishCourse_WithoutLessons_ShouldFail()
        {
            // Arrange
            var courseId = Guid.NewGuid();
            var course = new Course 
            { 
                Id = courseId, 
                Title = "Test Course",
                Status = CourseStatus.Draft,
                Lessons = new List<Lesson>() // Empty list
            };

            _courseRepositoryMock.Setup(r => r.getCourseById(courseId)).Returns(course);

            // Act & Assert
            // Expecting an exception or false. Assuming exception for validation failure.
            var exception = Assert.Throws<InvalidOperationException>(() => _publishCourseUseCase.Execute(courseId));
            Assert.Equal("Cannot publish a course without lessons.", exception.Message);
            
            _courseRepositoryMock.Verify(r => r.publishCourse(It.IsAny<Guid>()), Times.Never);
        }

        [Fact]
        public void DeleteCourse_ShouldBeSoftDelete()
        {
            // Arrange
            var courseId = Guid.NewGuid();
            _courseRepositoryMock.Setup(r => r.deleteCourse(courseId)).Returns(true);

            // Act
            var result = _deleteCourseUseCase.Execute(courseId);

            // Assert
            Assert.True(result);
            _courseRepositoryMock.Verify(r => r.deleteCourse(courseId), Times.Once);
        }
    }
}
