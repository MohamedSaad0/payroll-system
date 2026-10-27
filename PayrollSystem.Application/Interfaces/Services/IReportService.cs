using PayrollSystem.Application.DTOs.Reports;
using System;
using System.Collections.Generic;
using System.Text;

namespace PayrollSystem.Application.Interfaces.Services
{
    public interface IReportService
    {
        Task<IReadOnlyList<AttendanceReportDto>> GetAttendanceReportAsync(int year, int month, CancellationToken cancellationToken = default);
        Task<IReadOnlyList<PayrollReportDto>> GetPayrollReportAsync(int year, int month, CancellationToken cancellationToken = default);
        Task<IReadOnlyList<EmployeeReportDto>> GetEmployeesReportAsync(CancellationToken cancellationToken = default);
    }
}
