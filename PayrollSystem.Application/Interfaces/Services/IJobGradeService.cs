using PayrollSystem.Application.DTOs.JobGrades;
using System;
using System.Collections.Generic;
using System.Text;

namespace PayrollSystem.Application.Interfaces.Services
{
    public interface IJobGradeService
    {
        Task<IReadOnlyList<JobGradeDto>> GetAllAsync(CancellationToken cancellationToken = default);
        Task<JobGradeFormDto?> GetForEditAsync(int id, CancellationToken cancellationToken = default);
        Task<(bool IsSuccess, string ErrorMessage)> UpdateAsync(JobGradeFormDto dto, CancellationToken cancellationToken = default);
    }
}
