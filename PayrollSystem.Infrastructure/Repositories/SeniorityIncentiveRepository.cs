using Microsoft.EntityFrameworkCore;
using PayrollSystem.Application.Interfaces.Repositories;
using PayrollSystem.Domain.Entities;
using PayrollSystem.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace PayrollSystem.Infrastructure.Repositories
{
    public class SeniorityIncentiveRepository : ISeniorityIncentiveRepository
    {
        private readonly PayrollDbContext _context;

        public SeniorityIncentiveRepository(PayrollDbContext context)
        {
            _context = context;
        }

        public async Task<IReadOnlyList<SeniorityIncentive>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return await _context.SeniorityIncentives.AsNoTracking().OrderBy(s => s.MinYears).ToListAsync(cancellationToken);
        }

        public async Task<SeniorityIncentive?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            return await _context.SeniorityIncentives.FirstOrDefaultAsync(s => s.Id == id, cancellationToken);
        }

        public async Task<bool> ExistsByMinYearsAsync(int minYears, int? excludeId = null, CancellationToken cancellationToken = default)
        {
            var query = _context.SeniorityIncentives.AsQueryable();

            if (excludeId.HasValue)
            {
                query = query.Where(s => s.Id != excludeId.Value);
            }

            return await query.AnyAsync(s => s.MinYears == minYears, cancellationToken);
        }

        public async Task AddAsync(SeniorityIncentive incentive, CancellationToken cancellationToken = default)
        {
            _context.SeniorityIncentives.Add(incentive);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task UpdateAsync(SeniorityIncentive incentive, CancellationToken cancellationToken = default)
        {
            _context.SeniorityIncentives.Update(incentive);
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
