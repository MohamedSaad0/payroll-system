using System;
using System.Collections.Generic;
using System.Text;

namespace PayrollSystem.Domain.Entities
{
    public class AbsencePolicy
    {
        public int Id { get; private set; }
        public string Bracket { get; private set; } = string.Empty;
        public int FromDays { get; private set; }
        public int ToDays { get; private set; }
        public decimal Percentage { get; private set; }
        public bool IsIncentive { get; private set; }

        public AbsencePolicy()
        {
            
        }
        public AbsencePolicy(string bracket, int fromDays, int toDays, decimal percentage, bool isIncentive)
        {
            if(string.IsNullOrWhiteSpace(bracket))
            {
                throw new ArgumentException("Bracket can not be empty", nameof(bracket));
            }
            if(fromDays < 0 || toDays < fromDays)
            {
                throw new ArgumentException("Invalid absence days range.");
            }

            Bracket = bracket.Trim();
            FromDays = fromDays;
            ToDays = toDays;
            IsIncentive = isIncentive;

            UpdatePercentage(percentage);
        }

        public void UpdatePercentage(decimal percentage)
        {
           if(percentage < 0 || percentage > 100)
            {
                throw new ArgumentOutOfRangeException(nameof(percentage), "Percentege must be between 0 and 100 ");
            }

           Percentage = percentage;
        }
    }
}
