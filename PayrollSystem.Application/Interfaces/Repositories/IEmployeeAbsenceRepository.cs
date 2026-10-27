using PayrollSystem.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace PayrollSystem.Application.Interfaces.Repositories
{
    public interface IEmployeeAbsenceRepository
    {
        Task<IReadOnlyList<EmployeeAbsence>> GetByMonthAsync(int year, int month, CancellationToken cancellationToken = default);
        Task<EmployeeAbsence?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
        Task<EmployeeAbsence?> GetByEmployeeAndMonthAsync(int employeeId, int year, int month, CancellationToken cancellationToken = default);

        Task AddAsync(EmployeeAbsence employee, CancellationToken cancellationToken = default);
        Task UpdateAsync(EmployeeAbsence employee, CancellationToken cancellationToken = default);
        Task RemoveAsync(EmployeeAbsence absence, CancellationToken cancellationToken = default);
    }

}
