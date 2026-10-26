using PayrollSystem.Application.DTOs.Employees;
using PayrollSystem.Application.Interfaces.Repositories;
using PayrollSystem.Application.Interfaces.Services;
using PayrollSystem.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace PayrollSystem.Application.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository _employeeRepository;

        public EmployeeService(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        public async Task<IReadOnlyList<EmployeeDto>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            var employees = await _employeeRepository.GetAllWithDetailsAsync(cancellationToken);

            return employees.Select(e => new EmployeeDto
            {
                Id = e.Id,
                Name = e.Name,
                Email = e.Email,
                PhoneNumber = e.PhoneNumber,
                HireDate = e.HireDate,
                DepartmentName = e.Department.Name,
                JobGradeName = e.JobGrade.Name
            }).ToList();
        }

        public async Task<EmployeeFormDto?> GetForEditAsync(int id, CancellationToken cancellationToken = default)
        {
            var employee = await _employeeRepository.GetByIdAsync(id, cancellationToken);
            if (employee == null)
            {
                return null;
            }

            return new EmployeeFormDto
            {
                Id = employee.Id,
                Name = employee.Name,
                DateOfBirth = employee.DateOfBirth,
                Address = employee.Address,
                PhoneNumber = employee.PhoneNumber,
                Email = employee.Email,
                HireDate = employee.HireDate,
                JobGradeId = employee.JobGradeId,
                DepartmentId = employee.DepartmentId
            };
        }

        public async Task<(bool IsSuccess, string ErrorMessage)> CreateAsync(EmployeeFormDto dto, CancellationToken cancellationToken = default)
        {
            if (await _employeeRepository.ExistsByEmailAsync(dto.Email, null, cancellationToken))
            {
                return (false, "البريد الإلكتروني مسجل لموظف آخر.");
            }

            var employee = new Employee(
                dto.Name, dto.DateOfBirth, dto.Address, dto.PhoneNumber,
                dto.Email, dto.HireDate, dto.JobGradeId, dto.DepartmentId);

            await _employeeRepository.AddAsync(employee, cancellationToken);
            return (true, string.Empty);
        }

        public async Task<(bool IsSuccess, string ErrorMessage)> UpdateAsync(EmployeeFormDto dto, CancellationToken cancellationToken = default)
        {
            var employee = await _employeeRepository.GetByIdAsync(dto.Id, cancellationToken);
            if (employee == null)
            {
                return (false, "الموظف غير موجود.");
            }

            if (await _employeeRepository.ExistsByEmailAsync(dto.Email, dto.Id, cancellationToken))
            {
                return (false, "البريد الإلكتروني مسجل لموظف آخر.");
            }

            employee.Update(dto.Name, dto.DateOfBirth, dto.Address, dto.PhoneNumber,
                dto.Email, dto.HireDate, dto.JobGradeId, dto.DepartmentId);

            await _employeeRepository.UpdateAsync(employee, cancellationToken);
            return (true, string.Empty);
        }

        public async Task<(bool IsSuccess, string ErrorMessage)> DeleteAsync(int id, CancellationToken cancellationToken = default)
        {
            var employee = await _employeeRepository.GetByIdAsync(id, cancellationToken);
            if (employee == null)
            {
                return (false, "الموظف غير موجود.");
            }

            await _employeeRepository.RemoveAsync(employee, cancellationToken);
            return (true, string.Empty);
        }
    }
}
