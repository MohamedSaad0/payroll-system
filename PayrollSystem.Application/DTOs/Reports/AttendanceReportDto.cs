using System;
using System.Collections.Generic;
using System.Text;

namespace PayrollSystem.Application.DTOs.Reports
{
    public sealed record AttendanceReportDto
    {
        public string EmployeeName { get; init; } = string.Empty;
        public string DepartmentName { get; init; } = string.Empty; 
        public int Year { get; init; }
        public int Month { get; init; }
        public int AbsenceDaysCount { get; init; }

    }
}
