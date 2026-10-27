using System;
using System.Collections.Generic;
using System.Text;

namespace PayrollSystem.Application.DTOs.Reports
{
    public sealed  record PayrollReportDto
    {
        public string EmployeeName { get; init; } = string.Empty;
        public string DepartmentName { get; init; } = string.Empty;
        public string JobGradeName { get; init; } = string.Empty;
        public int Year { get; init; }
        public int Month { get; init; }
        public decimal BaseSalary { get; init; }
        public decimal DepartmentIncentiveAmount { get; init; }
        public decimal SeniorityIncentiveAmount { get; init; }
        public decimal AttendanceIncentiveAmount { get; init; }
        public decimal TotalIncentives => DepartmentIncentiveAmount + SeniorityIncentiveAmount + AttendanceIncentiveAmount;
        public decimal AbsenceDeductionAmount { get; init; }
        public decimal NetSalary => BaseSalary + TotalIncentives - AbsenceDeductionAmount;

    }
}
