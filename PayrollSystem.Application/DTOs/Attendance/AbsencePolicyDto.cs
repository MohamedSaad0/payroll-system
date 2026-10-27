using System;
using System.Collections.Generic;
using System.Text;

namespace PayrollSystem.Application.DTOs.Attendance
{
    public sealed record AbsencePolicyDto
    {
        public int Id { get; init; }
        public string Bracket { get; init; } = string.Empty;
        public decimal Percentage { get; init; }
        public bool IsIncentive { get; init; }
    }
}
