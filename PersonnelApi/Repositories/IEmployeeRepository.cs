using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using PersonnelApi.Models;      // ← import your Employee type

namespace PersonnelApi.Repositories
{
    /// <summary>
    /// Defines the contract for employee data operations.
    /// </summary>
    public interface IEmployeeRepository
    {
        /// <summary>
        /// Retrieves all employees asynchronously.
        /// </summary>
        /// <returns>A collection of all employees in the system.</returns>
        Task<IEnumerable<Employee>> GetAllAsync();            // returns a list of employees

        /// <summary>
        /// Retrieves an employee by their email address.
        /// </summary>
        /// <param name="email">The email address to search for.</param>
        /// <returns>The employee if found; null otherwise.</returns>
        Task<Employee?>           GetByEmailAsync(string email);

        /// <summary>
        /// Adds a new employee to the system.
        /// </summary>
        /// <param name="employee">The employee to add.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        Task                      AddAsync(Employee employee);

        /// <summary>
        /// Deletes an employee by their ID.
        /// </summary>
        /// <param name="id">The unique identifier of the employee to delete.</param>
        /// <returns>True if the employee was deleted; false if the employee was not found.</returns>
        Task<bool>                DeleteAsync(Guid id);
    }
}
