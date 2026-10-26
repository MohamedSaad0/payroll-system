using Microsoft.EntityFrameworkCore;
using PayrollSystem.Application.Interfaces.Repositories;
using PayrollSystem.Domain.Entities;
using PayrollSystem.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace PayrollSystem.Infrastructure.Repositories
{
    public class JobGradeRepository : IJobGradeRepository
    {
        private readonly PayrollDbContext _context;
        public JobGradeRepository(PayrollDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(JobGrade jobGrade, CancellationToken cancellationToken = default)
        {
            _context.JobGrades.Add(jobGrade);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task<IReadOnlyList<JobGrade>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return await _context.JobGrades.AsNoTracking().ToListAsync(cancellationToken);
        }

        public async Task<JobGrade?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            return await _context.JobGrades.FirstOrDefaultAsync(j => j.Id == id, cancellationToken);
        }

        public async Task UpdateAsync(JobGrade jobGrade, CancellationToken cancellationToken = default)
        {
            _context.JobGrades.Update(jobGrade);
            await _context.SaveChangesAsync(cancellationToken);

        }
    }
}
