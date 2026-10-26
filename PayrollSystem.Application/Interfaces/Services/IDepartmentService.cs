using PayrollSystem.Application.DTOs.Departments;
using System;
using System.Collections.Generic;
using System.Text;

namespace PayrollSystem.Application.Interfaces.Services
{
    public interface IDepartmentService
    {
        Task<IReadOnlyList<DepartmentDto>> GetAllAsync(CancellationToken cancellationToken = default);
        Task<DepartmentFormDto?> GetForEditAsync(int id, CancellationToken cancellationToken = default);
        Task<(bool IsSuccess, string ErrorMessage)> CreateAsync(DepartmentFormDto dto, CancellationToken cancellationToken = default);
        Task<(bool IsSuccess, string ErrorMessage)> UpdateAsync(DepartmentFormDto dto, CancellationToken cancellationToken = default);
        Task<(bool IsSuccess, string ErrorMessage)> DeleteAsync(int id, CancellationToken cancellationToken = default);
    }
}
