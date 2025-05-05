using System;
using System.Linq;
using System.Threading.Tasks;
using PersonnelApi.Models;
using PersonnelApi.Repositories;
using Xunit;

namespace PersonnelApi.Tests.Repositories
{
    public class InMemoryEmployeeRepositoryTests
    {
        private readonly InMemoryEmployeeRepository _repository;

        public InMemoryEmployeeRepositoryTests()
        {
            _repository = new InMemoryEmployeeRepository();
        }

        [Fact]
        public async Task GetAllAsync_WhenEmpty_ReturnsEmptyList()
        {
            // Act
            var result = await _repository.GetAllAsync();

            // Assert
            Assert.Empty(result);
        }

        [Fact]
        public async Task AddAsync_WhenValidEmployee_CanBeRetrieved()
        {
            // Arrange
            var employee = new Employee
            {
                FirstName = "John",
                LastName = "Doe",
                Email = "john.doe@example.com"
            };

            // Act
            await _repository.AddAsync(employee);
            var result = await _repository.GetAllAsync();

            // Assert
            Assert.Single(result);
            var retrieved = result.First();
            Assert.Equal(employee.FirstName, retrieved.FirstName);
            Assert.Equal(employee.LastName, retrieved.LastName);
            Assert.Equal(employee.Email, retrieved.Email);
        }

        [Fact]
        public async Task GetByEmailAsync_WhenEmployeeExists_ReturnsEmployee()
        {
            // Arrange
            var employee = new Employee
            {
                FirstName = "Jane",
                LastName = "Smith",
                Email = "jane.smith@example.com"
            };
            await _repository.AddAsync(employee);

            // Act
            var result = await _repository.GetByEmailAsync(employee.Email);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(employee.Email, result.Email);
        }

        [Fact]
        public async Task GetByEmailAsync_WhenEmployeeDoesNotExist_ReturnsNull()
        {
            // Act
            var result = await _repository.GetByEmailAsync("nonexistent@example.com");

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task DeleteAsync_WhenEmployeeExists_ReturnsTrue()
        {
            // Arrange
            var employee = new Employee
            {
                FirstName = "Bob",
                LastName = "Johnson",
                Email = "bob.johnson@example.com"
            };
            await _repository.AddAsync(employee);

            // Act
            var result = await _repository.DeleteAsync(employee.Id);

            // Assert
            Assert.True(result);
            var allEmployees = await _repository.GetAllAsync();
            Assert.Empty(allEmployees);
        }

        [Fact]
        public async Task DeleteAsync_WhenEmployeeDoesNotExist_ReturnsFalse()
        {
            // Act
            var result = await _repository.DeleteAsync(Guid.NewGuid());

            // Assert
            Assert.False(result);
        }
    }
} 