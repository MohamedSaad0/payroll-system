using System;
using System.Collections.Generic;
using System.Text;

namespace PayrollSystem.Application.DTOs.Attendance
{
    public sealed record EmployeeAbsenceDto
    {
        public int Id { get; init; }
        public int EmployeeId { get; init; }
        public string EmployeeName { get; init; } = string.Empty;
        public string DepartmentName { get; init; } = string.Empty;
        public int Year { get; init; }
        public int Month { get; init; }
        public int AbsenceDaysCount { get; init; }
    }
}
