using System;
using System.Collections.Generic;
using System.Text;

namespace PayrollSystem.Domain.Entities
{
    public class Employee
    {
        public int Id { get; private set; }
        public string Name { get; private set; } = string.Empty;
        public DateTime DateOfBirth { get; private set; }
        public string Address { get; private set; } = string.Empty;
        public string PhoneNumber { get; private set; } = string.Empty;
        public string Email { get; private set; } = string.Empty;
        public DateTime HireDate { get; private set; }
        public int JobGradeId { get; private set; }
        public JobGrade JobGrade { get; private set; } = null!;
        public int DepartmentId { get; private set; }
        public Department Department { get; private set; } = null!;
        public ICollection<EmployeeAbsence> Absences { get; private set; } = [];

        private Employee()
        {

        }

        public Employee
            (string name,
            DateTime dateOfBirth,
            string address,
            string phoneNumber,
            string email,
            DateTime hireDate,
            int jobGradeId,
            int departmentId)
        {
            Update(name, dateOfBirth, address, phoneNumber, email, hireDate, jobGradeId, departmentId);
        }

        public void Update(string name, DateTime dateOfBirth, string address, string phoneNumber, string email, DateTime hireDate, int jobGradeId, int departmentId)
        {
            if(string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException("Employee name is required.", nameof(name));
            }
            if (hireDate < dateOfBirth.AddYears(18))
            {
                throw new ArgumentException("Employee must be at least 18 years old at the time of hiring.");
            }
            if (string.IsNullOrWhiteSpace(email))
            {
                throw new ArgumentException("Email is required.", nameof(email));
            }
            if(jobGradeId <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(jobGradeId), "Valid job grade is required");
            }
            if(departmentId <=0)
            {
                throw new ArgumentOutOfRangeException(nameof(departmentId), "Valid department grade is required");
            }

            Name = name.Trim();
            DateOfBirth = dateOfBirth;
            Address = address?.Trim() ?? string.Empty;
            PhoneNumber = phoneNumber?.Trim() ?? string.Empty;
            Email = email.Trim();
            HireDate = hireDate;
            JobGradeId = jobGradeId;
            DepartmentId = departmentId;
        }

    }
}
