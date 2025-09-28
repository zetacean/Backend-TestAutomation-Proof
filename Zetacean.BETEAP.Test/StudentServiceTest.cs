using AutoMapper;
using FluentAssertions;
using Moq;
using Zetacean.BETEAP.Students.DTOs;
using Zetacean.BETEAP.Students.Models;
using Zetacean.BETEAP.Students.Repository;
using Zetacean.BETEAP.Students.Services;

namespace Zetacean.BETEAP.Test
{
    public class StudentServiceTest
    {
        private readonly Mock<IRepository<Student>> _mockRepo;
        private readonly Mock<IMapper> _mockMapper;
        private readonly StudentService _service;

        public StudentServiceTest()
        {
            _mockRepo = new Mock<IRepository<Student>>();
            _mockMapper = new Mock<IMapper>();
            _service = new StudentService(_mockRepo.Object, _mockMapper.Object);
        }

        [Fact]
        public async Task Get_ShouldReturnMappedStudents()
        {
            // Arrange
            var student1 = Utils.GenerateRandomStudent();
            var student2 = Utils.GenerateRandomStudent();

            var students = new List<Student>() { student1, student2 };

            _mockRepo.Setup(r => r.Get()).ReturnsAsync(students);
            _mockMapper
                .Setup(m => m.Map<StudentDto>(It.IsAny<Student>()))
                .Returns<Student>(s => new StudentDto
                {
                    Id = s.Id,
                    DocumentNumber = s.DocumentNumber,
                    FirstName = s.FirstName,
                    LastName = s.LastName,
                });

            // Act
            var result = await _service.Get();

            // Assert
            result.Should().HaveCount(2);
            result.Should().ContainSingle(r => r.FirstName == student1.FirstName);
            result.Should().ContainSingle(r => r.FirstName == student2.FirstName);
        }

        [Fact]
        public async Task GetById_ShouldReturnMappedStudent()
        {
            // Arrange
            var student = Utils.GenerateRandomStudent();

            _mockRepo.Setup(r => r.GetById(student.Id)).ReturnsAsync(student);
            _mockMapper
                .Setup(m => m.Map<StudentDto>(It.IsAny<Student>()))
                .Returns<Student>(s => new StudentDto
                {
                    Id = s.Id,
                    BirthDate = s.BirthDate,
                    DocumentNumber = s.DocumentNumber,
                    FirstName = s.FirstName,
                    LastName = s.LastName,
                });

            // Act
            var result = await _service.GetById(student.Id);

            // Assert
            result.Should().NotBeNull();
            result.FirstName.Should().Be(student.FirstName);
        }

        [Fact]
        public async Task GetById_ShouldReturnNull_WhenStudentDoesNotExist()
        {
            // Arrange
            _mockRepo.Setup(r => r.GetById(It.IsAny<int>())).ReturnsAsync((Student?)null);

            // Act
            var result = await _service.GetById(998);

            // Assert
            result.Should().BeNull();
        }
    }
}
