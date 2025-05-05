using System;

namespace PersonnelApi.DTOs
{
    /// <summary>
    /// Data Transfer Object for returning employee information through the API.
    /// </summary>
    public class EmployeeDto
    {
        /// <summary>
        /// Gets or sets the unique identifier of the employee.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the employee's first name.
        /// </summary>
        public string FirstName { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the employee's last name.
        /// </summary>
        public string LastName { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the employee's email address.
        /// </summary>
        public string Email { get; set; } = string.Empty;
    }
}
