using PayrollSystem.Application.DTOs.Attendance;
using PayrollSystem.Application.Interfaces.Repositories;
using PayrollSystem.Application.Interfaces.Services;
using PayrollSystem.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace PayrollSystem.Application.Services
{
    public class AttendanceService : IAttendanceService
    {
        private readonly IAbsencePolicyRepository _policyRepository;
        private readonly IEmployeeAbsenceRepository _absenceRepository;

        public AttendanceService(
            IAbsencePolicyRepository policyRepository,
            IEmployeeAbsenceRepository absenceRepository)
        {
            _policyRepository = policyRepository;
            _absenceRepository = absenceRepository;
        }

        public async Task<IReadOnlyList<AbsencePolicyDto>> GetAllPoliciesAsync(CancellationToken cancellationToken = default)
        {
            var policies = await _policyRepository.GetAllAsync(cancellationToken);
            return policies.Select(p => new AbsencePolicyDto
            {
                Id = p.Id,
                Bracket = p.Bracket,
                Percentage = p.Percentage,
                IsIncentive = p.IsIncentive
            }).ToList();
        }

        public async Task<AbsencePolicyFormDto?> GetPolicyForEditAsync(int id, CancellationToken cancellationToken = default)
        {
            var policy = await _policyRepository.GetByIdAsync(id, cancellationToken);
            if (policy == null)
            {
                return null;
            }

            return new AbsencePolicyFormDto
            {
                Id = policy.Id,
                Bracket = policy.Bracket,
                Percentage = policy.Percentage
            };
        }

        public async Task<(bool IsSuccess, string ErrorMessage)> UpdatePolicyAsync(AbsencePolicyFormDto dto, CancellationToken cancellationToken = default)
        {
            var policy = await _policyRepository.GetByIdAsync(dto.Id, cancellationToken);
            if (policy == null)
            {
                return (false, "القاعدة غير موجودة.");
            }

            policy.UpdatePercentage(dto.Percentage);

            await _policyRepository.UpdateAsync(policy, cancellationToken);
            return (true, string.Empty);
        }

        public async Task<IReadOnlyList<EmployeeAbsenceDto>> GetAbsencesByMonthAsync(int year, int month, CancellationToken cancellationToken = default)
        {
            var absences = await _absenceRepository.GetByMonthAsync(year, month, cancellationToken);

            return absences.Select(a => new EmployeeAbsenceDto
            {
                Id = a.Id,
                EmployeeId = a.EmployeeId,
                EmployeeName = a.Employee.Name,
                DepartmentName = a.Employee.Department.Name,
                Year = a.Year,
                Month = a.Month,
                AbsenceDaysCount = a.AbsenceDaysCount
            }).ToList();
        }

        public async Task<EmployeeAbsenceFormDto?> GetAbsenceForEditAsync(int id, CancellationToken cancellationToken = default)
        {
            var absence = await _absenceRepository.GetByIdAsync(id, cancellationToken);
            if (absence == null)
            {
                return null;
            }

            return new EmployeeAbsenceFormDto
            {
                Id = absence.Id,
                EmployeeId = absence.EmployeeId,
                Year = absence.Year,
                Month = absence.Month,
                AbsenceDaysCount = absence.AbsenceDaysCount
            };
        }

        public async Task<(bool IsSuccess, string ErrorMessage)> CreateAbsenceAsync(EmployeeAbsenceFormDto dto, CancellationToken cancellationToken = default)
        {
            var existingRecord = await _absenceRepository.GetByEmployeeAndMonthAsync(dto.EmployeeId, dto.Year, dto.Month, cancellationToken);
            if (existingRecord != null)
            {
                return (false, "يوجد سجل غياب مسجل بالفعل لهذا الموظف في هذا الشهر والسنة. يرجى تعديل السجل الحالي.");
            }

            var absence = new EmployeeAbsence(dto.Year, dto.Month, dto.AbsenceDaysCount, dto.EmployeeId);

            await _absenceRepository.AddAsync(absence, cancellationToken);
            return (true, string.Empty);
        }

        public async Task<(bool IsSuccess, string ErrorMessage)> UpdateAbsenceAsync(EmployeeAbsenceFormDto dto, CancellationToken cancellationToken = default)
        {
            var absence = await _absenceRepository.GetByIdAsync(dto.Id, cancellationToken);
            if (absence == null)
            {
                return (false, "سجل الغياب غير موجود.");
            }
            absence.UpdateAbsenceDays(dto.AbsenceDaysCount);

            await _absenceRepository.UpdateAsync(absence, cancellationToken);
            return (true, string.Empty);
        }
    }
}
