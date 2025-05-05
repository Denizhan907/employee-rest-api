using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Moq;
using PersonnelApi.Controllers;
using PersonnelApi.DTOs;
using PersonnelApi.Models;
using PersonnelApi.Repositories;
using Xunit;

namespace PersonnelApi.Tests.Controllers
{
    public class EmployeesControllerTests
    {
        private readonly Mock<IEmployeeRepository> _mockRepo;
        private readonly EmployeesController _controller;

        public EmployeesControllerTests()
        {
            _mockRepo = new Mock<IEmployeeRepository>();
            _controller = new EmployeesController(_mockRepo.Object);
        }

        [Fact]
        public async Task GetAll_ReturnsAllEmployees()
        {
            // Arrange
            var employees = new List<Employee>
            {
                new Employee { Id = Guid.NewGuid(), FirstName = "John", LastName = "Doe", Email = "john@example.com" },
                new Employee { Id = Guid.NewGuid(), FirstName = "Jane", LastName = "Smith", Email = "jane@example.com" }
            };
            _mockRepo.Setup(repo => repo.GetAllAsync())
                .ReturnsAsync(employees);

            // Act
            var result = await _controller.GetAll();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnValue = Assert.IsAssignableFrom<IEnumerable<EmployeeDto>>(okResult.Value);
            Assert.Equal(2, returnValue.Count());
        }

        [Fact]
        public async Task Create_WithValidData_ReturnsCreatedResult()
        {
            // Arrange
            var createDto = new CreateEmployeeDto
            {
                FirstName = "John",
                LastName = "Doe",
                Email = "john@example.com"
            };

            _mockRepo.Setup(repo => repo.GetByEmailAsync(createDto.Email))
                .ReturnsAsync((Employee)null);

            // Act
            var result = await _controller.Create(createDto);

            // Assert
            var createdResult = Assert.IsType<CreatedAtActionResult>(result.Result);
            var returnValue = Assert.IsType<EmployeeDto>(createdResult.Value);
            Assert.Equal(createDto.FirstName, returnValue.FirstName);
            Assert.Equal(createDto.LastName, returnValue.LastName);
            Assert.Equal(createDto.Email, returnValue.Email);
        }

        [Fact]
        public async Task Create_WithDuplicateEmail_ReturnsConflict()
        {
            // Arrange
            var createDto = new CreateEmployeeDto
            {
                FirstName = "John",
                LastName = "Doe",
                Email = "john@example.com"
            };

            var existingEmployee = new Employee
            {
                Id = Guid.NewGuid(),
                FirstName = "Existing",
                LastName = "User",
                Email = createDto.Email
            };

            _mockRepo.Setup(repo => repo.GetByEmailAsync(createDto.Email))
                .ReturnsAsync(existingEmployee);

            // Act
            var result = await _controller.Create(createDto);

            // Assert
            Assert.IsType<ConflictObjectResult>(result.Result);
        }

        [Fact]
        public async Task Delete_WhenEmployeeExists_ReturnsNoContent()
        {
            // Arrange
            var employeeId = Guid.NewGuid();
            _mockRepo.Setup(repo => repo.DeleteAsync(employeeId))
                .ReturnsAsync(true);

            // Act
            var result = await _controller.Delete(employeeId);

            // Assert
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task Delete_WhenEmployeeDoesNotExist_ReturnsNotFound()
        {
            // Arrange
            var employeeId = Guid.NewGuid();
            _mockRepo.Setup(repo => repo.DeleteAsync(employeeId))
                .ReturnsAsync(false);

            // Act
            var result = await _controller.Delete(employeeId);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }
    }
} 