using PayrollSystem.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace PayrollSystem.Application.Interfaces.Repositories
{
    public interface IEmployeeRepository
    {
        Task<IReadOnlyList<Employee>> GetAllWithDetailsAsync(CancellationToken cancellationToken = default);
        Task<Employee?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
        Task<Employee?> GetByIdWithDetailsAsync(int id, CancellationToken cancellationToken = default);

        Task<bool> ExistsByEmailAsync(string email, int? excludedId,CancellationToken cancellationToken = default);
        Task<bool> HasAnyByDepartmentIdAsync(int departmentId, CancellationToken cancellationToken = default);

        Task AddAsync(Employee employee, CancellationToken cancellationToken = default);
        Task UpdateAsync(Employee employee, CancellationToken cancellationToken = default);
        Task RemoveAsync(Employee employee, CancellationToken cancellationToken = default);
    }
}
