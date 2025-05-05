using Microsoft.AspNetCore.Mvc;
using PersonnelApi.DTOs;
using PersonnelApi.Models;
using PersonnelApi.Repositories;

namespace PersonnelApi.Controllers
{
    /// <summary>
    /// Controller for managing employee resources.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeesController : ControllerBase
    {
        private readonly IEmployeeRepository _repo;

        /// <summary>
        /// Initializes a new instance of the EmployeesController.
        /// </summary>
        /// <param name="repo">The employee repository.</param>
        public EmployeesController(IEmployeeRepository repo) => _repo = repo;

        /// <summary>
        /// Retrieves all employees.
        /// </summary>
        /// <returns>A list of all employees.</returns>
        /// <response code="200">Returns the list of employees</response>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<EmployeeDto>), 200)]
        public async Task<ActionResult<IEnumerable<EmployeeDto>>> GetAll()
        {
            var emps = await _repo.GetAllAsync();
            var dtos = emps.Select(e => new EmployeeDto {
                Id        = e.Id,
                FirstName = e.FirstName,
                LastName  = e.LastName,
                Email     = e.Email
            }).ToList();
            return Ok(dtos);
        }

        /// <summary>
        /// Creates a new employee.
        /// </summary>
        /// <param name="dto">The employee creation data.</param>
        /// <returns>The created employee.</returns>
        /// <response code="201">Returns the newly created employee</response>
        /// <response code="409">If an employee with the same email already exists</response>
        [HttpPost]
        [ProducesResponseType(typeof(EmployeeDto), 201)]
        [ProducesResponseType(409)]
        public async Task<ActionResult<EmployeeDto>> Create(CreateEmployeeDto dto)
        {
            if (await _repo.GetByEmailAsync(dto.Email) is not null)
                return Conflict(new { message = "Email must be unique." });

            var employee = new Employee {
                FirstName = dto.FirstName!,
                LastName  = dto.LastName!,
                Email     = dto.Email!
            };

            await _repo.AddAsync(employee);

            var result = new EmployeeDto {
                Id        = employee.Id,
                FirstName = employee.FirstName,
                LastName  = employee.LastName,
                Email     = employee.Email
            };

            return CreatedAtAction(nameof(GetAll), null, result);
        }

        /// <summary>
        /// Deletes an employee by ID.
        /// </summary>
        /// <param name="id">The ID of the employee to delete.</param>
        /// <returns>No content if successful, not found if employee doesn't exist.</returns>
        /// <response code="204">If the employee was successfully deleted</response>
        /// <response code="404">If the employee was not found</response>
        [HttpDelete("{id:guid}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> Delete(Guid id)
        {
            var removed = await _repo.DeleteAsync(id);
            if (!removed) return NotFound();
            return NoContent();
        }
    }
}
