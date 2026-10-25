using System;
using System.Collections.Generic;
using System.Text;

namespace PayrollSystem.Domain.Entities
{
    public class JobGrade
    {
        public int Id { get; private set; }
        public string Name { get; private set; } = string.Empty;
        public decimal BaseSalary { get; private set; }
        public ICollection<Employee> Employees { get; private set; } = [];

        private JobGrade() { }

        public JobGrade(string name, decimal baseSalary)
        {
            update(name, baseSalary);
        }

        public void update(string name, decimal baseSalary)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException("Grade name cannot be empty", nameof(name));
            }

            if (baseSalary <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(baseSalary), "Base salary must be greater than zero.");
            }

            Name = name;
            BaseSalary = baseSalary;
        }
    }
}
