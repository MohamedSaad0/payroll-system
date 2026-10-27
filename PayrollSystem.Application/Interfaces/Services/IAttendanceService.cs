using PayrollSystem.Application.DTOs.Attendance;
using System;
using System.Collections.Generic;
using System.Text;

namespace PayrollSystem.Application.Interfaces.Services
{
    public interface IAttendanceService
    {
        Task<IReadOnlyList<AbsencePolicyDto>> GetAllPoliciesAsync(CancellationToken cancellationToken = default);
        Task<AbsencePolicyFormDto?> GetPolicyForEditAsync(int id, CancellationToken cancellationToken = default);
        Task<(bool IsSuccess, string ErrorMessage)> UpdatePolicyAsync(AbsencePolicyFormDto dto, CancellationToken cancellationToken = default);
        Task<IReadOnlyList<EmployeeAbsenceDto>> GetAbsencesByMonthAsync(int year, int month, CancellationToken cancellationToken = default);
        Task<EmployeeAbsenceFormDto?> GetAbsenceForEditAsync(int id, CancellationToken cancellationToken = default);
        Task<(bool IsSuccess, string ErrorMessage)> CreateAbsenceAsync(EmployeeAbsenceFormDto dto, CancellationToken cancellationToken = default);
        Task<(bool IsSuccess, string ErrorMessage)> UpdateAbsenceAsync(EmployeeAbsenceFormDto dto, CancellationToken cancellationToken = default);
    }
}
