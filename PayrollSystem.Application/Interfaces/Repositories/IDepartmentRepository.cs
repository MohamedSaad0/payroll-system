using PayrollSystem.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace PayrollSystem.Application.Interfaces.Repositories
{
    public interface IDepartmentRepository
    {
        Task<IReadOnlyList<Department>> GetAllAsync(CancellationToken cancellationToken = default);
        Task<Department?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
        Task<bool> ExistsByNameAsync(string name, int? excludedId = null, CancellationToken cancellationToken = default);

        Task AddAsync(Department department, CancellationToken cancellationToken = default);
        Task UpdateAsync(Department department, CancellationToken cancellationToken = default);
        Task RemoveAsync(Department department, CancellationToken cancellationToken = default);
    }
}
