using System.ComponentModel.DataAnnotations;

namespace PersonnelApi.DTOs
{
    /// <summary>
    /// Data Transfer Object for creating a new employee.
    /// </summary>
    public class CreateEmployeeDto
    {
        /// <summary>
        /// Gets or sets the employee's first name.
        /// Required field.
        /// </summary>
        [Required] public string FirstName { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the employee's last name.
        /// Required field.
        /// </summary>
        [Required] public string LastName { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the employee's email address.
        /// Must be unique in the system.
        /// Required field.
        /// </summary>
        [Required, EmailAddress] public string Email { get; set; } = string.Empty;
    }
}
