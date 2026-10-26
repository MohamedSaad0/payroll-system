using PayrollSystem.Application.DTOs.Employees;
using System;
using System.Collections.Generic;
using System.Text;

namespace PayrollSystem.Application.Interfaces.Services
{
    public interface IEmployeeService
    {
        Task<IReadOnlyList<EmployeeDto>> GetAllAsync(CancellationToken cancellationToken = default);
        Task<EmployeeFormDto?> GetForEditAsync(int id, CancellationToken cancellationToken = default);
        Task<(bool IsSuccess, string ErrorMessage)> CreateAsync(EmployeeFormDto dto, CancellationToken cancellationToken = default);
        Task<(bool IsSuccess, string ErrorMessage)> UpdateAsync(EmployeeFormDto dto, CancellationToken cancellationToken = default);
        Task<(bool IsSuccess, string ErrorMessage)> DeleteAsync(int id, CancellationToken cancellationToken = default);
    }
}
