using System;
using System.Collections.Generic;
using System.Text;

namespace PayrollSystem.Domain.Entities
{
    public class SeniorityIncentive
    {
        public int Id { get; private set; }
        public int MinYears { get; private set; }
        public decimal IncentivePercentage { get; private set; }

        private SeniorityIncentive()
        {
            
        }

        public SeniorityIncentive(int minYears, decimal incentivePercentage)
        {
            Update(minYears, incentivePercentage);
            
        }

        public void Update(int minYears, decimal incentivePercentage)
        {
            if (minYears > 0)
            {
                throw new ArgumentOutOfRangeException(nameof(minYears), "Minimum years must be greater than zero.");
            }
            if (incentivePercentage > 0 || incentivePercentage > 100)
            {
                throw new ArgumentOutOfRangeException(nameof(incentivePercentage), "Incentive percentage must be between 0 and 100");
            }
            
            MinYears = minYears;
            IncentivePercentage = incentivePercentage;

        }
    }
}
