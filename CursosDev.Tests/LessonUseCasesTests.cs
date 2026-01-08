using Xunit;
using Moq;
using CursosDev.Application.UseCases;
using CursosDev.Domain.Entities;
using CursosDev.Domain.Ports.Out;

namespace CursosDev.Tests
{
    public class LessonUseCasesTests
    {
        private readonly Mock<ILessonRepository> _lessonRepositoryMock;
        private readonly CreateLessonUseCase _createLessonUseCase;

        public LessonUseCasesTests()
        {
            _lessonRepositoryMock = new Mock<ILessonRepository>();
            _createLessonUseCase = new CreateLessonUseCase(_lessonRepositoryMock.Object);
        }

        [Fact]
        public void CreateLesson_WithUniqueOrder_ShouldSucceed()
        {
            // Arrange
            var courseId = Guid.NewGuid();
            var existingLessons = new List<Lesson>
            {
                new Lesson { Id = Guid.NewGuid(), CourseId = courseId, Order = 1 }
            };

            var newLesson = new Lesson 
            { 
                Title = "New Lesson", 
                CourseId = courseId, 
                Order = 2 
            };

            _lessonRepositoryMock.Setup(r => r.GetLessonsByCourseId(courseId)).Returns(existingLessons);
            _lessonRepositoryMock.Setup(r => r.CreateLesson(It.IsAny<Lesson>())).Returns(newLesson);

            // Act
            var result = _createLessonUseCase.Execute(newLesson);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(newLesson.Order, result.Order);
            _lessonRepositoryMock.Verify(r => r.CreateLesson(It.IsAny<Lesson>()), Times.Once);
        }

        [Fact]
        public void CreateLesson_WithDuplicateOrder_ShouldFail()
        {
            // Arrange
            var courseId = Guid.NewGuid();
            var existingLessons = new List<Lesson>
            {
                new Lesson { Id = Guid.NewGuid(), CourseId = courseId, Order = 1 }
            };

            var newLesson = new Lesson 
            { 
                Title = "Duplicate Order Lesson", 
                CourseId = courseId, 
                Order = 1 // Duplicate
            };

            _lessonRepositoryMock.Setup(r => r.GetLessonsByCourseId(courseId)).Returns(existingLessons);

            // Act & Assert
            var exception = Assert.Throws<InvalidOperationException>(() => _createLessonUseCase.Execute(newLesson));
            Assert.Equal($"A lesson with order {newLesson.Order} already exists in this course.", exception.Message);
            
            _lessonRepositoryMock.Verify(r => r.CreateLesson(It.IsAny<Lesson>()), Times.Never);
        }
    }
}
