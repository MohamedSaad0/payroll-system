using PayrollSystem.Application.DTOs.Reports;
using PayrollSystem.Application.Interfaces.Repositories;
using PayrollSystem.Application.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace PayrollSystem.Application.Services
{
    public class ReportService : IReportService
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IEmployeeAbsenceRepository _absenceRepository;
        private readonly ISeniorityIncentiveRepository _seniorityRepository;
        private readonly IAbsencePolicyRepository _policyRepository;
        public ReportService(
        IEmployeeRepository employeeRepository,
        IEmployeeAbsenceRepository absenceRepository,
        ISeniorityIncentiveRepository seniorityRepository,
        IAbsencePolicyRepository policyRepository)
        {
            _employeeRepository = employeeRepository;
            _absenceRepository = absenceRepository;
            _seniorityRepository = seniorityRepository;
            _policyRepository = policyRepository;
        }

        public async Task<IReadOnlyList<EmployeeReportDto>> GetEmployeesReportAsync(CancellationToken cancellationToken = default)
        {
            var employees = await _employeeRepository.GetAllWithDetailsAsync(cancellationToken);

            return employees.Select(e => new EmployeeReportDto
            {
                Name = e.Name,
                DepartmentName = e.Department.Name,
                JobGradeName = e.JobGrade.Name,
                BaseSalary = e.JobGrade.BaseSalary,
                DateOfBirth = e.DateOfBirth,
                HireDate = e.HireDate,
                Email = e.Email,
                PhoneNumber = e.PhoneNumber
            }).ToList();
        }

        public async Task<IReadOnlyList<AttendanceReportDto>> GetAttendanceReportAsync(int year, int month, CancellationToken cancellationToken = default)
        {
            var absences = await _absenceRepository.GetByMonthAsync(year, month, cancellationToken);

            return absences.Select(a => new AttendanceReportDto
            {
                EmployeeName = a.Employee.Name,
                DepartmentName = a.Employee.Department.Name,
                Year = a.Year,
                Month = a.Month,
                AbsenceDaysCount = a.AbsenceDaysCount
            }).ToList();
        }


        public async Task<IReadOnlyList<PayrollReportDto>> GetPayrollReportAsync(int year, int month, CancellationToken cancellationToken = default)
        {

            var employees = await _employeeRepository.GetAllWithDetailsAsync(cancellationToken);
            var absences = await _absenceRepository.GetByMonthAsync(year, month, cancellationToken);

            var rawSeniorityRules = await _seniorityRepository.GetAllAsync(cancellationToken);
            var seniorityRules = rawSeniorityRules.OrderByDescending(r => r.MinYears).ToList();

            var absencePolicies = await _policyRepository.GetAllAsync(cancellationToken);

            var reports = new List<PayrollReportDto>();

            var payrollDate = new DateTime(year, month, DateTime.DaysInMonth(year, month));

            foreach (var employee in employees)
            {
                var baseSalary = employee.JobGrade.BaseSalary;

                var deptIncentivePercent = employee.Department.IncentivePercentage;
                var deptIncentiveAmount = baseSalary * (deptIncentivePercent / 100m);

                int yearsOfService = payrollDate.Year - employee.HireDate.Year;
                if (employee.HireDate.Date > payrollDate.AddYears(-yearsOfService))
                {
                    yearsOfService--;
                }

                var matchedSeniorityRule = seniorityRules.FirstOrDefault(r => yearsOfService > r.MinYears);
                var seniorityIncentivePercent = matchedSeniorityRule?.IncentivePercentage ?? 0m;
                var seniorityIncentiveAmount = baseSalary * (seniorityIncentivePercent / 100m);

                var employeeAbsenceRecord = absences.FirstOrDefault(a => a.EmployeeId == employee.Id);
                var daysAbsent = employeeAbsenceRecord?.AbsenceDaysCount ?? 0;

                decimal attendanceIncentiveAmount = 0m;
                decimal absenceDeductionAmount = 0m;

                var matchedPolicy = absencePolicies.FirstOrDefault(p => daysAbsent >= p.FromDays && daysAbsent <= p.ToDays);

                if (matchedPolicy != null)
                {
                    var amount = baseSalary * (matchedPolicy.Percentage / 100m);

                    if (matchedPolicy.IsIncentive)
                    {
                        attendanceIncentiveAmount = amount;
                    }
                    else
                    {
                        absenceDeductionAmount = amount;
                    }
                }

                reports.Add(new PayrollReportDto
                {
                    EmployeeName = employee.Name,
                    DepartmentName = employee.Department.Name,
                    JobGradeName = employee.JobGrade.Name,
                    Year = year,
                    Month = month,
                    BaseSalary = baseSalary,
                    DepartmentIncentiveAmount = deptIncentiveAmount,
                    SeniorityIncentiveAmount = seniorityIncentiveAmount,
                    AttendanceIncentiveAmount = attendanceIncentiveAmount,
                    AbsenceDeductionAmount = absenceDeductionAmount
                });
            }

            return reports;
        }

    }
}
