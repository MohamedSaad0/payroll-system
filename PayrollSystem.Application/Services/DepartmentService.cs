using PayrollSystem.Application.DTOs.Departments;
using PayrollSystem.Application.Interfaces.Repositories;
using PayrollSystem.Application.Interfaces.Services;
using PayrollSystem.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace PayrollSystem.Application.Services
{
    public class DepartmentService : IDepartmentService
    {
        private readonly IDepartmentRepository _departmentRepository;
        private readonly IEmployeeRepository _employeeRepository;

        public DepartmentService(
            IDepartmentRepository departmentRepository,
            IEmployeeRepository employeeRepository)
        {
            _departmentRepository = departmentRepository;
            _employeeRepository = employeeRepository;
        }

        public async Task<IReadOnlyList<DepartmentDto>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            var departments = await _departmentRepository.GetAllAsync(cancellationToken);

            return departments.Select(d => new DepartmentDto
            {
                Id = d.Id,
                Name = d.Name,
                IncentivePercentage = d.IncentivePercentage
            }).ToList();
        }

        public async Task<DepartmentFormDto?> GetForEditAsync(int id, CancellationToken cancellationToken = default)
        {
            var department = await _departmentRepository.GetByIdAsync(id, cancellationToken);
            if (department == null) return null;

            return new DepartmentFormDto
            {
                Id = department.Id,
                Name = department.Name,
                IncentivePercentage = department.IncentivePercentage
            };
        }

        public async Task<(bool IsSuccess, string ErrorMessage)> CreateAsync(DepartmentFormDto dto, CancellationToken cancellationToken = default)
        {
            if (await _departmentRepository.ExistsByNameAsync(dto.Name, null, cancellationToken))
            {
                return (false, "اسم القسم مسجل مسبقاً.");
            }

            var department = new Department(dto.Name, dto.IncentivePercentage);

            await _departmentRepository.AddAsync(department, cancellationToken);
            return (true, string.Empty);
        }

        public async Task<(bool IsSuccess, string ErrorMessage)> UpdateAsync(DepartmentFormDto dto, CancellationToken cancellationToken = default)
        {
            var department = await _departmentRepository.GetByIdAsync(dto.Id, cancellationToken);
            if (department == null)
            {
                return (false, "القسم غير موجود.");
            }

            if (await _departmentRepository.ExistsByNameAsync(dto.Name, dto.Id, cancellationToken))
            {
                return (false, "اسم القسم مسجل مسبقاً بقسم آخر.");
            }

            department.Update(dto.Name, dto.IncentivePercentage);

            await _departmentRepository.UpdateAsync(department, cancellationToken);
            return (true, string.Empty);
        }

        public async Task<(bool IsSuccess, string ErrorMessage)> DeleteAsync(int id, CancellationToken cancellationToken = default)
        {
            var department = await _departmentRepository.GetByIdAsync(id, cancellationToken);
            if (department == null)
            {
                return (false, "القسم غير موجود.");
            }

            if (await _employeeRepository.HasAnyByDepartmentIdAsync(id, cancellationToken))
            {
                return (false, "لا يمكن حذف هذا القسم لوجود موظفين مسجلين به. يرجى نقل الموظفين أولاً.");
            }

            await _departmentRepository.RemoveAsync(department, cancellationToken);
            return (true, string.Empty);
        }
    }
}
