using PayrollSystem.Application.DTOs.JobGrades;
using PayrollSystem.Application.Interfaces.Repositories;
using PayrollSystem.Application.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace PayrollSystem.Application.Services
{
    public class JobGradeService : IJobGradeService
    {
        private readonly IJobGradeRepository _jobGradeRepository;

        public JobGradeService(IJobGradeRepository jobGradeRepository)
        {
            _jobGradeRepository = jobGradeRepository;
        }

        public async Task<IReadOnlyList<JobGradeDto>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            var grades = await _jobGradeRepository.GetAllAsync(cancellationToken);

            return grades.Select(g => new JobGradeDto
            {
                Id = g.Id,
                Name = g.Name,
                BaseSalary = g.BaseSalary
            }).ToList();
        }

        public async Task<JobGradeFormDto?> GetForEditAsync(int id, CancellationToken cancellationToken = default)
        {
            var grade = await _jobGradeRepository.GetByIdAsync(id, cancellationToken);
            if (grade == null) return null;

            return new JobGradeFormDto
            {
                Id = grade.Id,
                Name = grade.Name,
                BaseSalary = grade.BaseSalary
            };
        }

        public async Task<(bool IsSuccess, string ErrorMessage)> UpdateAsync(JobGradeFormDto dto, CancellationToken cancellationToken = default)
        {
            var grade = await _jobGradeRepository.GetByIdAsync(dto.Id, cancellationToken);
            if (grade == null)
            {
                return (false, "الدرجة الوظيفية غير موجودة.");
            }

            grade.Update(grade.Name, dto.BaseSalary);

            await _jobGradeRepository.UpdateAsync(grade, cancellationToken);
            return (true, string.Empty);
        }
    }
}
