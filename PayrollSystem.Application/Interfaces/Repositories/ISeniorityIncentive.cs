using PayrollSystem.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace PayrollSystem.Application.Interfaces.Repositories
{
    public interface ISeniorityIncentive
    {
        Task<IReadOnlyList<SeniorityIncentive>> GetAllAsync(CancellationToken cancellationToken = default);
        Task<SeniorityIncentive?> FindByIdAsync(int id, CancellationToken cancellationToken = default);
        Task<bool> ExistsByMinYearsAsync(int minYear, int? excludeId = null, CancellationToken cancellationToken = default);

        Task AddAsync(SeniorityIncentive seniorityIncentive, CancellationToken cancellationToken = default);
        Task UpdateAsync(SeniorityIncentive seniorityIncentive, CancellationToken cancellationToken = default);

    }
}
