using PayrollSystem.Application.DTOs.Incentives;
using PayrollSystem.Application.Interfaces.Repositories;
using PayrollSystem.Application.Interfaces.Services;
using PayrollSystem.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace PayrollSystem.Application.Services
{
    public class IncentiveService : IIncentiveService
    {
        private readonly ISeniorityIncentiveRepository _seniorityRepository;

        public IncentiveService(ISeniorityIncentiveRepository seniorityRepository)
        {
            _seniorityRepository = seniorityRepository;
        }

        public async Task<IReadOnlyList<SeniorityIncentiveDto>> GetAllSeniorityRulesAsync(CancellationToken cancellationToken = default)
        {
            var rules = await _seniorityRepository.GetAllAsync(cancellationToken);

            return rules.Select(r => new SeniorityIncentiveDto
            {
                Id = r.Id,
                MinYears = r.MinYears,
                IncentivePercentage = r.IncentivePercentage
            }).ToList();
        }

        public async Task<SeniorityIncentiveFormDto?> GetSeniorityRuleForEditAsync(int id, CancellationToken cancellationToken = default)
        {
            var rule = await _seniorityRepository.GetByIdAsync(id, cancellationToken);
            if (rule == null)
            {
                return null;
            }

            return new SeniorityIncentiveFormDto
            {
                Id = rule.Id,
                MinYears = rule.MinYears,
                IncentivePercentage = rule.IncentivePercentage
            };
        }

        public async Task<(bool IsSuccess, string ErrorMessage)> CreateSeniorityRuleAsync(SeniorityIncentiveFormDto dto, CancellationToken cancellationToken = default)
        {
            if (await _seniorityRepository.ExistsByMinYearsAsync(dto.MinYears, null, cancellationToken))
            {
                return (false, "يوجد حافز مسجل مسبقاً لهذا العدد من السنوات.");
            }

            var rule = new SeniorityIncentive(dto.MinYears, dto.IncentivePercentage);

            await _seniorityRepository.AddAsync(rule, cancellationToken);
            return (true, string.Empty);
        }

        public async Task<(bool IsSuccess, string ErrorMessage)> UpdateSeniorityRuleAsync(SeniorityIncentiveFormDto dto, CancellationToken cancellationToken = default)
        {
            var rule = await _seniorityRepository.GetByIdAsync(dto.Id, cancellationToken);
            if (rule == null)
                return (false, "القاعدة غير موجودة.");

            if (await _seniorityRepository.ExistsByMinYearsAsync(dto.MinYears, dto.Id, cancellationToken))
            {
                return (false, "يوجد حافز مسجل مسبقاً لهذا العدد من السنوات.");
            }

            rule.Update(dto.MinYears, dto.IncentivePercentage);

            await _seniorityRepository.UpdateAsync(rule, cancellationToken);
            return (true, string.Empty);
        }
    }
}
