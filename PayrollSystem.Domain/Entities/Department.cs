using System;
using System.Collections.Generic;
using System.Text;

namespace PayrollSystem.Domain.Entities
{
    public class Department
    {
        public int Id { get; private set; }
        public string Name { get; private set; } = string.Empty;
        public decimal IncentivePercentage { get; private set; }

        public ICollection<Employee> Employees { get; private set; } = [];

        private Department()
        {
            
        }

        public Department(int id, string name, decimal incentivePercentage = 0)
        {
            Update(name, incentivePercentage);
        }

        private void Update(string name, decimal incentivePercentage)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentNullException(nameof(name), "Department name cannot be empty.");
            }

            Name = name.Trim();
            UpdateIncentive(incentivePercentage);
        }

        public void UpdateIncentive(decimal incentivePercentage)
        {
            if (incentivePercentage < 0 || incentivePercentage > 100)
            {
                throw new ArgumentOutOfRangeException(nameof(incentivePercentage), "Incentive percentage must be between 0 and 100");
            }

            IncentivePercentage = incentivePercentage;
        }
    }
}
