using System;
using System.Collections.Generic;
using System.Text;

namespace PayrollSystem.Application.DTOs.Incentives
{
    public sealed record SeniorityIncentiveDto
    {
        public int Id { get; init; }
        public int MinYears { get; init; }
        public decimal IncentivePercentage { get; init; }
    }
}
