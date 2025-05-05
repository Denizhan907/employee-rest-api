using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PersonnelApi.Models;      // ← import Employee

namespace PersonnelApi.Repositories
{
    public class InMemoryEmployeeRepository : IEmployeeRepository
    {
        private readonly List<Employee> _employees = new();

        public Task<IEnumerable<Employee>> GetAllAsync() =>
            Task.FromResult<IEnumerable<Employee>>(_employees);

        public Task<Employee?> GetByEmailAsync(string email) =>
            Task.FromResult(_employees.FirstOrDefault(e => e.Email == email));

        public Task AddAsync(Employee employee)
        {
            _employees.Add(employee);
            return Task.CompletedTask;
        }

        public Task<bool> DeleteAsync(Guid id)
        {
            var existing = _employees.FirstOrDefault(e => e.Id == id);
            if (existing is null) return Task.FromResult(false);

            _employees.Remove(existing);
            return Task.FromResult(true);
        }
    }
}
