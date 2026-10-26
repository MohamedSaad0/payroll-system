using Microsoft.EntityFrameworkCore;
using PayrollSystem.Application.Interfaces.Repositories;
using PayrollSystem.Domain.Entities;
using PayrollSystem.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace PayrollSystem.Infrastructure.Repositories
{
    public class AbsencePolicyRepository : IAbsencePolicyRepository
    {
        private readonly PayrollDbContext _context;
        public AbsencePolicyRepository(PayrollDbContext context)
        {
            _context = context;
        }
        public async Task<IReadOnlyList<AbsencePolicy>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return await _context.AbsencePolicies.AsNoTracking().OrderBy(a => a.FromDays).ToListAsync(cancellationToken);
        }

        public async Task<AbsencePolicy?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            return await _context.AbsencePolicies.FirstOrDefaultAsync(a => a.Id == id, cancellationToken);
        }

        public async Task UpdateAsync(AbsencePolicy policy, CancellationToken cancellationToken = default)
        {
            _context.AbsencePolicies.Update(policy);
            await _context.SaveChangesAsync();
        }
    }
}
