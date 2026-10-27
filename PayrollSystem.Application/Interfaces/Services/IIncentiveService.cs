using PayrollSystem.Application.DTOs.Incentives;
using System;
using System.Collections.Generic;
using System.Text;

namespace PayrollSystem.Application.Interfaces.Services
{
    public interface IIncentiveService
    {
        Task<IReadOnlyList<SeniorityIncentiveDto>> GetAllSeniorityRulesAsync(CancellationToken cancellationToken = default);
        Task<SeniorityIncentiveFormDto?> GetSeniorityRuleForEditAsync(int id, CancellationToken cancellationToken = default);
        Task<(bool IsSuccess, string ErrorMessage)> CreateSeniorityRuleAsync(SeniorityIncentiveFormDto dto, CancellationToken cancellationToken = default);
        Task<(bool IsSuccess, string ErrorMessage)> UpdateSeniorityRuleAsync(SeniorityIncentiveFormDto dto, CancellationToken cancellationToken = default);
        Task<(bool IsSuccess, string ErrorMessage)> UpdateDepartmentIncentiveAsync(int id, decimal percentage, CancellationToken cancellationToken = default);
    }
}
