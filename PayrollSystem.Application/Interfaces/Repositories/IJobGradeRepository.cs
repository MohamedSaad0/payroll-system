using PayrollSystem.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace PayrollSystem.Application.Interfaces.Repositories
{
    public interface IJobGradeRepository
    {
        Task<IReadOnlyList<JobGrade>> GetAllAsync(CancellationToken cancellationToken = default);
        Task<JobGrade?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
        Task AddAsync(JobGrade jobGrade, CancellationToken cancellationToken = default);
        Task UpdateAsync(JobGrade jobGrade, CancellationToken cancellationToken = default);
    }
}