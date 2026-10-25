using PayrollSystem.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace PayrollSystem.Application.Interfaces.Repositories
{
    public interface IAbsencePolicy
    {
        Task<IReadOnlyList<AbsencePolicy>> GetAllAsync(CancellationToken cancellationToken = default);
        Task<AbsencePolicy?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
    }
}
